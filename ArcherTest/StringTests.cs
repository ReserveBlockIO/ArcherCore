using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcherTest
{
    public class StringTests
    {
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
    }
}
