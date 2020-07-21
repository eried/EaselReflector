using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using EaselReflector.Properties;
using FolderSelect;
using Shell32;

namespace EaselReflector
{
    public partial class FormMain : Form
    {
        private readonly FolderSelectDialog _folder = new FolderSelectDialog {Title = "Directory for syncing projects"};
        private int _lastPage = 1;
        private readonly List<ProjectLink> _projects = new List<ProjectLink>();

        public FormMain()
        {
            InitializeComponent();

            labelVersion.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            if (Directory.Exists(Settings.Default.LastFolder))
                _folder.FileName = Settings.Default.LastFolder;

            UpdateGui();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            BrowseFolder();
        }

        private void BrowseFolder()
        {
            if (_folder.ShowDialog())
            {
                UpdateGui();
                buttonSync.Select();
            }
        }

        private void buttonSync_Click(object sender, EventArgs e)
        {
            try
            {
                // Reset the page counter for downloading the Easel projects
                _lastPage = 1;

                UpdateGui(false);

                // Check the existing files
                ReadAllProjects();

                // Sync projects
                backgroundWorkerDownload.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to sync your projects.\n\n" + ex.Message, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                UpdateGui();
            }
        }

        private void UpdateGui(bool isEnabled = true)
        {
            groupBoxOptions.Enabled = isEnabled;
            buttonClose.Enabled = isEnabled;
            progressBarMain.Visible = !isEnabled;
            textBoxFile.Text = _folder.FileName;
            buttonSync.Enabled = isEnabled && !string.IsNullOrEmpty(_folder.FileName);
            progressBarMain.Style = ProgressBarStyle.Marquee;
        }

        private void backgroundWorkerDownload_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = 0;
            bool nextPage;

            do
            {
                // Do not advance to next page by default
                nextPage = false;

                // Download the projects page
                var request = (HttpWebRequest) WebRequest.Create(string.Format(Resources.UrlProjects, _lastPage));
                request.CookieContainer = GetCookies.GetUriCookieContainer(Resources.UrlHome);

                var response = request.GetResponse();
                if (response.ResponseUri.AbsolutePath.Contains("sign_in"))
                {
                    // Can't download, ask for login
                    e.Result = -1;
                }
                else
                {
                    var r = new StreamReader(response.GetResponseStream()).ReadToEnd();

                    // Parse projects
                    foreach (Match p in Regex.Matches(r, Resources.RegexProjects,
                        RegexOptions.Singleline))
                        backgroundWorkerDownload.ReportProgress(-1, new Tuple<string, string>(p.Groups["url"].Value,
                            p.Groups["name"].Value.Replace("\n", "").Replace("\r", "").Trim()));

                    if (r.Contains("class=\"next_page\""))
                    {
                        _lastPage++;
                        nextPage = true;
                        e.Result = 1;
                    }
                }
            } while (nextPage);
        }

        private void ReadAllProjects()
        {
            _projects.Clear();

            foreach (
                var lnkPath in
                    Directory.EnumerateFiles(_folder.FileName, "*" + Resources.LinkExtension,
                        SearchOption.AllDirectories))
            {
                var dir = new Shell().NameSpace(Path.GetDirectoryName(lnkPath));
                var itm = dir.Items().Item(Path.GetFileName(lnkPath));
                var lnk = (ShellLinkObject) itm.GetLink;
                _projects.Add(new ProjectLink {Url = lnk.Target.Path, Path = lnkPath, Name = lnk.Description});
            }
        }

        private void backgroundWorkerDownload_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var enableGui = true;
            if (e.Result is int)
            {
                var r = (int) e.Result;

                switch (r)
                {
                    case -1:
                        var f = new FormLogin();

                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            backgroundWorkerDownload.RunWorkerAsync();

                            progressBarMain.Value = 10;
                            progressBarMain.Style = ProgressBarStyle.Continuous;

                            enableGui = false;
                        }
                        break;
                }
            }

            if (enableGui)
            {
                // Save last synced folder
                Settings.Default.LastFolder = _folder.FileName;
                Settings.Default.Save();

                // Restore UI
                UpdateGui();
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBoxFile_DoubleClick(object sender, EventArgs e)
        {
            BrowseFolder();
        }

        private void backgroundWorkerDownload_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == -1)
            {
                // Increment the progress bar
                progressBarMain.Value += progressBarMain.Value < 50
                    ? 10
                    : (progressBarMain.Value < 75 ? 5 : (progressBarMain.Value < 95 ? 1 : 0));

                // Create shortcut
                var p = e.UserState as Tuple<string, string>;
                var urlDestination = Resources.UrlHome + "/" + p.Item1;
                var found = false;

                foreach (var project in _projects.Where(project => project.Url == urlDestination))
                {
                    found = true;

                    // Update the filename only, only if needed
                    if (Path.GetFileNameWithoutExtension(project.Path) == p.Item2) continue;

                    string s;
                    File.Move(project.Path, GetValidNewPath(Path.GetDirectoryName(project.Path), p.Item2, out s));
                }

                if (!found)
                {
                    // Create new
                    string lnkName;
                    var lnkPath = GetValidNewPath(_folder.FileName, p.Item2, out lnkName);

                    // Create an empty .lnk file so we can create an object for it
                    File.WriteAllText(lnkPath, string.Empty);

                    // Initialize a ShellLinkObject for that .lnk file
                    var shl = new Shell();
                    var dir = shl.NameSpace(Path.GetDirectoryName(lnkPath));
                    var itm = dir.Items().Item(lnkName);
                    var lnk = (ShellLinkObject) itm.GetLink;
                    lnk.Path = urlDestination;
                    lnk.Description = "Original project name: " + p.Item2;

                    // And dummy an icon (it will use notepad's)
                    lnk.SetIconLocation(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "imageres.dll"), 66);

                    // Done, save it
                    lnk.Save(lnkPath);
                }
            }
            else
                progressBarMain.Value = e.ProgressPercentage;
        }

        /// <summary>
        ///     Cyclically looks for a free filename in an specified folder
        /// </summary>
        /// <param name="destinationFolder">Destination folder for the file</param>
        /// <param name="originalName">Source name</param>
        /// <param name="finalName">New name for the unique file</param>
        /// <returns>Full path of the new file</returns>
        private static string GetValidNewPath(string destinationFolder, string originalName, out string finalName)
        {
            var n = 0;
            string finalPath;

            do
            {
                finalName = originalName + (n++ > 0 ? $" ({n})" : "") + Resources.LinkExtension;
                finalPath = Path.Combine(destinationFolder, finalName);
            } while (File.Exists(finalPath));

            return finalPath;
        }
    }

    /// <summary>
    ///     Represents the data in a project link
    /// </summary>
    internal struct ProjectLink
    {
        public string Url;
        public string Path;
        public string Name;
    }
}