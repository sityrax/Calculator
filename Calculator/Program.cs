using System;
using static System.Console;
using static Calculator.Mediator;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string input = InputAnalysis(args);
                WriteLine(Calculate(input));
            }
        }


        static string InputAnalysis(string[] args)
        {
            if (args.Length == 0)
            {
                return ReadLine();
            }
            else
            {
                return args[0];
            }
        }
    }
}
