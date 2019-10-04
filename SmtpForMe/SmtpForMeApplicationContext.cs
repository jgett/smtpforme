using Microsoft.Owin.Hosting;
using SmtpForMe.Properties;
using SmtpServer;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmtpForMe
{
    public class SmtpForMeApplicationContext : ApplicationContext
    {
        private NotifyIcon _trayIcon;

        private IDisposable _webapp;

        public SmtpForMeApplicationContext()
        {
            // Initialize Tray Icon
            _trayIcon = new NotifyIcon()
            {
                Icon = Resources.SmtpForMeIcon,
                ContextMenu = new ContextMenu(new MenuItem[]
                {
                    new MenuItem("About", About),
                    new MenuItem("Exit", Exit)
                }),
                Visible = true,
                Text = "SmtpForMe"
            };

            _trayIcon.MouseClick += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    Process.Start(Settings.GetUserInterfaceUri());
                }
            };

            var webServiceHost = Settings.GetWebServiceHost();
            var smtpServiceHost = Settings.GetSmtpServiceHost();
            var smtpServicePort = Settings.GetSmtpServicePort();

            _webapp = WebApp.Start<Startup>(webServiceHost);

            Task.Run(async () =>
            {
                await StartSmtpServer(smtpServiceHost, smtpServicePort);
            });
        }

        private async Task StartSmtpServer(string host, int port)
        {
            var options = new SmtpServerOptionsBuilder()
                .ServerName(host)
                .Port(port)
                .MessageStore(new SmtpForMeMessageStore())
                .Build();

            var smtpServer = new SmtpServer.SmtpServer(options);
            await smtpServer.StartAsync(CancellationToken.None);
        }


        public void About(object sender, EventArgs e)
        {
            var form1 = new Form1() { Context = this };
            form1.Show();
        }

        public void Exit(object sender, EventArgs e)
        {
            // Hide tray icon, otherwise it will remain shown until user mouses over it
            _trayIcon.Visible = false;
            Application.Exit();
        }
    }
}
