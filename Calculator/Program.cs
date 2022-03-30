using System;
using static System.Console;
using ME = MathExpression;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string input = InputAnalysis(args);
                string calculationResult;
                try
                {
                    calculationResult = ME::Calculator.Calculate(input).ToString();
                }
                catch (Exception x)
                {
                    calculationResult = string.Concat(x.Message, "\nPleace, fix the problem and try again.");
                }
                WriteLine(calculationResult);
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
