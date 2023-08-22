using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ArcherCore.Pings
{
    public static class Pings
    {
        public static async Task<bool> PingURL(string url)
        {
            try
            {
                Ping ping = new Ping();

                PingReply pingResult = await ping.SendPingAsync(url);

                if (pingResult.Status.ToString() == "Success")
                {
                    return true;
                }
            }
            catch { }

            return false;
        }
    }
}
