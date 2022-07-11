using System;

namespace MathExpression
{
    public interface IMathOperation
    {
        public string OperationSymbol { get; }

        /// <exception cref="ArithmeticException"></exception>
        public double Calculate();
        public IMathOperation CreateInstance(IMathOperand firstOperand, IMathOperand secondOperand);
        /// <exception cref="ArgumentException">Wrong operation symbol.</exception>
        public IMathOperand OperationParse(IMathExpression mathExpression, IMathOperand mathOperand, string input, int startIndex, int index, int endIndex);
        public bool CheckSymbol(string input, int index);
    }

    public abstract class BinaryExpression
    {
        protected IMathOperand firstOperand;
        protected IMathOperand secondOperand;

        public BinaryExpression(IMathOperand firstOperand, IMathOperand secondOperand)
        {
            this.firstOperand  = firstOperand;
            this.secondOperand = secondOperand;
        }
    }
}
