using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathExpression;
using Calculator;
using System;
using System.Diagnostics;

namespace Calculator.Tests
{
    [TestClass()]
    public class MathConverterTests
    {
        [TestMethod()]
        public void ConvertTest()
        {
            string[] inputString = new[] { "1+2-3",          //simple expression with addition and substraction
                                           "5*7-2",          //multiply and substruction
                                           "5-7*2",          //another sequence 
                                           "5*7--2",         //ignore wrong second minus
                                           "-5-7*2",         //negative number without brackets
                                           "1+(2-3)",        //simple expression with brackets
                                           "5*(7-2)",        //multiply with brackets
                                           "5*7-(-2)",       //negative number in brackets
                                           "5* (7-2)",       //multiply with space and brackets
                                           "5-(-7)*2",       //negative number in brackets between two operations
                                           "7*(5+2)-3*5",           //expression in brackets between another expressions
                                           "125 + 225 * 3",         //numbers higher order number
                                           "7*(5+(2-3))*5",         //brackets inside brackets
                                           "7*((5+(2-3))*5)",       //brackets inside brackets... inside brackets
                                           "1900-(5+55)*(33-3)",                    //expression with two brackets
                                           "90-((5+(25+20))-(11*3-3))",             //expression with two brackets inside brackets
                                           "1900-(5+5+(2*(25-10)-5)*2)*(33-3)" };   //expression with brackets inside brackets with two brackets

            string[] expected = new[] { "0", "33", "-9", "33", "-19", "0", "25", "37", "25", "19", "34", "800", "140", "140", "100", "70", "100" };

            string[] actual = new string[inputString.Length];
            for (int i = 0; i < inputString.Length; i++)
            {
                actual[i] = MathConverter.StringToMathConvert(inputString[i]).Value.ToString();
            }

            CollectionAssert.AreEqual(expected, actual);
        }


        [ExpectedException(typeof(ArgumentException))]
        [TestMethod()]
        public void ConvertTestExceptionBracket()
        {
            MathConverter.StringToMathConvert("1900-((5+5+(2*(25-10)-5)*2)*(33-3)");
        }


        [ExpectedException(typeof(ArgumentException))]
        [TestMethod()]
        public void ConvertTestExceptionSpace()
        {
            MathConverter.StringToMathConvert("1 900-((5+5+(2*(25-10)-5)*2)*(33-3)");
        }


        [ExpectedException(typeof(ArgumentException))]
        [TestMethod()]
        public void ConvertTestExceptionSymbol()
        {
            MathConverter.StringToMathConvert("1900-(5+5+(g 2*(25-10)-5)*2)*(33-3)");
        }


        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod()]
        public void ConvertTestExceptionExtraOperation()
        {
           MathOperand answer = MathConverter.StringToMathConvert("5++7-2");
           double result = answer.Value;
        }


        //[TestMethod()]
        //public void NumberParseTest()
        //{
        //    string[] input = new[] { "123", "45", "3245", "96874234", "562 567", "4658 " };

        //    int[] inputPosition = new[] { 2, 1, 3, 7, 6, 4 };

        //    int[] expected = new[] { 3, 2, 4, 8, 3, 0 };

        //    int[] actual = new int[input.Length];
        //    for (int i = 0; i < input.Length; i++)
        //    {
        //        actual[i] = StringToMathConverter.NumberParse(input[i], inputPosition[i]);
        //    }

        //    CollectionAssert.AreEqual(expected, actual);
        //}


        //[TestMethod()]
        //public void BracketsBorderTest()
        //{
        //    string[] inputString = new[] { "(())", " (())", "35()", "(()", "())", "((()(()()))" };

        //    int[] expected = new[] { 0, 1, 2, 1, 2, 1 };

        //    int[] actual = new int[inputString.Length];
        //    for (int i = 0; i < inputString.Length; i++)
        //    {
        //        actual[i] = StringToMathConverter.BracketsBorder(inputString[i], inputString[i].Length - 1);
        //    }

        //    CollectionAssert.AreEqual(expected, actual);
        //}


        //[TestMethod()]
        //public void BracketsExpressionTest()
        //{
        //    string[] inputString = new[] { "(25-5)", "((35-5))", "(7-(4+5))" };

        //    MathOperand[] expected = new MathOperand[] { 20, 30, -2 };

        //    MathOperand[] actual = new MathOperand[inputString.Length];
        //    for (int i = 0; i < inputString.Length; i++)
        //    {
        //        actual[i] = StringToMathConverter.BracketsExpression(inputString[i], inputString[i].Length - 1);
        //    }

        //    CollectionAssert.AreEqual(expected, actual);
        //}


        //[ExpectedException(typeof(ArgumentNullException))]
        //[TestMethod()]
        //public void BracketsExpressionTestException()
        //{
        //    StringToMathConverter.BracketsExpression("(ab)c)", 5);
        //}


        //[ExpectedException(typeof(ArgumentException))]
        //[TestMethod()]
        //public void BracketsCheckTestExceptionLeftBracket()
        //{
        //    StringToMathConverter.BracketsCheck("(( 5 443 + 7685)");
        //}


        //[ExpectedException(typeof(ArgumentException))]
        //[TestMethod()]
        //public void BracketsCheckTestExceptionRightBracket()
        //{
        //    StringToMathConverter.BracketsCheck("( 5 443) + 7685)");
        //}
    }
}