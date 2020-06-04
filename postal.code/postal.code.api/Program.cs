using easy.crypt;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace postal.code.api
{
    public class Program
    {
        public const string AllowSpecificOrigins = "_AllowSpecificOrigins";

        private static object _lock;

        public static IConfiguration Configuration { get; set; }

        public static string DataBaseHost { get { lock (_lock) { return GetConfig<string>("MongoDb", "Host"); } } }
        public static int DataBasePort { get { lock (_lock) { return GetConfig<int>("MongoDb", "Port"); } } }
        internal static string DataBaseUser { get { lock (_lock) {
                    var value = GetConfig<string>("MongoDb", "User");
                    Console.WriteLine($"User: {value}");
                    var user = Cryptographer.Decrypt(value, "fod�o");
                    Console.WriteLine($"User: {user}");
                    return "atlasUser";/*Cryptographer.Decrypt(GetConfig<string>("MongoDb", "User"), "fod�o");*/ } } }
        internal static string DataBasePws { get { lock (_lock) {
                    var value = GetConfig<string>("MongoDb", "Password");
                    Console.WriteLine($"User: {value}");
                    var user = Cryptographer.Decrypt(value, "fod�o");
                    Console.WriteLine($"Password: {user}");
                    return "itsgallus"; /*Cryptographer.Decrypt(GetConfig<string>("MongoDb", "Password"), "fod�o");*/ } } }
        public static string DataBaseAuth { get { lock (_lock) { return GetConfig<string>("MongoDb", "Auth"); } } }
        public static string DataBaseName { get { lock (_lock) { return GetConfig<string>("MongoDb", "DataBase"); } } }

        public static TimeZoneInfo TimeZone { get { lock (_lock) { return TimeZoneInfo.FindSystemTimeZoneById(GetConfig<string>("Program", "TimeZone")); } } }
        public static DateTime UtcNow { get { lock (_lock) { return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZone); } } }

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
            _lock = new { id = Guid.NewGuid() };
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
