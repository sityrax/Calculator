using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;

namespace MathExpression.MathOperations.Tests
{
    [TestClass()]
    public class AdditionOperationTests
    {
        [TestMethod()]
        public void AdditionTest()
        {
            #region inputFirstOperand
            Mock<IMathOperand>[] inputFirstOperand = new Mock<IMathOperand>[5];
            inputFirstOperand[0] = new Mock<IMathOperand>();
            inputFirstOperand[1] = new Mock<IMathOperand>();
            inputFirstOperand[2] = new Mock<IMathOperand>();
            inputFirstOperand[3] = new Mock<IMathOperand>();
            inputFirstOperand[4] = new Mock<IMathOperand>();
            inputFirstOperand[0].SetupGet(x => x.Value).Returns(1);
            inputFirstOperand[1].SetupGet(x => x.Value).Returns(25);
            inputFirstOperand[2].SetupGet(x => x.Value).Returns(255);
            inputFirstOperand[3].SetupGet(x => x.Value).Returns(153);
            inputFirstOperand[4].SetupGet(x => x.Value).Returns(0);
            #endregion

            #region inputSecondOperand
            Mock<IMathOperand>[] inputSecondOperand = new Mock<IMathOperand>[5];
            inputSecondOperand[0] = new Mock<IMathOperand>();
            inputSecondOperand[1] = new Mock<IMathOperand>();
            inputSecondOperand[2] = new Mock<IMathOperand>();
            inputSecondOperand[3] = new Mock<IMathOperand>();
            inputSecondOperand[4] = new Mock<IMathOperand>();
            inputSecondOperand[0].SetupGet(x => x.Value).Returns(1);
            inputSecondOperand[1].SetupGet(x => x.Value).Returns(25);
            inputSecondOperand[2].SetupGet(x => x.Value).Returns(255);
            inputSecondOperand[3].SetupGet(x => x.Value).Returns(107);
            inputSecondOperand[4].SetupGet(x => x.Value).Returns(double.MaxValue);
            #endregion

            double[] expected = new double[] { 2, 50, 510, 260, double.MaxValue };

            double[] actual = new double[inputFirstOperand.Length];
            Addition[] operation = new Addition[inputFirstOperand.Length];

            for (int i = 0; i < inputFirstOperand.Length; i++)
            {
                operation[i] = new Addition(inputFirstOperand[i].Object, inputSecondOperand[i].Object);
                actual[i] = operation[i].Calculate();
            }

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AdditionCheckTest()
        {
            //Arrange
            Addition operation = new Addition();
            string expression = "1245,67+345-456";

            //Act
            if (!operation.CheckSymbol(expression, 7))
                throw new Exception("Symbol unidentified.");
            if (operation.CheckSymbol(expression, 10))
                throw new Exception("Wrong symbol dentified as correct.");

            //Assert
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void AdditionOperationParseTest()
        {
            //Arrange
            Addition operation = new Addition();
            Mock<IMathOperand> MO = new Mock<IMathOperand>();
            Mock<IMathExpression> ME = new Mock<IMathExpression>();
            Mock<IMathOperand> returned = new Mock<IMathOperand>();
            string[] inputs = new string[] { "1245,67+345", "1245,67/25+345*35" };
            int[] indexes = new int[] { inputs[0].IndexOf('+'), inputs[1].IndexOf('+') };
            returned.SetupSequence(x => x.Value).Returns(1245.67)
                                                .Returns(345)
                                                .Returns(1245.67 / 25)
                                                .Returns(345 * 35);

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
            MO.Verify(x => x.CreateInstance(It.IsAny<Addition>()), Times.Exactly(2));
        }

        [TestMethod()]
        public void AdditionCreateInstanceTest()
        {
            //Arrange
            Addition operation = new Addition();
            Mock<IMathOperand> firstOperand = new Mock<IMathOperand>();
            Mock<IMathOperand> secondOperand = new Mock<IMathOperand>();
            IMathOperation mathOperation;
            firstOperand.SetupGet(x => x.Value).Returns(123);
            secondOperand.SetupGet(x => x.Value).Returns(345);

            //Act
            mathOperation = operation.CreateInstance(firstOperand.Object, secondOperand.Object);
            double actual = mathOperation.Calculate();

            //Assert
            firstOperand.Verify(x => x.Value, Times.AtLeastOnce);
            secondOperand.Verify(x => x.Value, Times.AtLeastOnce);
            Assert.AreEqual(firstOperand.Object.Value + secondOperand.Object.Value, actual);
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestCategory("Exceptions")]
        [TestMethod()]
        public void AdditionTestArgumentNullExceptionException()
        {
            Mock<IMathOperand>[] mathOperands = MathOperandInstance();
            mathOperands[0].SetupGet(m => m.Value).Returns(25);
            IMathOperation addition = new Addition(mathOperands[0].Object, null);
            double result = addition.Calculate();
        }


        [ExpectedException(typeof(OverflowException))]
        [TestCategory("Exceptions")]
        [TestMethod()]
        public void AdditionTestOverflowException()
        {
            Mock<IMathOperand>[] mathOperands = MathOperandInstance();
            mathOperands[0].SetupGet(m => m.Value).Returns(double.MaxValue);
            mathOperands[1].SetupGet(m => m.Value).Returns(1);
            Addition addition = new Addition(mathOperands[0].Object, mathOperands[1].Object);
            addition.Calculate();
        }


        [ExpectedException(typeof(OverflowException))]
        [TestCategory("Exceptions")]
        [TestMethod()]
        public void AdditionTestOverflowException2()
        {
            Mock<IMathOperand>[] mathOperands = MathOperandInstance();
            mathOperands[0].SetupGet(m => m.Value).Returns(-double.MaxValue * 0.75);
            mathOperands[1].SetupGet(m => m.Value).Returns(-double.MaxValue * 0.75);
            Addition addition = new Addition(mathOperands[0].Object, mathOperands[1].Object);
            addition.Calculate();
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestCategory("Exceptions")]
        [TestMethod()]
        public void NullFirstOperandTestException()
        {
            Mock<IMathOperand>[] mathOperands = MathOperandInstance();
            mathOperands[0].SetupGet(m => m.Value).Returns(1);
            Addition multiplication = new Addition(null, mathOperands[0].Object);
            multiplication.Calculate();
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestCategory("Exceptions")]
        [TestMethod()]
        public void NullSecondOperandTestException()
        {
            Mock<IMathOperand>[] mathOperands = MathOperandInstance();
            mathOperands[0].SetupGet(m => m.Value).Returns(1);
            Addition multiplication = new Addition(mathOperands[0].Object, null);
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