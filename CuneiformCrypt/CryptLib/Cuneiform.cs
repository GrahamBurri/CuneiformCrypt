﻿using System;

namespace CryptLib
{
    public class Cuneiform
    {
        // Unicode character codes for Cuneiform numbers follow
        // Babylonian numbering had no zero, and used a blank space to indicate missing place values
        private const int ONE = 0x12415;
        private const int TWO = 0x12416;
        private const int THREE = 0x12417;
        private const int FOUR = 0x12418;
        private const int FIVE = 0x12419;
        private const int SIX = 0x1241A;
        private const int SEVEN = 0x1241B;
        private const int EIGHT = 0x1241C;
        private const int NINE = 0x1241D;

        public static string getCuneiformNumber(int decimalNumber)
        {
            return Char.ConvertFromUtf32(ONE);
        }
    }
}