using Microsoft.Owin.Hosting;
using SmtpForMe.Properties;
using SmtpServer;
using System;
using System.Configuration;
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
                    new MenuItem("Exit", Exit)
                }),
                Visible = true,
                Text = "SmtpForMe"
            };

            _trayIcon.MouseClick += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    var form1 = new Form1() { Context = this };
                    form1.Show();
                }
            };

            var webServiceHost = GetRequiredSettingAsString("WebServiceHost");
            var smtpServiceHost = GetRequiredSettingAsString("SmtpServiceHost");
            var smtpServicePort = GetRequiredSettingAsInt32("SmtpServicePort");

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

        public void Exit(object sender, EventArgs e)
        {
            // Hide tray icon, otherwise it will remain shown until user mouses over it
            _trayIcon.Visible = false;
            Application.Exit();
        }

        private string GetRequiredSettingAsString(string key)
        {
            string value = ConfigurationManager.AppSettings[key];

            if (string.IsNullOrEmpty(value))
                throw new Exception($"Missing required AppSetting: {key}");

            return value;
        }

        private int GetRequiredSettingAsInt32(string key)
        {
            string value = GetRequiredSettingAsString(key);

            if (!int.TryParse(value, out int result))
                throw new Exception($"{key} must be an integer.");

            return result;
        }
    }
}
