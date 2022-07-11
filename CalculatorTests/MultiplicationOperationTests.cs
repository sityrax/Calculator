using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;

namespace MathExpression.MathOperations.Tests
{
    [TestClass()]
    public class MultiplicationOperationTests
    {
        [TestMethod()]
        public void MultiplicationTest()
        {
            //Arrange
            #region inputFirstOperand
            Mock<IMathOperand>[] inputFirstOperand = new Mock<IMathOperand>[4];
            inputFirstOperand[0] = new Mock<IMathOperand>();
            inputFirstOperand[1] = new Mock<IMathOperand>();
            inputFirstOperand[2] = new Mock<IMathOperand>();
            inputFirstOperand[3] = new Mock<IMathOperand>();
            inputFirstOperand[0].SetupGet(x => x.Value).Returns(1);
            inputFirstOperand[1].SetupGet(x => x.Value).Returns(25);
            inputFirstOperand[2].SetupGet(x => x.Value).Returns(0);
            inputFirstOperand[3].SetupGet(x => x.Value).Returns(0.5f);
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
            inputSecondOperand[3].SetupGet(x => x.Value).Returns(50);
            #endregion

            double[] expected = new double[] { 1, 250, 0, 25 };

            double[] actual = new double[inputFirstOperand.Length];
            Multiplication[] operation = new Multiplication[inputFirstOperand.Length];

            //Act
            for (int i = 0; i < inputFirstOperand.Length; i++)
            {
                operation[i] = new Multiplication(inputFirstOperand[i].Object, inputSecondOperand[i].Object);
                actual[i] = operation[i].Calculate();
            }

            //Asser
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MultiplicationCheckTest()
        {
            //Arrange
            Multiplication operation = new Multiplication();
            string expression = "1245,67*345/456";

            //Act
            if (!operation.CheckSymbol(expression, 7))
                throw new Exception("Symbol unidentified.");
            if (operation.CheckSymbol(expression, 11))
                throw new Exception("Wrong symbol dentified as correct.");

            //Assert
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void MultiplicationOperationParseTest()
        {
            //Arrange
            Multiplication operation = new Multiplication();
            Mock<IMathOperand> MO = new Mock<IMathOperand>();
            Mock<IMathOperand> returned = new Mock<IMathOperand>();
            Mock<IMathExpression> ME = new Mock<IMathExpression>();
            string[] inputs = new string[] { "1245,67*345", "1245,67/25*345+35" };
            int[] indexes = new int[] { inputs[0].IndexOf('*'), inputs[1].IndexOf('*') };
            returned.SetupSequence(x => x.Value).Returns(1245.67)
                                                .Returns(345)
                                                .Returns(1245.67 / 25)
                                                .Returns(345 + 35);

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
            MO.Verify(x => x.CreateInstance(It.IsAny<Multiplication>()), Times.Exactly(2));
        }

        [TestMethod()]
        public void MultiplicationCreateInstanceTest()
        {
            //Arrange
            Multiplication operation = new Multiplication();
            Mock<IMathOperand> firstOperand = new Mock<IMathOperand>();
            Mock<IMathOperand> secondOperand = new Mock<IMathOperand>();
            IMathOperation mathOperation;
            firstOperand.SetupGet(x => x.Value).Returns(34);
            secondOperand.SetupGet(x => x.Value).Returns(12);

            //Act
            mathOperation = operation.CreateInstance(firstOperand.Object, secondOperand.Object);
            double actual = mathOperation.Calculate();

            //Assert
            firstOperand.Verify(x => x.Value, Times.AtLeastOnce);
            secondOperand.Verify(x => x.Value, Times.AtLeastOnce);
            Assert.AreEqual(firstOperand.Object.Value * secondOperand.Object.Value, actual);
        }

        [ExpectedException(typeof(OverflowException))]
        [TestCategory("Exceptions")]
        [TestMethod()]
        public void OverflowTestException()
        {
            Mock<IMathOperand>[] mathOperands = MathOperandInstance();
            mathOperands[0].SetupGet(m => m.Value).Returns(double.MaxValue);
            mathOperands[1].SetupGet(m => m.Value).Returns(1.1d);
            Multiplication multiplication = new Multiplication(mathOperands[0].Object, mathOperands[1].Object);
            multiplication.Calculate();
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestCategory("Exceptions")]
        [TestMethod()]
        public void NullFirstOperandTestException()
        {
            Mock<IMathOperand>[] mathOperands = MathOperandInstance();
            mathOperands[0].SetupGet(m => m.Value).Returns(1);
            Multiplication multiplication = new Multiplication(null, mathOperands[0].Object);
            multiplication.Calculate();
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestCategory("Exceptions")]
        [TestMethod()]
        public void NullSecondOperandTestException()
        {
            Mock<IMathOperand>[] mathOperands = MathOperandInstance();
            mathOperands[0].SetupGet(m => m.Value).Returns(1);
            Multiplication multiplication = new Multiplication(mathOperands[0].Object, null);
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