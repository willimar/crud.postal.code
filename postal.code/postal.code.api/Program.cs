using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mc.cript;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace postal.code.api
{
    public class Program
    {
        public const string AllowSpecificOrigins = "_AllowSpecificOrigins";

        public static IConfiguration Configuration { get; set; }

        public static string DataBaseHost { get { return GetConfig<string>("MongoDb", "Host"); } }
        public static int DataBasePort { get { return GetConfig<int>("MongoDb", "Port"); } }
        internal static string DataBaseUser { get { return Cryptographer.Decrypt(GetConfig<string>("MongoDb", "User"), "fodão"); } }
        internal static string DataBasePws { get { return Cryptographer.Decrypt(GetConfig<string>("MongoDb", "Password"), "fodão"); } }
        public static string DataBaseAuth { get { return GetConfig<string>("MongoDb", "Auth"); } }
        public static string DataBaseName { get { return GetConfig<string>("MongoDb", "DataBase"); } }

        public static TimeZoneInfo TimeZone { get { return TimeZoneInfo.FindSystemTimeZoneById(GetConfig<string>("Program", "TimeZone")); } }
        public static DateTime UtcNow { get { return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZone); } }

        internal static TCast GetConfig<TCast>(string sectionName, string fieldName)
        {
            var section = Configuration.GetSection(sectionName).GetSection(fieldName);

            if (string.IsNullOrWhiteSpace(section.Value))
            {
                return default;
            }
            else
            {
                return (TCast)Convert.ChangeType(section.Value, typeof(TCast));
            }
        }

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
