using System;

namespace Calculator
{
    class Mediator
    {
        public static string Calculate(string input)
        {
            string result;
            try
            {
                MathOperand mathOperand = StringToMathConverter.Convert(input);
                result = string.Concat("= ", mathOperand.Value);
            }
            catch (Exception x)
            {
                result = string.Concat(x.Message, "\nPleace, fix the problem and try again.");
            }
            return result;
        }
    }
}
