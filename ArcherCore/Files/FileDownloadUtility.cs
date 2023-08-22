using ArcherCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcherCore.Files
{
    public static class FileDownloadUtility
    {
        public static async Task<(bool, string)> DownloadFile(Uri uri, string fileName)
        {
            var _httpClientFactory = HttpVariables.HttpClientFactory;
            try
            {
                // Get HTTP stream
                using (var client = _httpClientFactory.CreateClient())
                {
                    var httpStream = await client.GetStreamAsync(uri);
                    // Get file stream
                    using FileStream fileStream = File.OpenWrite(fileName);

                    // Write HTTP to file
                    httpStream.CopyTo(fileStream);
                }
            }
            catch (Exception e)
            {
                return (false, e.ToString());
            }

            return (true, "");
        }

        public static async Task<(bool, string)> DownloadFile(string url, string fileName)
        {
            var _httpClientFactory = HttpVariables.HttpClientFactory;
            try
            {
                var uri = CreateUri(url);
                // Get HTTP stream
                using (var client = _httpClientFactory.CreateClient())
                {
                    var httpStream = await client.GetStreamAsync(uri);
                    // Get file stream
                    using FileStream fileStream = File.OpenWrite(fileName);

                    // Write HTTP to file
                    httpStream.CopyTo(fileStream);
                }
            }
            catch (Exception e)
            {
                return (false, e.ToString());
            }

            return (true, "");
        }

        public static Uri CreateUri(string url)
        {
            // Create Uri
            UriBuilder uriBuilder = new(url);
            return uriBuilder.Uri;
        }
    }
}
