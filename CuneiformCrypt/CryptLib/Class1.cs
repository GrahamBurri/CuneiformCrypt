using System;

namespace CryptLib
{
    public class Cuneiform
    {
        private const int ONE = 0x12415;

        public static string getCuneiformNumber(int decimalNumber)
        {
            return Char.ConvertFromUtf32(ONE);
        }
    }
}
