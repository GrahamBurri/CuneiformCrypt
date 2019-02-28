using System;
using System.Drawing;
using System.IO;

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
        private const int FOUR = 0x12418; //1243C variant Limmu
        private const int FIVE = 0x12419;
        private const int SIX = 0x1241A;
        private const int SEVEN = 0x1241B; // 12442 variant
        private const int EIGHT = 0x1241C;
        private const int NINE = 0x1241D;

        // "Geshu" variants- I can't find what these are supposed to be used for, but I'll leave them here for legacy purposes
        //private const int TEN = 0x1241E;
        //private const int TWENTY = 0x1241F;
        //private const int THIRTY = 0x12420;
        //private const int FORTY = 0x12421;
        //private const int FIFTY = 0x12422;

        // Babylonian U. For some reason, unicode contains U characters up to 9, but we only need up to 5.
        private const int TEN = 0x1230B;
        private const int TWENTY = 0x12471; // 0x12399; // Apparently MS hasn't updated Segoe UI Historic since 12399 was introduced, so we'll need to find an alternative
        private const int THIRTY = 0x1230D;
        private const int FORTY = 0x1240F; // 12469 variant
        private const int FIFTY = 0x12410; // 1246A variant

        private static int[] NUMS = new int[] { ZERO, ONE, TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, NINE }; // Can't be constant?
        private static int[] TENS = new int[] { 0, TEN, TWENTY, THIRTY, FORTY, FIFTY };

        // Not entirely sure how to get a larger address size on the ints here. Investigation is needed
        // Efficency can probably be improved here

        /// <summary>
        /// Given an integer, return the corresponding cuneiform numeral as a string.
        /// </summary>
        /// <param name="decimalNumber"></param>
        /// <returns></returns>
        public static string getCuneiformNumber(Int64 decimalNumber)
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
        private static String convertDecimal(Int64 decimalNumber)
        {
            // We'll need to limit values to 5 places base-60 to avoid running up against the integer limit for now
            Int64 oldDecimal = decimalNumber;
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

        internal static Queue<int> stringToIntegerQueue(string s)
        {
            var acc = new Queue<int>();

            foreach (char c in s)
            {
                acc.Enqueue((int)c);
            }

            return acc;
        }

        internal static String processIntegerQueue(Queue<int> queue)
        {
            string acc = String.Empty;

            while (queue.ItemsRemaining > 0)
            {
                acc += getCuneiformNumber(queue.Dequeue());
            }

            return acc;
        }

        public static void fileToCuneiform(string from, string to)
        {
            string contents = File.ReadAllText(from);
            Queue<int> queue = stringToIntegerQueue(contents);
            string cuneiform = processIntegerQueue(queue);
            File.WriteAllText(to, cuneiform);
        }

        // Doesn't work
        // Actually might work but idk
        /*
        internal static Queue<Int64> bytesToInt64Queue(byte[] bytes)
        {
            var acc = new Queue<Int64>();
            var byteQueue = new Queue<byte>(bytes);
            int extra = byteQueue.ItemsRemaining % 8; // How many extra bytes do we have?

            byte[] header = new byte[8];

            if (extra > 0)
            {
                for (int i = 0; i < extra; i++)
                {
                    // Populate the end of the array with the bytes
                    header[7 - extra + i] = byteQueue.Dequeue();
                }
                // Queue up those numbers for processing
                acc.Enqueue(BitConverter.ToInt64(header, 0));
            }

            while(byteQueue.ItemsRemaining > 0)
            {
                byte[] bytesToProcess = new byte[8];
                for (int i = 0; i < 8; i++)
                {
                    // Put 8 bytes from queue into array for conversion
                    bytesToProcess[i] = byteQueue.Dequeue();
                }
                // Queue up those numbers for processing
                acc.Enqueue(BitConverter.ToInt64(bytesToProcess, 0));
            }

            return acc;
        }
        */

        // Requires prior path validation
        // Also doesn't seem to work
        /*
        public static void fileToCuneiform(string from, string to)
        {
            byte[] contents = File.ReadAllBytes(from);
            Queue<Int64> queue = bytesToInt64Queue(contents);
            string cuneiform = processIntegerQueue(queue);
            File.WriteAllText(to, cuneiform);
        }
        */
    }
}
