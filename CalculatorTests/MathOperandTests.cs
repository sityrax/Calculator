using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathExpression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace MathExpression.Tests
{
    [TestClass()]
    public class MathOperandTests
    {
        [TestMethod()]
        public void EqualsTest()
        {
            //Arrange
            MathOperand mathOperand1 = 5;
            MathOperand mathOperand2 = 5;

            //Act
            if (mathOperand1 == mathOperand2)
                Assert.IsTrue(true);
            else

            //Assert
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateInstanceTest()
        {
            //Arrange
            MathOperand mathOperand = new MathOperand();
            Mock<IMathOperation> mathOperation = new Mock<IMathOperation>();
            mathOperation.Setup(x => x.Calculate()).Returns(5d);

            //Act
            IMathOperand actual1 = mathOperand.CreateInstance(5);
            IMathOperand actual2 = mathOperand.CreateInstance(mathOperation.Object);

            //Assert
            Assert.AreEqual(actual1, 5d);
            Assert.AreEqual(actual2, mathOperation.Object.Calculate());
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestCategory("Exceptions")]
        [TestMethod()]
        public void NullValueTestException()
        {
            //Arrange
            Mock<IMathOperation> mathOperation = new Mock<IMathOperation>();
            mathOperation.Setup(x => x.Calculate()).Throws(new NullReferenceException());
            MathOperand mathOperand = new MathOperand(mathOperation.Object);

            //Act
            double actual = mathOperand.Value;
        }
    }
}