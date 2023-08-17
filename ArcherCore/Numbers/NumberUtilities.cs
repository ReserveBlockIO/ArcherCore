using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcherCore.Numbers
{
    public static class NumberUtilities
    {
        /// <summary>
        /// Finds the closest number to the given inputNumber
        /// </summary>
        /// <param name="inputNumber">input long</param>
        /// <param name="numbers">input long[]?</param>
        /// <returns>
        /// Returns a long that is closest to input.
        /// </returns>
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

        /// <summary>
        /// Gets amount of decimal places from the given number
        /// </summary>
        /// <param name="number">input decimal</param>
        /// <returns>
        /// Returns an int count of number of decimal places.
        /// </returns>
        public static int GetNumberOfDecimalPlaces(decimal number)
        {
            var decimalsUsed = BitConverter.GetBytes(decimal.GetBits(number)[3])[2];
            return decimalsUsed;
        }
    }
}
