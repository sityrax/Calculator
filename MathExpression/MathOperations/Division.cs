using System;

namespace MathExpression.MathOperations
{
    public class Division : BinaryExpression
    {
        ///<exception cref="ArgumentNullException"/>
        public Division(MathOperand firstOperand, MathOperand secondOperand) : base(firstOperand, secondOperand) { }


        /// <exception cref="DivideByZeroException"/>
        public override double Calculate()
        {
            if (secondOperand == 0)
                throw new DivideByZeroException("Division by zero detected.");
            return firstOperand / secondOperand;
        }
    }
}
