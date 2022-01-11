using System;

namespace Calculator
{
    class Mediator
    {
        public static string Calculate(string input)
        {
            string returns;
            try
            {
                MathOperand mathOperand = StringToMathConverter.Convert(input);
                returns = string.Concat("=", mathOperand.Value);
            }
            catch (Exception x)
            {
                returns = string.Concat(x.Message, "\nPleace, fix the problem and try again.");
            }
            return returns;
        }
    }
}
