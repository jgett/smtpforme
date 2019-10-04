using System;
using System.Configuration;
using System.Net;

namespace SmtpForMe
{
    public static class Settings
    {
        public static string GetUserInterfaceUri()
        {
            var result = ConfigurationManager.AppSettings["UserInterfaceUri"];
            var webHost = ConfigurationManager.AppSettings["WebServiceHost"];

            if (result == webHost)
                return result;

            if (webHost != "http://127.0.0.1:4856")
                result = result + "?host=" + WebUtility.UrlEncode(webHost);

            return result;
        }

        public static string GetWebServiceHost() => GetRequiredSettingAsString("WebServiceHost");

        public static string GetSmtpServiceHost() => GetRequiredSettingAsString("SmtpServiceHost");

        public static int GetSmtpServicePort() => GetRequiredSettingAsInt32("SmtpServicePort");

        public static string GetRequiredSettingAsString(string key)
        {
            string value = ConfigurationManager.AppSettings[key];

            if (string.IsNullOrEmpty(value))
                throw new Exception($"Missing required AppSetting: {key}");

            return value;
        }

        public static int GetRequiredSettingAsInt32(string key)
        {
            string value = GetRequiredSettingAsString(key);

            if (!int.TryParse(value, out int result))
                throw new Exception($"{key} must be an integer.");

            return result;
        }
    }
}
