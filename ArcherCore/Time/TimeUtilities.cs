using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcherCore.Time
{
    public static class TimeUtilities
    {
        /// <summary>
        /// This method will get a unix timestamp
        /// </summary>
        /// <param name="addSeconds">input int</param>
        /// <returns>
        /// Returns a long unix timestamp
        /// </returns>
        public static long GetUnixTime(int addSeconds = 0)
        {
            return DateTimeOffset.UtcNow.AddSeconds(addSeconds).ToUnixTimeSeconds();
        }

        /// <summary>
        /// This method will get a unix timestamp in Milliseconds
        /// </summary>
        /// <param name="addSeconds">input int</param>
        /// <returns>
        /// Returns a long unix timestamp in Milliseconds
        /// </returns>
        public static long GetUnixMillisecondTime(int addSeconds = 0)
        {
            return DateTimeOffset.UtcNow.AddSeconds(addSeconds).ToUnixTimeMilliseconds();
        }

        /// <summary>
        /// This method will get a unix timestamp from a DateTime
        /// </summary>
        /// <param name="date">input DateTime</param>
        /// <returns>
        /// Returns a long unix timestamp
        /// </returns>
        public static long GetUnixTimeFromDateTime(DateTime date)
        {
            return ((DateTimeOffset)date).ToUnixTimeSeconds();
        }

        /// <summary>
        /// This method will take a unix timestamp and convert to a DateTime
        /// </summary>
        /// <param name="unixTime">input long</param>
        /// <returns>
        /// Returns a DateTime from a long Unix Timestamp
        /// </returns>
        public static DateTime ToDateTimeFromUnix(long unixTime)
        {
            DateTime frDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            frDateTime = frDateTime.AddSeconds(unixTime).ToLocalTime();
            return frDateTime;
        }
    }
}
