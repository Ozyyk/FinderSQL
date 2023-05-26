using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using server02;

namespace server02
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            int port = configuration.GetValue<int>("AppSettings:Port");
            PortHolder.Port = port; // Uložení hodnoty portu do statické proměnné Port v třídě PortHolder

            CreateHostBuilder(args, configuration).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args, IConfiguration configuration) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<StartUp>();
                    webBuilder.UseConfiguration(configuration);
                    webBuilder.UseUrls($"http://localhost:{PortHolder.Port}"); // Použití hodnoty portu z statické proměnné Port v třídě PortHolder
                });

        public static class PortHolder
        {
            public static int Port { get; set; }
        }

        //public static void Main(string[] args)
        //{
        //    IConfiguration configuration = new ConfigurationBuilder()
        //        .SetBasePath(AppContext.BaseDirectory)
        //        .AddJsonFile("appsettings.json")
        //        .Build();

        //    CreateHostBuilder(args, configuration).Build().Run();
        //}


        //public static IHostBuilder CreateHostBuilder(string[] args, IConfiguration configuration) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<StartUp>();
        //            webBuilder.UseConfiguration(configuration);
        //        });


    }
}