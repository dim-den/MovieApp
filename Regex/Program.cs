using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(PowerSumDigTerm(1));
        }

        public static long PowerSumDigTerm(int n)
        {
            int count = 0;
            for (int i = 2; ; i++)
            {
                if((i * i).ToString().Sum(x => int.Parse(x.ToString())) == Math.Pow(i, (i*i).ToString().Length))
                {
                    if (++count == n)
                        return i;
                }
            }
        }
    }
}
