using System;
using System.Threading;
using System.Windows.Forms;

namespace SmtpForMe
{
    static class Program
    {
        private static Mutex mutex = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string appName = "SmtpForMe";
            bool createdNew;

            mutex = new Mutex(true, appName, out createdNew);

            if (!createdNew)
            {
                //app is already running! Exiting the application  
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                Application.Run(new SmtpForMeApplicationContext());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Startup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
