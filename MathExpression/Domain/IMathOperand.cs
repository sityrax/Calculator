namespace MathExpression
{
    public interface IMathOperand
    {
        double Value { get; set; }

        IMathOperand CreateInstance(double operand);
        IMathOperand CreateInstance(IMathOperation mathOperation);
        bool Equals(object obj);
    }
}