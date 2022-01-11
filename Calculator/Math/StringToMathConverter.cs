using System;
using System.Diagnostics;

namespace Calculator
{
    public class StringToMathConverter
    {
        /// <returns>Instance of MathOperand composed of input string.</returns>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="DivideByZeroException"/>
        public static MathOperand Convert(string input)
        {
            BracketsCheck(input);
            return ExpressionParse(input, 0, input.Length - 1);
        }


        /// <returns>Instance of MathOperand composed of input string.</returns>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="DivideByZeroException"/>
        static MathOperand ExpressionParse(string input, int startIndex, int endIndex)
        {
            int maxPriority = 3;

            for (int operationPriority = 0; operationPriority <= maxPriority; operationPriority++)
            {
                for (int currentIndex = endIndex; currentIndex >= startIndex; currentIndex--)
                {
                    if (input[currentIndex] == ' ')
                        continue;
                    switch (operationPriority)
                    {
                        case 0:
                            switch (input[currentIndex])
                            {
                                case '+':
                                    return new Addition(ExpressionParse(input, startIndex, currentIndex - 1), ExpressionParse(input, currentIndex + 1, endIndex));
                                case '-':
                                    return new Subtraction(ExpressionParse(input, startIndex, currentIndex - 1), ExpressionParse(input, currentIndex + 1, endIndex));
                            }
                            break;
                        case 1:
                            switch (input[currentIndex])
                            {
                                case '*':
                                    return new Multiplication(ExpressionParse(input, startIndex, currentIndex - 1), ExpressionParse(input, currentIndex + 1, endIndex));
                                case '/':
                                    return new Division(ExpressionParse(input, startIndex, currentIndex - 1), ExpressionParse(input, currentIndex + 1, endIndex));
                            }
                            break;
                        case 2:
                            if (input[currentIndex] == ')')
                                return BracketsExpression(input, currentIndex);
                            break;

                        default:
                                int numberLength = NumberParse(input, currentIndex);
                            if (numberLength > 0)
                            {
                                int startIndexExtraction = (currentIndex + 1) - numberLength;
                                if (startIndexExtraction > startIndex)
                                    try
                                    {
                                        ExpressionParse(input, startIndex, startIndexExtraction - 1);
                                    }
                                    catch (ArgumentNullException) { }

                                return NumberExtraction(input, startIndexExtraction, currentIndex);
                            }
                            throw new ArgumentException($"Invalid symbol '{input[currentIndex]}' detected.");
                    }
                    if (input[currentIndex] == ')')
                    currentIndex = BracketsBorder(input, currentIndex);
                }
            }
            throw new ArgumentNullException("Missing operand in expression.");
        }


        /// <returns>Number length</returns>
        static int NumberParse(string input, int lastIndex)
        {
            if (lastIndex >= 0)
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
                        int length = NumberParse(input, lastIndex - 1);
                        return ++length;
                    default:
                        return 0;
                }
            else
                return 0;
        }


        /// <summary>Extract a number from a string.</summary>
        /// <exception cref="ArgumentException"/>
        static double NumberExtraction(string input, int startIndex, int endIndex)
        {
            int decimalPoint = 0;
            double outputNumber = 0;

            for (int currentIndex = endIndex; currentIndex >= startIndex; currentIndex--)
            {
                int orderOfMagnitude = (endIndex - currentIndex) + 1;
                double orderFactor = OrderFactor(orderOfMagnitude - decimalPoint);

                switch (input[currentIndex])
                {
                    case '0':
                        outputNumber += 0 * orderFactor;
                        break;
                    case '1':
                        outputNumber += 1 * orderFactor;
                        break;
                    case '2':
                        outputNumber += 2 * orderFactor;
                        break;
                    case '3':
                        outputNumber += 3 * orderFactor;
                        break;
                    case '4':
                        outputNumber += 4 * orderFactor;
                        break;
                    case '5':
                        outputNumber += 5 * orderFactor;
                        break;
                    case '6':
                        outputNumber += 6 * orderFactor;
                        break;
                    case '7':
                        outputNumber += 7 * orderFactor;
                        break;
                    case '8':
                        outputNumber += 8 * orderFactor;
                        break;
                    case '9':
                        outputNumber += 9 * orderFactor;
                        break;
                    case ',':
                        if (decimalPoint == 0)
                        {
                            orderFactor = OrderFactor(currentIndex - endIndex);
                            decimalPoint = endIndex - currentIndex + 1;
                            outputNumber *= orderFactor;
                        }
                        else
                        {
                            throw new ArgumentException("Unexpected symbol ',' detected.");
                        }
                        break;
                    default:
                        return outputNumber;
                }
            }
            return outputNumber;
        }


        static double OrderFactor(int orderOfMagnitude)
        {
            decimal number = 1;
            if(orderOfMagnitude >= 0)
            for (int i = 1; i < orderOfMagnitude; i++)
            {
                number *= 10;
            }
            else
                for (int i = -1; i >= orderOfMagnitude; i--)
                {
                    number /= 10;
                }
            return (double)number;
        }


        /// <returns>Instance of MathOperand composed of expression in brackets</returns>
        /// <exception cref="ArgumentNullException"/>
        static MathOperand BracketsExpression(string input, int endIndex)
        {
            int startIndex = BracketsBorder(input, endIndex);

            if (endIndex != startIndex)
            {
               return ExpressionParse(input, startIndex + 1, endIndex - 1);
            }
            else
                throw new ArgumentNullException("Missing symbol \"(\".");   //Unreachable now.
        }


        /// <returns>startIndex of expression in brackets</returns>
        static int BracketsBorder(string input, int endIndex)
        {
            int nestingLevel = 0;
            for (int i = endIndex; i >= 0; i--)
            {
                if (input[i] == ')')
                    nestingLevel++;
                if (input[i] == '(')
                    if (nestingLevel > 1)
                        nestingLevel--;
                    else
                        return i;
            }
            return endIndex;
        }


        ///  <returns>Throws ArgumentException if extra brackets were found.</returns>
        /// <exception cref="ArgumentException"/>
        static void BracketsCheck(string input)
        {
            int nestingLevel = 0;
            for (int i = input.Length - 1; i >= 0; i--)
            {
                if (input[i] == ')')
                    nestingLevel++;
                if (input[i] == '(')
                    nestingLevel--;
            }
            if (nestingLevel != 0)
                throw new ArgumentException("Unexpected extra brackets.");
        }
    }
}
