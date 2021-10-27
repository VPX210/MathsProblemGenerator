using System;
using System.Collections.Generic;
using System.Text;

namespace MathsProblemGenerator
{
    public class ConsoleHelper
    {
        public static int ReadValue(int defaultVal)
        {
            try
            {
                return int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine($"Using default value:{defaultVal}");
                return defaultVal;
            }
        }

        public static int AskValue(string ask, int min, int max, int defaultVal)
        {
            Console.Write($"{ask} :");
            var result = ReadValue(defaultVal);
            if (result < min)
                result = min;
            if (result > max)
                result = max;
            return result;
        }
    }
}
