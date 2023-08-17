using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcherTest
{
    public class NumberTest
    {
        [Test]
        public void FindClosestNumberTest()
        {
            var number = 66;
            var result = ArcherCore.Numbers.NumberUtilities.FindClosestNumber(number);

            if(result.Equals(64))
                Assert.Pass();

            Assert.Fail();
        }

        [Test]
        public void GetNumberOfDecimalPlacesTest() 
        {
            var number = 1.1234M;
            var result = ArcherCore.Numbers.NumberUtilities.GetNumberOfDecimalPlaces(number);

            if(result.Equals(4)) 
                Assert.Pass();

            Assert.Fail();
        }
    }
}
