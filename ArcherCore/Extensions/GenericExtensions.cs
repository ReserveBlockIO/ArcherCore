using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ArcherCore.Extensions
{
    public static class GenericExtensions
    {
        /// <summary>
        /// Takes a file path string and returns its extension
        /// </summary>
        /// <param name="source">string</param>
        /// <returns>string extensions. Ex: '.txt'</returns>
        public static string ToFileExtension(this string source)
        {
            string myFilePath = source;
            string ext = Path.GetExtension(myFilePath);
            return ext;
        }
        public static byte[] ImageToByteArray(this byte[] imageBytes)
        {
            byte[] byteArray;
            using (MemoryStream stream = new MemoryStream(imageBytes))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    byteArray = reader.ReadBytes((int)stream.Length);
                }
            }
            return byteArray;
        }

        public static long ToUnixTimeSeconds(this DateTime obj)
        {
            long unixTime = ((DateTimeOffset)obj).ToUnixTimeSeconds();
            return unixTime;
        }

        public static decimal ToNormalizeDecimal(this decimal value)
        {
            var amountCheck = value % 1 == 0;
            var amountFormat = 0M;
            if (amountCheck)
            {
                var amountStr = value.ToString("0.0");
                amountFormat = decimal.Parse(amountStr);

                return amountFormat;
            }

            return value;
        }

        public static DateTime ToLocalDateTimeFromUnix(this long unixTime)
        {
            DateTime frDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            frDateTime = frDateTime.AddSeconds(unixTime).ToLocalTime();
            return frDateTime;
        }
        public static DateTime ToUTCDateTimeFromUnix(this long unixTime)
        {
            DateTime frDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            frDateTime = frDateTime.AddSeconds(unixTime).ToUniversalTime();
            return frDateTime;
        }
        public static bool SecureStringCompare(this SecureString s1, SecureString s2)
        {
            if (s1 == null)
            {
                return false;
            }
            if (s2 == null)
            {
                return false;
            }

            if (s1.Length != s2.Length)
            {
                return false;
            }

            IntPtr ss_bstr1_ptr = IntPtr.Zero;
            IntPtr ss_bstr2_ptr = IntPtr.Zero;

            try
            {
                ss_bstr1_ptr = Marshal.SecureStringToBSTR(s1);
                ss_bstr2_ptr = Marshal.SecureStringToBSTR(s2);

                String str1 = Marshal.PtrToStringBSTR(ss_bstr1_ptr);
                String str2 = Marshal.PtrToStringBSTR(ss_bstr2_ptr);

                return str1.Equals(str2);
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (ss_bstr1_ptr != IntPtr.Zero)
                {
                    Marshal.ZeroFreeBSTR(ss_bstr1_ptr);
                }

                if (ss_bstr2_ptr != IntPtr.Zero)
                {
                    Marshal.ZeroFreeBSTR(ss_bstr2_ptr);
                }
            }
        }

        public static string ToUnsecureString(this SecureString source)
        {
            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(source);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        public static string ToBase64(this byte[] buffer)
        {
            var byteToBase64 = Convert.ToBase64String(buffer);

            return byteToBase64;
        }

        public static string ToBase64(this string source)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(source);
            var stringToBase64 = Convert.ToBase64String(plainTextBytes);

            return stringToBase64;
        }

        public static string ToStringFromBase64(this string source)
        {
            var base64EncodedString = Convert.FromBase64String(source);
            var stringFromBase64 = Encoding.UTF8.GetString(base64EncodedString);

            return stringFromBase64;
        }
        public static byte[] FromBase64ToByteArray(this string base64String)
        {
            var byteArrayFromBase64 = Convert.FromBase64String(base64String);

            return byteArrayFromBase64;
        }

        /// <summary>
        /// Converts a string array into a space delimited string
        /// </summary>
        /// <returns>Space delimited string</returns>
        public static string ToStringFromArray(this string[] source)
        {
            var output = string.Join(" ", source);

            return output;
        }

        public static byte[] ToCompress(this byte[] bytes)
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
        public static byte[] ToDecompress(this byte[] s)
        {
            var bytes = s;
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    gs.CopyTo(mso);
                }
                return mso.ToArray();
            }
        }
        public static string ToCompress(this string s)
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

        public static string ToDecompress(this string s)
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
        public static int ToWordCount(this string text)
        {
            int wordCount = 0, index = 0;

            // skip whitespace until first word
            while (index < text.Length && char.IsWhiteSpace(text[index]))
                index++;

            while (index < text.Length)
            {
                // check if current char is part of a word
                while (index < text.Length && !char.IsWhiteSpace(text[index]))
                    index++;

                wordCount++;

                // skip whitespace until next word
                while (index < text.Length && char.IsWhiteSpace(text[index]))
                    index++;
            }

            return wordCount;
        }
        public static bool ToWordCountCheck(this string text, int count)
        {
            int wordCount = 0, index = 0;

            // skip whitespace until first word
            while (index < text.Length && char.IsWhiteSpace(text[index]))
                index++;

            while (index < text.Length)
            {
                // check if current char is part of a word
                while (index < text.Length && !char.IsWhiteSpace(text[index]))
                    index++;

                wordCount++;

                // skip whitespace until next word
                while (index < text.Length && char.IsWhiteSpace(text[index]))
                    index++;

                if (wordCount > count)
                    break;
            }

            if (wordCount > count)
                return false;
            return true;
        }

        
        public static void ToShuffle<T>(this IList<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static string ToStringReverse(this string source)
        {
            string strReversed = new string(source.Reverse().ToArray());
            return strReversed;
        }

        public static T[] ToLinkedListReverse<T>(this LinkedList<T> source)
        {
            var head = source.First;

            while (head.Next != null)
            {
                var next = head.Next;
                source.Remove(next);
                source.AddFirst(next.Value);
            }

            return source.ToArray();
        }
    }
}
