using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ArcherCore.Port
{
    public static class PortUtility
    {
        public static bool IsPortOpen(int port, string ip = "127.0.0.1")
        {
            using (TcpClient tcpClient = new TcpClient())
            {
                try
                {
                    tcpClient.Close();
                    tcpClient.Connect(ip, port);
                    return true;
                }
                catch (Exception)
                {
                    tcpClient.Close();
                    return false;
                }                
            }
        }
    }
}
