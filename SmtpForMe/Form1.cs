using SmtpForMe.Properties;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;

namespace SmtpForMe
{
    public partial class Form1 : Form
    {
        public SmtpForMeApplicationContext Context { get; set; }

        public Form1()
        {
            InitializeComponent();
            Icon = Resources.SmtpForMeIcon;
        }

        private string GetUserInterfaceUri()
        {
            var result = ConfigurationManager.AppSettings["UserInterfaceUri"];
            var webHost = ConfigurationManager.AppSettings["WebServiceHost"];

            if (result == webHost)
                return result;

            if (webHost != "http://127.0.0.1:4856")
                result = result + "?host=" + WebUtility.UrlEncode(webHost);

            return result;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LinkWebInterface.Text = GetUserInterfaceUri();
        }

        private void LinkWebInterface_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(GetUserInterfaceUri());
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            Hide();
            Close();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            BtnOK_Click(sender, e);
            Context.Exit(sender, e);
        }
    }
}
