using SmtpForMe.Properties;
using System;
using System.Configuration;
using System.Diagnostics;
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

        private void Form1_Load(object sender, EventArgs e)
        {
            LinkWebInterface.Text = ConfigurationManager.AppSettings["WebServiceHost"];
        }

        private void LinkWebInterface_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(ConfigurationManager.AppSettings["WebServiceHost"]);
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
