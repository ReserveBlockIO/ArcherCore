using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcherCore.Numbers
{
    public static class NumberUtilities
    {
        public static long FindClosestNumber(long inputNumber, long[]? numbers = null)
        {
            numbers = numbers == null ? new long[] { 1, 2, 4, 6, 8, 12, 16, 24, 32, 48, 64, 128, 256, 512, 1024, 2048, 5096 } : numbers;

            long closestNumber = numbers[0];
            long minimumDifference = Math.Abs(inputNumber - closestNumber);

            foreach (long number in numbers)
            {
                long difference = Math.Abs(inputNumber - number);

                if (difference < minimumDifference)
                {
                    minimumDifference = difference;
                    closestNumber = number;
                }
            }

            return closestNumber;
        }

        public static int GetNumberOfDecimalPlaces(decimal number)
        {
            var decimalsUsed = BitConverter.GetBytes(decimal.GetBits(number)[3])[2];
            return decimalsUsed;
        }
    }
}
