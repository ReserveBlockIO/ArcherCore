using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcherCore.Time
{
    public static class TimeUtilities
    {
        public static long GetUnixTime(int addSeconds = 0)
        {
            return DateTimeOffset.UtcNow.AddSeconds(addSeconds).ToUnixTimeSeconds();
        }

        public static long GetUnixMillisecondTime(int addSeconds = 0)
        {
            return DateTimeOffset.UtcNow.AddSeconds(addSeconds).ToUnixTimeMilliseconds();
        }

        public static long GetUnixTimeFromDateTime(DateTime date)
        {
            return ((DateTimeOffset)date).ToUnixTimeSeconds();
        }
        public static DateTime ToDateTimeFromUnix(long unixTime)
        {
            DateTime frDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            frDateTime = frDateTime.AddSeconds(unixTime).ToLocalTime();
            return frDateTime;
        }
    }
}
