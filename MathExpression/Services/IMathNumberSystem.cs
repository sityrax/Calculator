using System;

namespace MathExpression
{
    public interface IMathNumberSystem
    {
        int NumberCheck(string input, int firstIndex, int lastIndex);
        /// <exception cref="ArgumentException">Invalid symbol detected</exception>
        IMathOperand NumberParse(IMathExpression mathExpression, IMathOperand mathOperand, string input, int numberLength, int startIndex, int endIndex);
    }
}