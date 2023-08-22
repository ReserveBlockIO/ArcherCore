using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcherCore.Dates
{
    public static class DateUtilities
    {
        public static readonly DateTime kExtremeMin = new DateTime(1800, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        public static readonly DateTime kUnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);     // JavaScript epoch.
        public static readonly DateTime kExtremeMax = new DateTime(2179, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);

        public static bool IsExtremeDate(DateTime dt)
        {
            // Is this probably a useless date? NOTE: DateTime is not nullable so use this as null.
            // e.g. Year <= 1
            return dt <= kExtremeMin || dt >= kExtremeMax;
        }
        public static bool IsExtremeDate([NotNullWhen(false)] DateTime? dt)
        {
            if (dt == null)
                return true;
            return IsExtremeDate(dt.Value);
        }
    }
}
