using System;

namespace MathExpression
{
    public class MathConverter : IMathExpression
    {
        readonly IMathOperand mathOperand;
        readonly IMathNumberSystem mathNumberSystem;
        readonly IMathOperation[][] mathOperations;
        
        public MathConverter(IMathOperand mathOperand, IMathNumberSystem mathNumberSystem, IMathOperation[][] mathOperations)
        {
            this.mathOperand = mathOperand;
            this.mathNumberSystem = mathNumberSystem;
            this.mathOperations = mathOperations;
        }

        /// <returns>Instance of MathOperand composed of input string.</returns>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="DivideByZeroException"/>
        /// <exception cref="OverflowException"/>
        public IMathOperand StringToMathConvert(string input)
        {
            BracketsCheck(input);
            string inputСompact = input.Replace(" ", string.Empty); 
            return ExpressionParse(inputСompact, 0, inputСompact.Length - 1);
        }

        /// <returns>Instance of MathOperand composed of input string.</returns>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArithmeticException"/>
        public IMathOperand ExpressionParse(string input, int startIndex, int endIndex)
        {
            if (startIndex > endIndex)
                return null;

            // перебор операций по приоритету.
            foreach (var operationPriority in mathOperations)
            {
                // перебор символов в строке.
                for (int currentIndex = endIndex; currentIndex >= startIndex; currentIndex--)
                {
                    // перебор операций одного приоритета.
                    foreach (var Operation in operationPriority)
                    {
                        // проверка соответствия обозначения операции.
                        if(Operation.CheckSymbol(input, currentIndex))
                        {
                            return Operation.OperationParse(this, mathOperand, input, startIndex, currentIndex, endIndex);
                        }
                    }
                    // пропуск скобок.
                    if (input[currentIndex] == ')')
                    {
                        currentIndex = BracketsBorder(input, currentIndex);
                        if (mathNumberSystem.NumberCheck(input, startIndex, currentIndex - 1) > 0)
                            throw new ArgumentNullException("Missing operand in expression.");
                    }
                }
            }
            // проверка выражений в скобках.
            for (int currentIndex = endIndex; currentIndex > startIndex; currentIndex--)
            {
                if (input[currentIndex] == ')')
                   return BracketsExpression(input, currentIndex);
            }
            // если ни одна операция не подошла, то пробуем парсить число.
            int numberLength = mathNumberSystem.NumberCheck(input, startIndex, endIndex); 
                        return mathNumberSystem.NumberParse(this, mathOperand, input, numberLength, startIndex, endIndex);
        }

        /// <returns>Instance of MathOperand composed of expression in brackets.</returns>
        /// <exception cref="ArgumentNullException"/>
        IMathOperand BracketsExpression(string input, int endIndex)
        {
            int startIndex = BracketsBorder(input, endIndex);

            if (endIndex != startIndex)
            {
               return ExpressionParse(input, startIndex + 1, endIndex - 1);
            }
            else
                throw new ArgumentNullException("Missing symbol \"(\".");   // unreachable now.
        }

        /// <returns>startIndex of expression in brackets.</returns>
        int BracketsBorder(string input, int endIndex)
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
        void BracketsCheck(string input)
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
