﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ArcherCore.APIs
{
    public static class APIUtilities
    {
        //"https://api.github.com/repos/ReserveBlockIO/ReserveBlock-Core/releases/latest"
        public static async Task<Release?> GetLatestGithubRelease(string url)
        {
            try
            {
                HttpClient client = new HttpClient();
                var productValue = new ProductInfoHeaderValue("RBX-Version-Check", "1.0");

                client.DefaultRequestHeaders.UserAgent.Add(productValue);
                var httpResponse = await client.GetAsync(url);
                var responseString = await httpResponse.Content.ReadAsStringAsync();
                var release = JsonConvert.DeserializeObject<Release>(responseString.ToString());

                return release;
            }
            catch { return null; }
            
        }
        public class Release
        {
            public string url { get; set; }

            public string html_url { get; set; }

            public string assets_url { get; set; }

            public string upload_url { get; set; }

            public int id { get; set; }
            public string node_id { get; set; }

            public string tag_name { get; set; }

            public string target_commitish { get; set; }

            public string name { get; set; }

            public string body { get; set; }

            public bool draft { get; set; }

            public bool prerelease { get; set; }

            public DateTimeOffset created_at { get; set; }

            public DateTimeOffset? published_at { get; set; }

            public Author author { get; set; }

            public string tarball_url { get; set; }

            public string zipball_url { get; set; }

            public IReadOnlyList<ReleaseAsset> assets { get; set; }
        }

        public class ReleaseAsset
        {

            public string url { get; set; }

            public int id { get; set; }
            public string node_id { get; set; }

            public string name { get; set; }

            public string label { get; set; }

            public string State { get; set; }

            public string content_type { get; set; }

            public int size { get; set; }

            public int download_count { get; set; }

            public DateTimeOffset created_at { get; set; }

            public DateTimeOffset updated_at { get; set; }

            public string browser_download_url { get; set; }

            public Author uploader { get; set; }

        }

        public class Author
        {
            public string login { get; set; }

            public int Id { get; set; }

            public string node_id { get; set; }

            public string avatar_url { get; set; }

            public string url { get; set; }

            public string html_url { get; set; }

            public string followers_url { get; set; }

            public string following_url { get; set; }

            public string gists_url { get; set; }

            public string starred_url { get; set; }

            public string subscriptions_url { get; set; }

            public string organizations_url { get; set; }

            public string repos_url { get; set; }

            public string events_url { get; set; }

            public string received_events_url { get; set; }

            public string type { get; set; }

            public bool site_admin { get; set; }

        }
    }
}
