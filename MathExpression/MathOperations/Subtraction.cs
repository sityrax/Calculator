using System;

namespace MathExpression.MathOperations
{
    public class Subtraction : BinaryExpression
    {
        ///<exception cref="ArgumentNullException"/>
        public Subtraction(MathOperand firstOperand, MathOperand secondOperand) : base(firstOperand, secondOperand) { }


        public override double Calculate()
        {
            return firstOperand - secondOperand;
        }
    }
}
