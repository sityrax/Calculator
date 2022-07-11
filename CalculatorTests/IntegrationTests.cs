using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathExpression.MathOperations;
using MathExpression.MathNumbers;
using System;

namespace MathExpression.Tests
{
    [TestClass()]
    public class IntegrationTests
    {
        IMathOperand mathOperand;
        IMathOperation[][] operationArray;
        IMathNumberSystem mathNumberSystem;

        [TestInitialize]
        public void MathConverterTestsStart()
        {
            mathOperand = new MathOperand();
            mathNumberSystem = new DecimalNumberSystem();
            operationArray = new IMathOperation[][]
            {
                new IMathOperation[] { new Addition(),
                                       new Subtraction()},
                new IMathOperation[] { new Multiplication(),
                                       new Division()}
            };
        }

        [TestMethod()]
        public void ConvertTest()
        {
            //Arrange
            string[] inputString = new[] { "1+2-3",          //simple expression with addition and substraction
                                           "5*7-2",          //multiply and substruction
                                           "5-7*2",          //another sequence 
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

            string[] expected = new[] { "0", "33", "-9", "-19", "0", "25", "37", "25", "19", "34", "800", "140", "140", "100", "70", "100" };

            MathConverter mathConverter = new MathConverter(mathOperand, mathNumberSystem, operationArray);
            string[] actual = new string[inputString.Length];

            //Act
            for (int i = 0; i < inputString.Length; i++)
            {
                IMathOperand mathOperand = mathConverter.StringToMathConvert(inputString[i]);
                actual[i] = mathOperand.Value.ToString();
            }

            //Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestCategory("Exceptions")]
        [TestMethod()]
        public void ConvertTestExceptionBracket()
        {
            MathConverter mathConverter = new MathConverter(mathOperand, mathNumberSystem, operationArray);
            mathConverter.StringToMathConvert("1900-((5+5+(2*(25-10)-5)*2)*(33-3)");
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestCategory("Exceptions")]
        [TestMethod()]
        public void ConvertTestExceptionSpace()
        {
            MathConverter mathConverter = new MathConverter(mathOperand, mathNumberSystem, operationArray);
            mathConverter.StringToMathConvert("1 900-((5+5+(2*(25-10)-5)*2)*(33-3)");
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestCategory("Exceptions")]
        [TestMethod()]
        public void MissingOperandTestExceptionSpace()
        {
            MathConverter mathConverter = new MathConverter(mathOperand, mathNumberSystem, operationArray);
            mathConverter.StringToMathConvert("1900-25(5+5)");
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestCategory("Exceptions")]
        [TestMethod()]
        public void ConvertTestExceptionSymbol()
        {
            MathConverter mathConverter = new MathConverter(mathOperand, mathNumberSystem, operationArray);
            mathConverter.StringToMathConvert("1900-(5+5+(g 2*(25-10)-5)*2)*(33-3)");
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestCategory("Exceptions")]
        [TestMethod()]
        public void ConvertTestExceptionExtraOperation()
        {
            MathConverter mathConverter = new MathConverter(mathOperand, mathNumberSystem, operationArray);
            IMathOperand answer = mathConverter.StringToMathConvert("5++7");
            double result = answer.Value;    //TODO: null reference exception
        }
    }
}