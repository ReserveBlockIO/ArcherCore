using ArcherCore.Email;
using ArcherCore.Http;
using ArcherCore.Logging;
using ArcherCore.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net.Mail;
using System.Runtime.InteropServices;

namespace ArcherCore
{
    public static class Archer
    {
        public static async Task RunAllSetupWithDefaults(bool skipWebserver = true)
        {
            await SetupHttpClientFactory();
            await SetupMemoryLogger();
            await SetupLogging();
            await ForceUSCulture();
            string[] argsForWebserver = new string[] { };
            if (!skipWebserver)
                await SetupWebServer(argsForWebserver);
        }

        public static async Task SetupWebServer(string[] args)
        {
            _ = WebServer.Server.Main(args); ;
        }

        public static async Task SetupEmail(string host, int port, SmtpDeliveryMethod deliveryMethod, bool enableSsl = true, bool useDefaultCreds = false)
        {
            EmailVariables.Host = host;
            EmailVariables.Port = port;
            EmailVariables.SmtpDeliveryMethod = deliveryMethod;
            EmailVariables.EnableSSL = enableSsl;
            EmailVariables.UseDefaultCredentials = useDefaultCreds;
        }

        public static async Task SetupHttpClientFactory(bool enableLogging = false, string[]? args = null)
        {
            var httpClientBuilder = Host.CreateDefaultBuilder(args)
                     .ConfigureServices(services =>
                     {
                         services.AddHttpClient();
                         services.AddTransient<HttpService>();
                     })
                     .ConfigureLogging(enableLogging ? loggingBuilder => loggingBuilder.AddSimpleConsole() : loggingBuilder => loggingBuilder.ClearProviders())
                     .Build();

            await httpClientBuilder.StartAsync();
            HttpVariables.HttpClientFactory = httpClientBuilder.Services.GetRequiredService<HttpService>().HttpClientFactory();
        }

        public static async Task SetupMemoryLogger(decimal memoryOverloadAmount = 800M)
        {
            await MemoryService.InitializeMemory(memoryOverloadAmount);
            _ = MemoryService.MemoryLoop();
        }

        public static async Task SetupLogging(string? logPath = null, bool? useDb = false)
        {
            var databaseLocation = "Logs";
            var mainFolderPath = "ArcherCore";

            if (logPath == null)
            {
                string path = "";
                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                    path = homeDirectory + Path.DirectorySeparatorChar + mainFolderPath.ToLower() + Path.DirectorySeparatorChar + databaseLocation + Path.DirectorySeparatorChar;
                }
                else
                {
                    if (Debugger.IsAttached)
                    {
                        path = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "Logs" + Path.DirectorySeparatorChar + databaseLocation + Path.DirectorySeparatorChar;
                    }
                    else
                    {
                        path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + Path.DirectorySeparatorChar + mainFolderPath + Path.DirectorySeparatorChar + databaseLocation + Path.DirectorySeparatorChar;
                    }
                }

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                LoggingVariables.LoggingPath = path;
                LoggingVariables.UseDb = useDb.Value;
            }
            else
            {
                if (!Directory.Exists(logPath))
                {
                    Directory.CreateDirectory(logPath);
                }

                LoggingVariables.LoggingPath = logPath;
                LoggingVariables.UseDb = useDb.Value;
            }

            if(useDb.Value)
            {
                await LoggingService.InitializeDb();
            }

            _ = LoggingService.LogLoop();
        }

        public static async Task ForceUSCulture()
        {
            var culture = CultureInfo.GetCultureInfo("en-US");
            if (Thread.CurrentThread.CurrentCulture.Name != "en-US")
            {
                CultureInfo.DefaultThreadCurrentCulture = culture;
                CultureInfo.DefaultThreadCurrentUICulture = culture;

                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }
        }
    }
}