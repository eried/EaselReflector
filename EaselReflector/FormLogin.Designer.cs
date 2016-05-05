namespace EaselReflector
{
    partial class FormLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogin));
            this.webBrowserMain = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // webBrowserMain
            // 
            this.webBrowserMain.AllowWebBrowserDrop = false;
            this.webBrowserMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserMain.IsWebBrowserContextMenuEnabled = false;
            this.webBrowserMain.Location = new System.Drawing.Point(0, 0);
            this.webBrowserMain.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserMain.Name = "webBrowserMain";
            this.webBrowserMain.ScriptErrorsSuppressed = true;
            this.webBrowserMain.ScrollBarsEnabled = false;
            this.webBrowserMain.Size = new System.Drawing.Size(984, 661);
            this.webBrowserMain.TabIndex = 0;
            this.webBrowserMain.WebBrowserShortcutsEnabled = false;
            this.webBrowserMain.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowserMain_Navigated);
            this.webBrowserMain.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.webBrowserMain_Navigating);
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.webBrowserMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "FormLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowserMain;
    }
}