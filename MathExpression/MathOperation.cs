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

        ///<exception cref="OverflowException"/>
        ///<exception cref="DivideByZeroException"/>
        ///<exception cref="ArgumentNullException"/>
        public BinaryExpression(MathOperand firstOperand, MathOperand secondOperand)
        {
            this.firstOperand = firstOperand;
            this.secondOperand = secondOperand;
        }
    }
}
