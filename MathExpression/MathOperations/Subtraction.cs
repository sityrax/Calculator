using System;

namespace MathExpression.MathOperations
{
    public class Subtraction : BinaryExpression
    {
        ///<exception cref="ArgumentNullException"/>
        public Subtraction(MathOperand firstOperand, MathOperand secondOperand) : base(firstOperand, secondOperand) { }


        public override double Calculate()
        {
            if (firstOperand is null)
                firstOperand = 0;
            if (secondOperand is null)
                secondOperand = 0;
            return firstOperand - secondOperand;
        }
    }
}
