using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathExpression.MathNumbers;
using System;
using Moq;

namespace MathExpression.Tests
{
    [TestClass()]
    public class DecimalNumberSystemTests
    {
        [TestMethod()]
        public void NumberParseTest()
        {
            //Arrange
            DecimalNumberSystem decimalNumberSystem = new DecimalNumberSystem();
            Mock<IMathExpression> ME = new Mock<IMathExpression>();
            Mock<IMathOperand> MO    = new Mock<IMathOperand>();
            ME.Setup(x => x.ExpressionParse(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
              .Throws(new ArgumentNullException());

            string[] input     = { "123", "%$1", "=9807", "987,9", "4566,342;" };
            int[] numberLength = { 3, 1, 4, 5, 8 };
            int[] startIndex   = { 0, 2, 1, 0, 0 };
            int[] endIndex     = { 2, 2, 4, 4, 7 };

            //Act
            for (int i = 0; i < 5; i++)
            {
                decimalNumberSystem.NumberParse(ME.Object, MO.Object, input[i], numberLength[i], startIndex[i], endIndex[i]);
            }

            //Assert
            MO.Verify(x => x.CreateInstance(It.IsIn<double>(123, 1, 9807, 987.9d, 4566.342d)), Times.Exactly(5));
        }

        [TestMethod()]
        public void NumberCheckTest()
        {
            //Arrange
            DecimalNumberSystem decimalNumberSystem = new DecimalNumberSystem();

            string[] input  = { "123", "%$1", "=9807", "987,9", "4566,342;" };
            int[] expected  = { 3, 1, 4, 5, 8 };
            int[] lastIndex = { 2, 2, 4, 4, 7 };
            int[] actual = new int[5];

            //Act
            for (int i = 0; i < 5; i++)
            {
                actual[i] = decimalNumberSystem.NumberCheck(input[i], 0, lastIndex[i]);
            }

            //Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestCategory("Exception")]
        [TestMethod]
        public void NumberParseTestException()
        {
            DecimalNumberSystem decimalNumberSystem = new DecimalNumberSystem();
            decimalNumberSystem.NumberParse(It.IsAny<IMathExpression>(), It.IsAny<IMathOperand>(), "123", 0, 0, 2);
        }
    }
}