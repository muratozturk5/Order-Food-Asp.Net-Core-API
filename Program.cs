using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OrderFoodAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var isService = false;
            if (Debugger.IsAttached == false && args.Contains("--service"))
            {
                isService = true;
            }
            if (isService)
            {
                var pathToContentRoot = Directory.GetCurrentDirectory();
                var webHostargs = args.Where(args => args != "--console").ToArray();
                string ConfigurationFile = "appsettings.json";
                string PortNo = "9009";
                var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
                pathToContentRoot = Path.GetDirectoryName(pathToExe);
                string AppJsonFilePath = Path.Combine(pathToContentRoot, ConfigurationFile);
                if (File.Exists(AppJsonFilePath))
                {
                    using (StreamReader sr = new StreamReader(AppJsonFilePath))
                    {
                        string jsonData = sr.ReadToEnd();
                        JObject jObject = JObject.Parse(jsonData);
                        if (jObject["ServicePort"] != null)
                        {
                            PortNo = jObject["ServicePort"].ToString();
                        }
                    }
                }
                var configuration = new ConfigurationBuilder()
                 .AddCommandLine(args)
                 .Build();





                //var host = WebHost.CreateDefaultBuilder(webHostargs)
                //                  .UseKestrel()
                //                .UseContentRoot(pathToContentRoot)
                //                .UseStartup<Startup>()
                //                .UseIISIntegration()
                //             .UseUrls("http://localhost:9009", "http://odin:9009")
                //                .UseConfiguration(configuration)
                //                .Build();
                //host.RunAsService();

                var host = WebHost.CreateDefaultBuilder(webHostargs)
                                   .UseKestrel()
                                 .UseContentRoot(pathToContentRoot)
                                 .UseStartup<Startup>()
                                 .UseIISIntegration()
                              .UseUrls("http://localhost:9009", "http://odin:9009")
                                 .UseConfiguration(configuration)
                                 .Build();
                host.RunAsService();





            }
            else
            {
                CreateHostBuilder(args).Build().Run();

            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
             Host.CreateDefaultBuilder(args)
                 .ConfigureWebHostDefaults(webBuilder =>
                 {
                     webBuilder.UseStartup<Startup>();

                 });
    }
}
