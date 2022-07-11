using System;

namespace MathExpression.MathOperations
{
    public class Subtraction : BinaryExpression, IMathOperation
    {
        const char operationSymbol = '-';
        public string OperationSymbol { get => operationSymbol.ToString(); }

        /// <summary>
        /// Creates an instance with default values of MathOperands.
        /// </summary>
        public Subtraction() : base(firstOperand: null, secondOperand: null) { }
        public Subtraction(IMathOperand firstOperand, IMathOperand secondOperand) : base(firstOperand, secondOperand) { }

        public IMathOperation CreateInstance(IMathOperand firstOperand, IMathOperand secondOperand) => new Subtraction(firstOperand, secondOperand);

        public double Calculate()
        {
            return (firstOperand?.Value ?? 0) - secondOperand.Value;
        }

        public bool CheckSymbol(string input, int index)
        {
            // проверка соответствия обозначению операции.
            if (operationSymbol == input[index])
                return true;
            return false;
        }

        public IMathOperand OperationParse(IMathExpression ME,
                                   IMathOperand MO,
                                   string input,
                                   int startIndex,
                                   int currentIndex,
                                   int endIndex)
        {
            if (CheckSymbol(input, currentIndex))
            {
                var mathOperation = CreateInstance(ME.ExpressionParse(input, startIndex, currentIndex - 1),
                                                   ME.ExpressionParse(input, currentIndex + 1, endIndex));
                return MO.CreateInstance(mathOperation);
            }
            else
                throw new ArgumentException("Wrong operation symbol.");
        }
    }
}
