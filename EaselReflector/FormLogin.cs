using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EaselReflector
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();

            webBrowserMain.Navigate("http://easel.inventables.com/users/sign_in");
            DialogResult = DialogResult.Cancel;
        }

        private void webBrowserMain_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            UseWaitCursor = false;

            var url = e.Url.AbsoluteUri;
            if (url.Contains("easel.inventables") && !url.Contains("sign_in"))
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
