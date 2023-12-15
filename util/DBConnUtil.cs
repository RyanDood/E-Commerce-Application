using Microsoft.Extensions.Configuration;

namespace E_Commerce_Application.Util
{
    internal static class DBConnUtil
    {
        private static IConfiguration iconfiguration;

        static DBConnUtil() {
            getAppSettingsFile();
        }
        private static void getAppSettingsFile()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            iconfiguration = builder.Build();
        }

        public static string getConnectionString()
        {
            return iconfiguration.GetConnectionString("LocalConnectionString");
        }
    }
}
