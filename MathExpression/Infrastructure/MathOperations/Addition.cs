using System;

namespace MathExpression.MathOperations
{
    public class Addition : BinaryExpression, IMathOperation
    {
        const char operationSymbol = '+';
        public string OperationSymbol { get => operationSymbol.ToString(); }

        /// <summary>
        /// Creates an instance with default values of MathOperands.
        /// </summary>
        public Addition() : base(firstOperand: null, secondOperand: null) { }
        public Addition(IMathOperand firstOperand, IMathOperand secondOperand) : base(firstOperand, secondOperand) { }

        public IMathOperation CreateInstance(IMathOperand firstOperand, IMathOperand secondOperand) => new Addition(firstOperand, secondOperand);

        ///<exception cref="OverflowException"/>
        public double Calculate()
        {
            double result = firstOperand.Value + secondOperand.Value;
            if (double.MaxValue - firstOperand.Value < secondOperand.Value || double.MinValue - firstOperand.Value > secondOperand.Value)
                throw new OverflowException("Result has too long number.");
            return result;
        }

        public bool CheckSymbol(string input, int index)
        {
            // проверка соответствия обозначению операции.
            if (operationSymbol == input[index])
                return true;
            return false;
        }

        /// <remarks>It is recommended to check the character by CheckSymbol before use.</remarks>
        /// <exception cref="ArgumentException">Wrong operation symbol.</exception>
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
