using System;
using System.Drawing;

namespace CryptLib
{
    public class Cuneiform
    {
        // Unicode character codes for Cuneiform numbers follow
        // Babylonian numbering had no zero, and used a blank space to indicate missing place values, we'll use a _

        private const int ZERO = 0x005F;
        private const int ONE = 0x12415;
        private const int TWO = 0x12416;
        private const int THREE = 0x12417;
        private const int FOUR = 0x12418;
        private const int FIVE = 0x12419;
        private const int SIX = 0x1241A;
        private const int SEVEN = 0x1241B;
        private const int EIGHT = 0x1241C;
        private const int NINE = 0x1241D;

        private const int TEN = 0x1241E; // I'm pretty sure that these "geshu" characters are the correct ones
        private const int TWENTY = 0x1241F;
        private const int THIRTY = 0x12420;
        private const int FORTY = 0x12421;
        private const int FIFTY = 0x12422;

        private static int[] NUMS = new int[] { ZERO, ONE, TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, NINE }; // Can't be constant?
        private static int[] TENS = new int[] { 0, TEN, TWENTY, THIRTY, FORTY, FIFTY };

        public static string getCuneiformNumber(int decimalNumber)
        {
            int[] sixties = new int[5] { 0, 0, 0, 0, 0 };

            for(int n = 4; n > 0; n--)
            {
                while((decimalNumber - Math.Pow(60, n) > -1))
                {
                    decimalNumber -= (int)(Math.Pow(60, n));
                    sixties[n] += 1;
                }
            }

            String firstPart = "";
            for(int i = 4; i > 0; i--)
            {
                firstPart += convertDecimal(sixties[i]) + "|";
            }
            return firstPart + convertDecimal(decimalNumber);
        }
        private static String convertDecimal(int decimalNumber)
        {
            // We'll need to limit values to 5 places base-60 to avoid running up against the integer limit for now
            int oldDecimal = decimalNumber;
            int tensCount = 0; // this value should never be zero

            if (decimalNumber < 10)
            {
                return Char.ConvertFromUtf32(NUMS[decimalNumber]);
            }
            while (decimalNumber - 10 > -1)
            {
                decimalNumber -= 10;
                tensCount += 1;
            }

            return (Char.ConvertFromUtf32(TENS[tensCount]) + convertDecimal(decimalNumber));
        }
    }
}
