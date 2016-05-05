using System.Windows.Forms;
using EaselReflector.Properties;

namespace EaselReflector
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();

            webBrowserMain.Navigate(Resources.UrlHome + "/users/sign_in");
            DialogResult = DialogResult.Cancel;
        }

        private void webBrowserMain_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            UseWaitCursor = false;

            var url = e.Url.AbsoluteUri;
            if (url.Contains(Resources.UrlHome) && !url.Contains("sign_in"))
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void webBrowserMain_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            UseWaitCursor = true;
        }
    }
}