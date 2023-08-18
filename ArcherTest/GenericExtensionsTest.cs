using ArcherCore.Extensions;
using NuGet.Frameworks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcherTest
{
    public class GenericExtensionsTest
    {
        [Test]
        [Ignore("Github Build")]
        public void ToUnixTimeSecondsTest()
        {
            DateTime date = new DateTime(2023, 1, 1,0,0,0, DateTimeKind.Local);
            var unixDate = date.ToUnixTimeSeconds();

            if (unixDate == 1672552800)
                Assert.Pass();

            Assert.Fail();
        }

        [Test]
        public void ToNormalizeDecimalTest()
        {
            var number = 1M;
            var normalizedNumber = number.ToNormalizeDecimal();

            var secondNumber = 1.1234M;
            var secondNormalizedNumber = secondNumber.ToNormalizeDecimal();

            if (normalizedNumber == 1.0M && secondNormalizedNumber == 1.1234M)
                Assert.Pass();

            Assert.Fail();
        }

        [Test]
        [Ignore("Github Build")]
        public void ToLocalDateTimeFromUnixTest()
        {
            long unixTime = 1672552800;
            var localDate = new DateTime(2023, 1, 1,0,0,0, DateTimeKind.Local);

            if (unixTime.ToLocalDateTimeFromUnix() == localDate)
                Assert.Pass();

            Assert.Fail();
        }

        [Test]
        public void ToUTCDateTimeFromUnixTest()
        {
            long unixTime = 1672552800;
            var utcDate = new DateTime(2023, 1, 1, 6, 0, 0);

            if (unixTime.ToUTCDateTimeFromUnix() == utcDate)
                Assert.Pass();

            Assert.Fail();
        }

        [Test]
        public void SecureStringCompare()
        {
            var secure1 = ArcherCore.Strings.StringUtilities.GetSecureString("UnsecureString");
            var secure2 = ArcherCore.Strings.StringUtilities.GetSecureString("UnsecureString");
            var secure3 = ArcherCore.Strings.StringUtilities.GetSecureString("SomeOtherUnsecureString");

            if (!secure1.SecureStringCompare(secure2))
                Assert.Fail();

            if (!secure2.SecureStringCompare(secure1))
                Assert.Fail();

            if (secure1.SecureStringCompare(secure3))
                Assert.Fail();

            if (secure2.SecureStringCompare(secure3))
                Assert.Fail();

            Assert.Pass();
        }

        [Test]
        public void ToUnsecureStringTest()
        {
            var securedString = ArcherCore.Strings.StringUtilities.GetSecureString("UnsecureString");

            var unsecuredString = securedString.ToUnsecureString();

            if (unsecuredString == "UnsecureString")
                Assert.Pass();

            Assert.Fail();
        }

        [Test]
        public void ByteToBase64Test()
        {
            var source = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07 };
            var sourceBase64 = source.ToBase64();

            if (sourceBase64 == "AQIDBAUGBw==")
                Assert.Pass();

            Assert.Fail();
        }

        [Test]
        public void StringToBase64Test()
        {
            var text = "sometext";
            var textBase64 = text.ToBase64();

            if (textBase64 == "c29tZXRleHQ=")
                Assert.Pass();

            Assert.Fail();
        }

        [Test]
        public void ToStringFromBase64Test()
        {
            var textBase64 = "c29tZXRleHQ=";
            var text = textBase64.ToStringFromBase64();

            if (text == "sometext")
                Assert.Pass();

            Assert.Fail();
        }

        [Test]
        public void FromBase64ToByteArrayTest()
        {
            var original = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07 };
            var sourceBase64 = "AQIDBAUGBw==";
            var source = sourceBase64.FromBase64ToByteArray();

            if (source.SequenceEqual(original))
                Assert.Pass();

            Assert.Fail();
        }

        [Test]
        public void ToStringFromArrayTest()
        {
            string[] sourceArray = { "Hey", "Hello", "Hi" };
            var sourceArrayConcat = sourceArray.ToStringFromArray();

            if (sourceArrayConcat == "Hey Hello Hi")
                Assert.Pass();

            Assert.Fail();
        }

        [Test]
        [Ignore("Github Build")]
        public void BytesToCompressTest()
        {
            var source = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07 };
            var compareSource = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 0, 10, 99, 100, 98, 102, 97, 101, 99, 7, 0, 136, 104, 228, 112, 7, 0, 0, 0 };
            var sourceCompressed = source.ToCompress();

            if (sourceCompressed.SequenceEqual(compareSource))
                Assert.Pass();

            Assert.Fail();
        }

        [Test]
        public void BytesToDecompressTest()
        {
            var original = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07 };
            var source = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 0, 10, 99, 100, 98, 102, 97, 101, 99, 7, 0, 136, 104, 228, 112, 7, 0, 0, 0 };
            var decompressed = source.ToDecompress();

            if (decompressed.SequenceEqual(original))
                Assert.Pass();

            Assert.Fail();
        }

        [Test]
        [Ignore("Github Build")]
        public void StringToCompressTest()
        {
            var source = "sometexttocompress";
            var compressed = source.ToCompress();

            if (compressed == "H4sIAAAAAAAACitmyGfIZUhlKAHiCiBZAuQng8UKGIqAYsVACABaPyzTJAAAAA==")
                Assert.Pass();

            Assert.Fail();
        }

        [Test]
        public void StringToDecompressTest()
        {
            var source = "H4sIAAAAAAAACitmyGfIZUhlKAHiCiBZAuQng8UKGIqAYsVACABaPyzTJAAAAA==";
            var original = "sometexttocompress";

            var sourceDecompressed = source.ToDecompress();

            if (sourceDecompressed == original)
                Assert.Pass();

            Assert.Fail();
        }

        [Test]
        public void ToWordCountTest()
        {
            var source = "Some sentence to get a word count from";
            var sourceCount = source.ToWordCount();

            if (sourceCount == 8)
                Assert.Pass();

            Assert.Fail();
        }

        [Test]
        public void ToWordCountCheckTest()
        {
            var source = "Some sentence to get a word count from";
            var sourceCountFail = source.ToWordCountCheck(7);
            var sourceCountPass = source.ToWordCountCheck(8);

            if (!sourceCountFail && sourceCountPass)
                Assert.Pass();

            Assert.Fail();
        }

        [Test]
        public void ToStringReverseTest()
        {
            var source = "AstringtoreversE";
            var sourceReversed = source.ToStringReverse();
            var souceInverseReversed = sourceReversed.ToStringReverse();

            if (sourceReversed == "EsreverotgnirtsA" && souceInverseReversed == "AstringtoreversE")
                Assert.Pass();

            Assert.Fail();
        }
    }
}
