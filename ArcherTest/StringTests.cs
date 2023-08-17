using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace ArcherTest
{
    public class StringTests
    {
        [Test]
        public void StartsWithUpperTest()
        {
            var value = "Hey";
            var result = ArcherCore.Strings.StringUtilities.StartsWithUpper(value);

            if (result)
                Assert.Pass();

            Assert.Fail();
        }

        [Test]
        public void StartsWithUpperTest2()
        {
            var value = "hey";
            var result = ArcherCore.Strings.StringUtilities.StartsWithUpper(value);

            if (!result)
                Assert.Pass();

            Assert.Fail();
        }

        [Test]
        public void StartsWithLowerTest()
        {
            var value = "hey";
            var result = ArcherCore.Strings.StringUtilities.StartsWithLower(value);

            if (result)
                Assert.Pass();

            Assert.Fail();
        }

        [Test]
        public void StartsWithLowerTest2()
        {
            var value = "Hey";
            var result = ArcherCore.Strings.StringUtilities.StartsWithLower(value);

            if (!result)
                Assert.Pass();

            Assert.Fail();
        }

        [Test]
        public void GetSecureStringTest()
        {
            var value = "somestring";
            var secureValue = ArcherCore.Strings.StringUtilities.GetSecureString(value);

            var length = secureValue.Length;
            var unsecureValue = ArcherCore.Strings.StringUtilities.ConvertSecureStringToUnsecureString(secureValue);

            if(length == 10 && unsecureValue == value) 
                Assert.Pass();

            Assert.Fail();
        }

        [Test]
        public void ShortenGUIDTest()
        {
            Guid guidTest = new Guid("f32a1495-d639-4e3c-6457-08d984b2dbdf");
            var result = ArcherCore.Strings.StringUtilities.ShortenGUID(guidTest);

            if (result.Equals("lRQq8znWPE5kVwjZhLLb3w"))
                Assert.Pass();

            Assert.Fail();
        }

        [Test]
        public void RestoreGUIDFromShortenTest()
        {
            Guid guidTest = new Guid("f32a1495-d639-4e3c-6457-08d984b2dbdf");
            var shortenGuid = "lRQq8znWPE5kVwjZhLLb3w";
            var guidUnpacked = ArcherCore.Strings.StringUtilities.RestoreGUIDFromShorten(shortenGuid);

            if(guidUnpacked.Equals(guidTest))
                Assert.Pass();

            Assert.Fail();
        }

        [Test]
        public void GetStacksTest()
        {
            var value = "stacksonstacks";
            var stack = ArcherCore.Strings.StringUtilities.GetStacks(value, 2).ToList();

            bool firstPass = false;
            bool secondPass = false;

            if(stack.Count == 7)
                firstPass = true;

            if (!firstPass)
                Assert.Fail();

            if (stack[0] == "st" && stack[1] == "ac" && stack[2] == "ks")
                secondPass = true;

            if (firstPass && secondPass)
                Assert.Pass();

            Assert.Fail();
        }
    }
}
