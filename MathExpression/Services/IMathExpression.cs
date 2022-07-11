namespace MathExpression
{
    public interface IMathExpression
    {
        /// <exception cref="ArgumentNullException">Missing operand in expression.</exception>
        public IMathOperand ExpressionParse(string input, int startIndex, int endIndex);
    }
}