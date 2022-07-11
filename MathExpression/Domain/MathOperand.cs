using System;

namespace MathExpression
{
    /// <summary>Structure containing a hierarchy of mathematical operations.</summary>
    public class MathOperand : IMathOperand
    {
        double value;
        public double Value
        {
            get
            {
                try
                {
                    double result = subOperation is null ? value : subOperation.Calculate();
                    return result;
                }
                catch (NullReferenceException)
                {
                    throw new ArgumentNullException("Null operand detected.");
                }
            }
            set
            {
                this.value = value;
            }
        }

        public IMathOperation subOperation { get; }

        public MathOperand() => value = 0;
        public MathOperand(double operand) => value = operand;
        public MathOperand(IMathOperation operation) => subOperation = operation;

        public IMathOperand CreateInstance(double operand) => new MathOperand(operand);
        public IMathOperand CreateInstance(IMathOperation operation) => new MathOperand(operation);

        public override bool Equals(object obj)
        {
            if (obj is MathOperand mathOperand)
                return mathOperand.Value == Value;
            if (obj is double value)
                return value == Value;
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static implicit operator double(MathOperand operand) => operand.Value;
        public static implicit operator MathOperand(double operand) => new MathOperand(operand);

        public static bool operator ==(MathOperand item1, MathOperand item2) => item1.Equals(item2);
        public static bool operator !=(MathOperand item1, MathOperand item2) => !item1.Equals(item2);

    }
}
