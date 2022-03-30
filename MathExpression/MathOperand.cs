using System;

namespace MathExpression
{
    /// <summary>Structure containing a hierarchy of mathematical operations.</summary>
    public class MathOperand
    {
        double value;
        public double Value
        {
            get
            {
                double result = subExpression is null ? value : subExpression.Calculate();
                return result;
            }
            set
            {
                this.value = value;
            }
        }

        public MathOperation subExpression { get; }

        public MathOperand(double operand) => value = operand;
        public MathOperand(MathOperation expression) => subExpression = expression;

        public static implicit operator double(MathOperand operand) => operand.Value;
        public static implicit operator MathOperand(double operand) => new MathOperand(operand);
        public static implicit operator MathOperand(MathOperation expression) => new MathOperand(expression);


        public override bool Equals(object obj)
        {
            if (obj is MathOperand mathOperand)
                return mathOperand.Value == Value;
            return false;
        }


        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
