using System;
using System.Diagnostics;

namespace MathExpression
{
    public abstract class MathOperation
    {
        public abstract double Calculate();
    }


    public abstract class BinaryExpression : MathOperation
    {
        protected MathOperand firstOperand;
        protected MathOperand secondOperand;


        ///<exception cref="ArgumentNullException"/>
        public BinaryExpression(MathOperand firstOperand, MathOperand secondOperand)
        {
            if (firstOperand is null || secondOperand is null)
                throw new ArgumentNullException("Null operand detected.");
            this.firstOperand = firstOperand;
            this.secondOperand = secondOperand;
        }
    }
}
