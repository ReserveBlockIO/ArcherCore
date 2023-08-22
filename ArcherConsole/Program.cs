// See https://aka.ms/new-console-template for more information

using ArcherCore.Http;
using ArcherCore.Memory;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using static ArcherCore.APIs.APIUtilities;

internal class Program
{
    private static async Task Main(string[] args)
    {
        await ArcherCore.Archer.SetupHttpClientFactory();
        await ArcherCore.Archer.SetupMemoryLogger();
        await ArcherCore.Archer.SetupLogging();
        await ArcherCore.Archer.ForceUSCulture();
        string[] args2 = new string[] { };
        await ArcherCore.Archer.SetupWebServer(args2);

        //var _httpClientFactory = HttpVariables.HttpClientFactory;
        //using(var client = _httpClientFactory.CreateClient())
        //{
        //    var productValue = new ProductInfoHeaderValue("RBX-Version-Check", "1.0");

        //    client.DefaultRequestHeaders.UserAgent.Add(productValue);
        //    var httpResponse = await client.GetAsync("https://api.github.com/repos/ReserveBlockIO/ReserveBlock-Core/releases/latest");
        //    var responseString = await httpResponse.Content.ReadAsStringAsync();
        //    var release = JsonConvert.DeserializeObject<Release>(responseString.ToString());
        //}

        while (true)
        {
            Console.Write("» ");
            var input = Console.ReadLine();
            var isUpper = ArcherCore.Strings.StringUtilities.StartsWithUpper(input);
            var isLower = ArcherCore.Strings.StringUtilities.StartsWithLower(input);
            var upperResult = isUpper ? "Yes" : "No";
            var lowerResult = isLower ? "Yes" : "No";
            Console.WriteLine($"Is Upper? {upperResult}");
            Console.WriteLine($"Is Lower? {lowerResult}");
            if(input =="mem1")
            {
                var info = await MemoryUtilities.GetMemoryInfoJson();
                Console.WriteLine(info);
            }
        }
    }
}