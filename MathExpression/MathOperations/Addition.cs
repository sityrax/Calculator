using System;

namespace MathExpression.MathOperations
{
    public class Addition : BinaryExpression
    {
        ///<exception cref="ArgumentNullException"/>
        public Addition(MathOperand firstOperand, MathOperand secondOperand) : base(firstOperand, secondOperand) { }


        ///<exception cref="OverflowException"/>
        public override double Calculate()
        {
            double result = firstOperand + secondOperand;
            if (double.MaxValue - firstOperand < secondOperand || double.MinValue - firstOperand > secondOperand)
                throw new OverflowException("Result has too long number.");
            return result;
        }
    }
}
