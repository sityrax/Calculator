using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;

namespace MathExpression.MathOperations.Tests
{
    [TestClass()]
    public class DivisionOperationTests
    {
        [TestMethod()]
        public void DivisionTest()
        {
            #region inputFirstOperand
            Mock<IMathOperand>[] inputFirstOperand = new Mock<IMathOperand>[4];
            inputFirstOperand[0] = new Mock<IMathOperand>();
            inputFirstOperand[1] = new Mock<IMathOperand>();
            inputFirstOperand[2] = new Mock<IMathOperand>();
            inputFirstOperand[3] = new Mock<IMathOperand>();
            inputFirstOperand[0].SetupGet(x => x.Value).Returns(1);
            inputFirstOperand[1].SetupGet(x => x.Value).Returns(25);
            inputFirstOperand[2].SetupGet(x => x.Value).Returns(0);
            inputFirstOperand[3].SetupGet(x => x.Value).Returns(1024);
            #endregion

            #region inputSecondOperand
            Mock<IMathOperand>[] inputSecondOperand = new Mock<IMathOperand>[4];
            inputSecondOperand[0] = new Mock<IMathOperand>();
            inputSecondOperand[1] = new Mock<IMathOperand>();
            inputSecondOperand[2] = new Mock<IMathOperand>();
            inputSecondOperand[3] = new Mock<IMathOperand>();
            inputSecondOperand[0].SetupGet(x => x.Value).Returns(1);
            inputSecondOperand[1].SetupGet(x => x.Value).Returns(10);
            inputSecondOperand[2].SetupGet(x => x.Value).Returns(255);
            inputSecondOperand[3].SetupGet(x => x.Value).Returns(256);
            #endregion

            double[] expected = new double[] { 1, 2.5f, 0, 4 };

            double[] actual = new double[inputFirstOperand.Length];
            Division[] operation = new Division[inputFirstOperand.Length];

            for (int i = 0; i < inputFirstOperand.Length; i++)
            {
                operation[i] = new Division(inputFirstOperand[i].Object, inputSecondOperand[i].Object);
                actual[i] = operation[i].Calculate();
            }

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DivisionCheckTest()
        {
            //Arrange
            Division operation = new Division();
            string expression = "1245,67*345/456";

            //Act
            if (!operation.CheckSymbol(expression, 11))
                throw new Exception("Symbol unidentified.");
            if (operation.CheckSymbol(expression, 7))
                throw new Exception("Wrong symbol dentified as correct.");

            //Assert
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void DivisionOperationParseTest()
        {
            //Arrange
            Division operation = new Division();
            Mock<IMathOperand> MO = new Mock<IMathOperand>();
            Mock<IMathOperand> returned = new Mock<IMathOperand>();
            Mock<IMathExpression> ME = new Mock<IMathExpression>();
            string[] inputs = new string[] { "1245,67/345", "1245,67+25/345-35" };
            int[] indexes = new int[] { inputs[0].IndexOf('/'), inputs[1].IndexOf('/') };
            returned.SetupSequence(x => x.Value).Returns(1245.67)
                                                .Returns(345)
                                                .Returns(1245.67 + 25)
                                                .Returns(345 - 35);

            ME.Setup(x => x.ExpressionParse(inputs[0], 0, 6)).Returns(returned.Object);
            ME.Setup(x => x.ExpressionParse(inputs[0], 8, 10)).Returns(returned.Object);
            ME.Setup(x => x.ExpressionParse(inputs[1], 0, 9)).Returns(returned.Object);
            ME.Setup(x => x.ExpressionParse(inputs[1], 11, 16)).Returns(returned.Object);

            //Act
            for (int i = 0; i < inputs.Length; i++)
            {
                operation.OperationParse(ME.Object, MO.Object, inputs[i], 0, indexes[i], inputs[i].Length);

            }

            //Assert
            ME.Verify(x => x.ExpressionParse(It.IsIn<string>(inputs), It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(4));
            MO.Verify(x => x.CreateInstance(It.IsAny<Division>()), Times.Exactly(2));
        }

        [TestMethod()]
        public void DivisionCreateInstanceTest()
        {
            //Arrange
            Division operation = new Division();
            Mock<IMathOperand> firstOperand = new Mock<IMathOperand>();
            Mock<IMathOperand> secondOperand = new Mock<IMathOperand>();
            IMathOperation mathOperation;
            firstOperand.SetupGet(x => x.Value).Returns(36);
            secondOperand.SetupGet(x => x.Value).Returns(12);

            //Act
            mathOperation = operation.CreateInstance(firstOperand.Object, secondOperand.Object);
            double actual = mathOperation.Calculate();

            //Assert
            firstOperand.Verify(x => x.Value, Times.AtLeastOnce);
            secondOperand.Verify(x => x.Value, Times.AtLeastOnce);
            Assert.AreEqual(firstOperand.Object.Value / secondOperand.Object.Value, actual);
        }

        [ExpectedException(typeof(DivideByZeroException))]
        [TestCategory("Exceptions")]
        [TestMethod()]
        public void DivideByZeroTestException()
        {
            Mock<IMathOperand>[] mathOperands = MathOperandInstance();
            mathOperands[0].SetupGet(m => m.Value).Returns(500);
            mathOperands[1].SetupGet(m => m.Value).Returns(0);
            Division division = new Division(mathOperands[0].Object, mathOperands[1].Object);
            division.Calculate();
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestCategory("Exceptions")]
        [TestMethod()]
        public void NullFirstOperandTestException()
        {
            Mock<IMathOperand>[] mathOperands = MathOperandInstance();
            mathOperands[0].SetupGet(m => m.Value).Returns(1);
            Division multiplication = new Division(null, mathOperands[0].Object);
            multiplication.Calculate();
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestCategory("Exceptions")]
        [TestMethod()]
        public void NullSecondOperandTestException()
        {
            Mock<IMathOperand>[] mathOperands = MathOperandInstance();
            mathOperands[0].SetupGet(m => m.Value).Returns(1);
            Division multiplication = new Division(mathOperands[0].Object, null);
            multiplication.Calculate();
        }

        private static Mock<IMathOperand>[] MathOperandInstance()
        {
            Mock<IMathOperand>[] mathOperands = new Mock<IMathOperand>[2];
                                 mathOperands[0] = new Mock<IMathOperand>();
                                 mathOperands[1] = new Mock<IMathOperand>();
            return mathOperands;
        }
    }
}