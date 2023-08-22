using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcherCore.Compression
{
    public static class CompressionUtilities
    {
        /// <summary>
        /// Compresses a Byte Array using GZip
        /// </summary>
        /// <param name="bytes">input byte[]</param>
        /// <returns>
        /// Returns a byte[]?
        /// </returns>
        public static byte[] Compress(byte[] bytes)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var gzipStream = new GZipStream(memoryStream, CompressionLevel.Optimal))
                {
                    gzipStream.Write(bytes, 0, bytes.Length);
                }
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Decompresses a Byte Array using GZip
        /// </summary>
        /// <param name="bytes">input byte[]</param>
        /// <returns>
        /// Returns a byte[]?
        /// </returns>
        public static byte[] Decompress(byte[] bytes)
        {
            using (var memoryStream = new MemoryStream(bytes))
            {

                using (var outputStream = new MemoryStream())
                {
                    using (var decompressStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                    {
                        decompressStream.CopyTo(outputStream);
                    }
                    return outputStream.ToArray();
                }
            }
        }

        /// <summary>
        /// Compresses a string using GZip
        /// </summary>
        /// <param name="s">input string</param>
        /// <returns>
        /// Returns a compressed string?
        /// </returns>
        public static string Compress(string s)
        {
            var bytes = Encoding.Unicode.GetBytes(s);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    msi.CopyTo(gs);
                }
                return Convert.ToBase64String(mso.ToArray());
            }
        }

        /// <summary>
        /// Decompresses a string using GZip
        /// </summary>
        /// <param name="s">input string</param>
        /// <returns>
        /// Returns a compressed string?
        /// </returns>
        public static string Decompress(string s)
        {
            var bytes = Convert.FromBase64String(s);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    gs.CopyTo(mso);
                }
                return Encoding.Unicode.GetString(mso.ToArray());
            }
        }
    }
}
