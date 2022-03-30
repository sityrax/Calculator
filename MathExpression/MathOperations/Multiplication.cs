using System;

namespace MathExpression.MathOperations
{
    public class Multiplication : BinaryExpression
    {
        ///<exception cref="ArgumentNullException"/>
        public Multiplication(MathOperand firstOperand, MathOperand secondOperand) : base(firstOperand, secondOperand) { }


        ///<exception cref="OverflowException"/>
        public override double Calculate()
        {
            double result = firstOperand * secondOperand;
            if (double.IsInfinity(result))
                throw new OverflowException("Result has too long number.");
            return result;
        }
    }
}
