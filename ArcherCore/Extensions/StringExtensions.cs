using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ArcherCore.Extensions
{
    public static class StringExtensions
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
        /// <summary>
        /// Converts any string into a URL friendly version
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToFriendlyString(this string str)
        {
            if (string.IsNullOrEmpty(str)) return "";

            str = str.ToLower();

            str = Regex.Replace(str, @"&\w+;", "");
            str = Regex.Replace(str, @"[^a-z0-9\-\s]", "", RegexOptions.IgnoreCase);
            str = str.Replace(" ", "-");
            str = Regex.Replace(str, @"-{2,}", "-");

            return str;
        }
        /// <summary>
        /// Encodes ascii characters and returns string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string EncodeNonAsciiCharacters(this string value)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in value)
            {
                if (c > 127)
                {
                    // This character is too big for ASCII
                    string encodedValue = "\\u" + ((int)c).ToString("x4");
                    sb.Append(encodedValue);
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Decodes ascii characters and returns decoded string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string DecodeEncodedNonAsciiCharacters(this string value)
        {
            return Regex.Replace(
                value,
                @"\\u(?<Value>[a-zA-Z0-9]{4})",
                m =>
                {
                    return ((char)int.Parse(m.Groups["Value"].Value, NumberStyles.HexNumber)).ToString();
                });
        }

        /// <summary>
        /// Cleans string of magic quotes etc.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string CleanString(this string value)
        {
            string clean = value;
            Dictionary<string, string> chars = new Dictionary<string, string>()
            {
                {"’", "'"},
                {"“", "\""},
                {"”", "\""},
                {"–", "&ndash;"},
                {System.Environment.NewLine, " "},
                {"\n", " "},
                {"\r", " "},
            };

            foreach (KeyValuePair<string, string> ch in chars)
                clean = clean.Replace(ch.Key, ch.Value);

            return clean;
        }
        /// <summary>
        /// Parses string and returns a collection of sentences
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static List<string> GetSentences(this string value)
        {
            Regex rx = new Regex(@"(\S.+?[.!?])(?=\s+|$)");
            MatchCollection matches = rx.Matches(value);
            List<string> sentences = new List<string>();

            if (matches.Count > 0)
            {
                for (int i = 0; i < matches.Count; i++)
                {
                    sentences.Add(matches[i].Value);
                }
            }

            return sentences;
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
        public static string ToStringReverse(this string source)
        {
            string strReversed = new string(source.Reverse().ToArray());
            return strReversed;
        }
    }
}
