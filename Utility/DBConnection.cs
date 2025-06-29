using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
namespace FinanceManagement.Utility
{
    static class DBConnection
    {
        private static IConfiguration iConfiguration;

        static DBConnection()
        {
                GetAppSettingsFile();
                
        }
        private static void GetAppSettingsFile()
        {
            var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())   // this is to see the  json file
                        .AddJsonFile("appsettings.json");    //loads the configuration builder file
            iConfiguration = builder.Build();
        }



        public static string GetConnectionString()
        {
            return iConfiguration.GetConnectionString("LocalConnection");
        }

        public static SqlConnection GetConnectionObject()
        {
            SqlConnection sqlConn = new SqlConnection(GetConnectionString());
            return sqlConn;
        }
    }
}
