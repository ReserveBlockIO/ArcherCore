using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ArcherCore.Strings
{
    public static class StringUtilities
    {
        /// <summary>
        /// This method will return a bool if a string starts with an uppercase character.
        /// </summary>
        /// <param name="str">input string</param>
        /// <returns>
        /// Returns a boolean (true) if string has uppercase in first character
        /// </returns>
        public static bool StartsWithUpper(string? str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return false;

            char ch = str[0];
            return char.IsUpper(ch);
        }

        /// <summary>
        /// This method will return a bool if a string starts with an lowercase character.
        /// </summary>
        /// <param name="str">input string</param>
        /// <returns>
        /// Returns a boolean (true) if string has lowercase in first character
        /// </returns>
        public static bool StartsWithLower(string? str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return false;

            char ch = str[0];
            return char.IsLower(ch);
        }

        /// <summary>
        /// Creates a secure string from the given input
        /// </summary>
        /// <param name="input">input string</param>
        /// <returns>
        /// Returns a SecureString
        /// </returns>
        public static SecureString GetSecureString(string input)
        {
            var secureStr = new SecureString();
            if (input.Length > 0)
            {
                foreach (var c in input.ToCharArray()) secureStr.AppendChar(c);
            }
            return secureStr;
        }

        /// <summary>
        /// Takes a secure string and converts back to unsecure string
        /// </summary>
        /// <param name="secstrPassword">input SecureString</param>
        /// <returns>
        /// Returns a string
        /// </returns>
        public static string ConvertSecureStringToUnsecureString(SecureString secstrPassword)
        {
            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secstrPassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        /// <summary>
        /// Takes a string and stacks it from start to finish into a IEnumerable
        /// </summary>
        /// <param name="str">input string</param>
        /// <param name="chunkSize">input int</param>
        /// <returns>
        /// Returns a IEnumerable<string>
        /// </returns>
        public static IEnumerable<string> GetStacks(string str, int chunkSize)
        {
            if (String.IsNullOrEmpty(str)) throw new ArgumentException();
            if (chunkSize < 1) throw new ArgumentException();

            for (int i = 0; i < str.Length; i += chunkSize)
            {
                if (chunkSize + i > str.Length)
                    chunkSize = str.Length - i;

                yield return str.Substring(i, chunkSize);
            }
        }

        /// <summary>
        /// Takes a Guid and shortens it
        /// </summary>
        /// <param name="guid">input Guid</param>
        /// <returns>
        /// Returns a string
        /// </returns>
        public static string ShortenGUID(Guid guid)
        {
            var base64 = Convert.ToBase64String(guid.ToByteArray());
            var guidBase64 = base64.Substring(0, base64.Length - 2);

            return guidBase64;
        }

        /// <summary>
        /// Takes a shotened Guid string and converts back to Guid
        /// </summary>
        /// <param name="shortGuid">input string</param>
        /// <returns>
        /// Returns a Guid
        /// </returns>
        public static Guid RestoreGUIDFromShorten(string shortGuid)
        {
            var reconstructedGuid = new Guid(Convert.FromBase64String(shortGuid + "=="));

            return reconstructedGuid;
        }

        /// <summary>
        /// Prints out a Regex Cheat Sheet for easier regex creation
        /// </summary>
        /// <returns>
        /// Returns a string
        /// </returns>
        public static string GetRegexCheatSheet()
        {
            var regexCheatSheet =
@"/****************************************************************************************************
 ^ - Starts with
 $ - Ends with
 [] - Range
 () - Group
 . - Single character once
 + - one or more characters in a row
 ? - optional preceding character match
 \ - escape character
 \n - New line
 \d - Digit
 \D - Non-digit
 \s - White space
 \S - non-white space
 \w - alphanumeric/underscore character (word chars)
 \W - non-word characters
 {x,y} - Repeat low (x) to high (y) (no ""y"" means at least x, no "",y"" means that many)
 (x|y) - Alternative - x or y
 
 [^x] - Anything but x (where x is whatever character you want)
****************************************************************************************************/";

            return regexCheatSheet;
        }
    }
}
