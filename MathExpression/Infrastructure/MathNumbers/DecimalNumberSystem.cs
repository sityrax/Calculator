using System;

namespace MathExpression.MathNumbers
{
    public class DecimalNumberSystem : IMathNumberSystem
    {
        /// <returns>Number length.</returns>
        public int NumberCheck(string input, int firstIndex, int lastIndex)
        {
            if (lastIndex >= firstIndex)
                switch (input[lastIndex])
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case ',':
                        int length = NumberCheck(input, firstIndex, lastIndex - 1);
                        return ++length;
                    default:
                        return 0;
                }
            else
                return 0;
        }

        /// <exception cref="ArgumentException">Invalid symbol detected</exception>
        public IMathOperand NumberParse(IMathExpression ME, 
                                        IMathOperand MO, 
                                        string input, 
                                        int numberLength, 
                                        int startIndex, 
                                        int endIndex)
        {
            if (numberLength > 0)
            {
                int startIndexExtraction = (endIndex + 1) - numberLength;
                if (startIndexExtraction > startIndex)
                    try
                    {
                        if (ME.ExpressionParse(input, startIndex, startIndexExtraction - 1) is IMathOperand number)
                            throw new ArgumentException($"Extra number '{number.Value}' detected.");
                    }
                    catch (ArgumentNullException) { }
                string subNumber = input.Substring(startIndexExtraction, endIndex - startIndexExtraction + 1);
                return MO.CreateInstance(double.Parse(subNumber));
            }
            throw new ArgumentException($"Invalid symbol '{input[endIndex]}' detected.");
        }
    }
}
