using System;
using System.Diagnostics;

namespace Calculator
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


    //OPERATIONS:
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


    public class Subtraction : BinaryExpression
    {
        ///<exception cref="ArgumentNullException"/>
        public Subtraction(MathOperand firstOperand, MathOperand secondOperand) : base(firstOperand, secondOperand) { }


        public override double Calculate()
        {
            return firstOperand - secondOperand;
        }
    }


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
