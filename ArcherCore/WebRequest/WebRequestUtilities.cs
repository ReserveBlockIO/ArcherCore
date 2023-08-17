using ArcherCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace ArcherCore.WebRequest
{
    public static class WebRequestUtilities
    {
        public static async Task<string> GetWebRequest(string url)
        {
            try
            {
                var _httpClientFactory = HttpVariables.HttpClientFactory;
                using (var client = _httpClientFactory.CreateClient())
                {
                    var httpResponse = await client.GetAsync(url);
                    var responseString = await httpResponse.Content.ReadAsStringAsync();

                    return responseString;
                }
            }
            catch (Exception ex) { return ex.ToString(); }
            
        }
        public static async Task<string> GetWebRequest(string url, Dictionary<string, string> headers)
        {
            try
            {
                var _httpClientFactory = HttpVariables.HttpClientFactory;
                using (var client = _httpClientFactory.CreateClient())
                {
                    
                    foreach (var header in headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                    
                    var httpResponse = await client.GetAsync(url);
                    var responseString = await httpResponse.Content.ReadAsStringAsync();

                    return responseString;
                }
            }
            catch (Exception ex) { return ex.ToString(); }
        }
        public static async Task<string> GetWebRequest(string url, Dictionary<string, IEnumerable<string?>> headerList)
        {
            try
            {
                var _httpClientFactory = HttpVariables.HttpClientFactory;
                using (var client = _httpClientFactory.CreateClient())
                {
                    
                    foreach (var header in headerList)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                    
                    var httpResponse = await client.GetAsync(url);
                    var responseString = await httpResponse.Content.ReadAsStringAsync();

                    return responseString;
                }
            }
            catch (Exception ex) { return ex.ToString(); }
        }

        public static async Task<string> GetWebRequest(string url, Dictionary<string, string>? headers = null, Dictionary<string, IEnumerable<string?>>? headerList = null)
        {
            try
            {
                var _httpClientFactory = HttpVariables.HttpClientFactory;
                using (var client = _httpClientFactory.CreateClient())
                {
                    if (headers != null)
                    {
                        foreach (var header in headers)
                        {
                            client.DefaultRequestHeaders.Add(header.Key, header.Value);
                        }
                    }

                    if (headerList != null)
                    {
                        foreach (var header in headerList)
                        {
                            client.DefaultRequestHeaders.Add(header.Key, header.Value);
                        }
                    }
                    var httpResponse = await client.GetAsync(url);
                    var responseString = await httpResponse.Content.ReadAsStringAsync();

                    return responseString;
                }
            }
            catch(Exception ex) { return ex.ToString(); }
        }

        public static async Task<(bool, string)> PostWebRequest(string endpoint, string content, Encoding? encodingType = null, string? mediaType = "application/json")
        {
            try
            {
                var encoding = encodingType == null ? Encoding.UTF8 : encodingType;
                var _httpClientFactory = HttpVariables.HttpClientFactory;

                if (_httpClientFactory == null)
                    return (false, "HttpClientFactory was null. Please be sure 'await ArcherCore.Archer.SetupHttpClientFactory();' was ran at the startup of your application.");

                using (var client = _httpClientFactory.CreateClient())
                {
                    //Encoding.UTF8 usually good.
                    //mediaType = "application/json" usually good
                    var httpContent = new StringContent(content, encoding, mediaType);
                    using (var Response = await client.PostAsync(endpoint, httpContent))
                    {
                        if (Response.StatusCode == HttpStatusCode.OK)
                        {
                            var response = await Response.Content.ReadAsStringAsync();
                            return (true, response);
                        }
                        else
                        {
                            return(false, $"Failed with status code: {Response.StatusCode}");
                        }
                    }
                }
            }
            catch (Exception ex) { return (false, ex.ToString()); }
        }

    }
}
