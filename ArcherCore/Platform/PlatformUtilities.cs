using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ArcherCore.Platform
{
    public static class PlatformUtilities
    {
        /// <summary>
        /// This method will return what the detected platform is
        /// </summary>
        /// <returns>
        /// Returns a string of 'win', 'mac', or 'linux'
        /// </returns>
        public static string GetPlatform()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return "win";
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return "mac";
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return "linux";
            }

            return "error";
        }
    }
}
