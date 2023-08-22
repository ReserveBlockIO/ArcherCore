using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Compression;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ArcherCore.Extensions
{
    public static class GenericExtensions
    {
        
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
