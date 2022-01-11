﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator;
using System;

namespace Calculator.Tests
{
    [TestClass()]
    public class StringToMathConverterTests
    {
        [TestMethod()]
        public void ConvertTest()
        {
            string[] inputString = new[] { "1+2-3",
                                           "5*7-2",
                                           "5-7*2",
                                           "1+(2-3)",
                                           "5*(7-2)",
                                           "5* (7-2)",
                                           "7*(5+2)-3*5",
                                           "125 + 225 * 3",
                                           "7*(5+(2-3))*5",
                                           "7*((5+(2-3))*5)",
                                           "7*((5+(2-3))+5)",
                                           "1900-(5+55)*(33-3)",
                                           "90-((5+(25+20))-(11*3-3))",
                                           "1900-(5+5+(2*(25-10)-5)*2)*(33-3)" };

            string[] expected = new[] { "0", "33", "-9", "0", "25", "25", "34", "800", "140", "140", "63", "100", "70", "100" };

            string[] actual = new string[inputString.Length];
            for (int i = 0; i < inputString.Length; i++)
            {
                actual[i] = StringToMathConverter.Convert(inputString[i]).Value.ToString();
            }

            CollectionAssert.AreEqual(expected, actual);
        }


        [ExpectedException(typeof(ArgumentException))]
        [TestMethod()]
        public void ConvertTestExceptionBracket()
        {
            StringToMathConverter.Convert("1900-((5+5+(2*(25-10)-5)*2)*(33-3)");
        }


        [ExpectedException(typeof(ArgumentException))]
        [TestMethod()]
        public void ConvertTestExceptionSymbol()
        {
            StringToMathConverter.Convert("1900-(5+5+(g 2*(25-10)-5)*2)*(33-3)");
        }


        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod()]
        public void ConvertTestExceptionExtraOperation()
        {
            StringToMathConverter.Convert("1900-(5+5+(2*(25-10)-5*)*2)*(33-3)");
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
        //public void NumberExtractionTest()
        //{
        //    string[] inputString = new[] { "123", "45", "3245", "9687 4234", "562 567", "456 ", "4 658", "4,658" };
        //    int[] inputStartIndex = new[] { 0, 1, 1, 5, 3, 0, 0, 0, };
        //    int[] inputEndIndex = new[] { 2, 1, 3, 8, 6, 3, 3, 4, };

        //    double[] expected = new[] { 123, 5, 245, 4234, 567, 0, 65, 4.658 };

        //    double[] actual = new double[inputString.Length];
        //    for (int i = 0; i < inputString.Length; i++)
        //    {
        //        actual[i] = StringToMathConverter.NumberExtraction(inputString[i], inputStartIndex[i], inputEndIndex[i]);
        //    }

        //    CollectionAssert.AreEqual(expected, actual);
        //}


        //[ExpectedException(typeof(ArgumentException))]
        //[TestMethod()]
        //public void NumberExtractionTestException()
        //{
        //    StringToMathConverter.NumberExtraction("254,568,789", 0, 10);
        //}


        //[TestMethod()]
        //public void OrderOfMagnitudeTest()
        //{
        //    int[] input = new[] { 1, 2, 3, 5, -1, -2, -3, -15 };

        //    double[] expected = new[] { 1, 10, 100, 10000, 0.1d, 0.01d, 0.001d, 1e-15d };

        //    double[] actual = new double[input.Length];
        //    for (int i = 0; i < input.Length; i++)
        //    {
        //        actual[i] = StringToMathConverter.OrderFactor(input[i]);
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