using System;

namespace MathExpression
{
    public static class Calculator
    {
        /// <returns>Value of mathematical expression composed of string.</returns>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="DivideByZeroException"/>
        /// <exception cref="OverflowException"/>
        public static double Calculate(string input)
        {
            MathOperand mathOperand = MathConverter.StringToMathConvert(input);
            double expressionResult = mathOperand.Value;
            return expressionResult;
        }
    }
}
