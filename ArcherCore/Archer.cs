using ArcherCore.Email;
using ArcherCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Net.Mail;

namespace ArcherCore
{
    public static class Archer
    {
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