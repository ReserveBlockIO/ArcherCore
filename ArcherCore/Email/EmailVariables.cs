using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ArcherCore.Email
{
    public static class EmailVariables
    {
        public static string Host = "";
        public static int Port = 0;
        public static SmtpDeliveryMethod SmtpDeliveryMethod = SmtpDeliveryMethod.Network;
        public static bool EnableSSL = true;
        public static bool UseDefaultCredentials = false;
    }
}
