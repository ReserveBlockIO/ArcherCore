using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ArcherCore.IP
{
    public static class IPUtility
    {
        public static async Task<string?> GetExternalIP(string? externalIPToolUrl = "http://checkip.dyndns.org/")
        {
            try
            {
                string externalIP;
                externalIP = (new WebClient()).DownloadString(externalIPToolUrl);
                externalIP = (new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}"))
                             .Matches(externalIP)[0].ToString();

                return externalIP;
            }
            catch { return null; }
            
        }

        public static string? GetLocalIPAddress()
        {
            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
            }
            catch { return null; }

            return null;
        }
    }
}
