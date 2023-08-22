using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ArcherCore.WebServer
{
    public static class Server
    {
        public static async Task Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseKestrel(options =>
                   {
                       options.ListenAnyIP(443, listenOption => { listenOption.UseHttps(GetSelfSignedCertificate()); });
                       options.ListenAnyIP(80);
                       options.ListenAnyIP(8080);
                   })
                   .UseStartup<Startup>()
                   //.UseUrls(new string[] {$"http://*:{Globals.APIPort}", $"https://*:{Globals.APIPort}" })
                   .ConfigureLogging(loggingBuilder => loggingBuilder.ClearProviders()); //ClearProviders() - this clears it
               });

            var app = builder.Build();
            _ = app.RunAsync();
        }

        private static X509Certificate2 GetSelfSignedCertificate()
        {
            var password = Guid.NewGuid().ToString();
            var commonName = "ArcherCoreSelfSignedCertAPI";
            var rsaKeySize = 2048;
            var years = 100;
            var hashAlgorithm = HashAlgorithmName.SHA256;

            using (var rsa = RSA.Create(rsaKeySize))
            {
                var request = new CertificateRequest($"cn={commonName}", rsa, hashAlgorithm, RSASignaturePadding.Pkcs1);

                request.CertificateExtensions.Add(
                  new X509KeyUsageExtension(X509KeyUsageFlags.DataEncipherment | X509KeyUsageFlags.KeyEncipherment | X509KeyUsageFlags.DigitalSignature, false)
                );
                request.CertificateExtensions.Add(
                  new X509EnhancedKeyUsageExtension(
                    new OidCollection { new Oid("1.3.6.1.5.5.7.3.1") }, false)
                );

                var certificate = request.CreateSelfSigned(DateTimeOffset.Now.AddDays(-1), DateTimeOffset.Now.AddYears(years));
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    certificate.FriendlyName = commonName;

                // Return the PFX exported version that contains the key
                return new X509Certificate2(certificate.Export(X509ContentType.Pfx, password), password, X509KeyStorageFlags.MachineKeySet);
            }
        }
    }
}
