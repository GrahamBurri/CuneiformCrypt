using System;
using CryptLib;

namespace CryptRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Cuneiform c = new Cuneiform();
            Console.WriteLine(Cuneiform.getCuneiformNumber(1));
            Console.ReadKey();
        }
    }
}
