using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;

namespace MathExpression.MathOperations.Tests
{
    [TestClass()]
    public class SubtructionOperationTests
    {
        [TestMethod()]
        public void SubtractionTest()
        {
            //Arrange
            #region inputFirstOperand
            Mock<IMathOperand>[] inputFirstOperand = new Mock<IMathOperand>[5];
            inputFirstOperand[0] = new Mock<IMathOperand>();
            inputFirstOperand[1] = new Mock<IMathOperand>();
            inputFirstOperand[2] = new Mock<IMathOperand>();
            inputFirstOperand[3] = new Mock<IMathOperand>();
            inputFirstOperand[4] = new Mock<IMathOperand>();
            inputFirstOperand[0].SetupGet(x => x.Value).Returns(1);
            inputFirstOperand[1].SetupGet(x => x.Value).Returns(25);
            inputFirstOperand[2].SetupGet(x => x.Value).Returns(200);
            inputFirstOperand[3].SetupGet(x => x.Value).Returns(153);
            #endregion

            #region inputSecondOperand
            Mock<IMathOperand>[] inputSecondOperand = new Mock<IMathOperand>[5];
            inputSecondOperand[0] = new Mock<IMathOperand>();
            inputSecondOperand[1] = new Mock<IMathOperand>();
            inputSecondOperand[2] = new Mock<IMathOperand>();
            inputSecondOperand[3] = new Mock<IMathOperand>();
            inputSecondOperand[4] = new Mock<IMathOperand>();
            inputSecondOperand[0].SetupGet(x => x.Value).Returns(1);
            inputSecondOperand[1].SetupGet(x => x.Value).Returns(20);
            inputSecondOperand[2].SetupGet(x => x.Value).Returns(255);
            inputSecondOperand[3].SetupGet(x => x.Value).Returns(107);
            inputSecondOperand[4].SetupGet(x => x.Value).Returns(5);
            #endregion

            double[] expected = new double[] { 0, 5, -55, 46, -5 };

            double[] actual = new double[inputFirstOperand.Length];
            Subtraction[] operation = new Subtraction[inputFirstOperand.Length];

            //Act
            for (int i = 0; i < inputFirstOperand.Length; i++)
            {
                operation[i] = new Subtraction(inputFirstOperand[i].Object, inputSecondOperand[i].Object);
                actual[i] = operation[i].Calculate();
            }

            //Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SubtractionCheckTest()
        {
            //Arrange
            Subtraction operation = new Subtraction();
            string expression = "1245,67+345-456";

            //Act
            if (!operation.CheckSymbol(expression, 11))
                throw new Exception("Symbol unidentified.");
            if (operation.CheckSymbol(expression, 7))
                throw new Exception("Wrong symbol dentified as correct.");

            //Assert
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void SubtractionOperationParseTest()
        {
            //Arrange
            Subtraction operation = new Subtraction();
            Mock<IMathOperand> MO = new Mock<IMathOperand>();
            Mock<IMathOperand> returned = new Mock<IMathOperand>();
            Mock<IMathExpression> ME = new Mock<IMathExpression>();
            string[] inputs = new string[] { "1245,67-345", "1245,67/25-345*35" };
            int[] indexes = new int[] { inputs[0].IndexOf('-'), inputs[1].IndexOf('-') };
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
            MO.Verify(x => x.CreateInstance(It.IsAny<Subtraction>()), Times.Exactly(2));
        }

        [TestMethod()]
        public void UnaryMinusOperationParseTest()
        {
            //Arrange
            Subtraction operation = new Subtraction();
            Mock<IMathOperand> MO = new Mock<IMathOperand>();
            Mock<IMathOperand> returned = new Mock<IMathOperand>();
            Mock<IMathExpression> ME = new Mock<IMathExpression>();
            string inputs = "-345";
            int indexes = inputs.IndexOf('-');

            ME.Setup(x => x.ExpressionParse(inputs, 1, 3)).Returns(returned.Object);
            ME.Setup(x => x.ExpressionParse(inputs, 0, -1)).Returns(value: null);

            //Act
            operation.OperationParse(ME.Object, MO.Object, inputs, 0, indexes, inputs.Length - 1);

            //Assert
            ME.Verify(x => x.ExpressionParse(inputs, 1, 3), Times.Once);
            ME.Verify(x => x.ExpressionParse(inputs, 0, -1), Times.Once);
            MO.Verify(x => x.CreateInstance(It.IsAny<Subtraction>()), Times.Once);
        }

        [TestMethod()]
        public void SubtractionCreateInstanceTest()
        {
            //Arrange
            Subtraction operation = new Subtraction();
            Mock<IMathOperand> firstOperand = new Mock<IMathOperand>();
            Mock<IMathOperand> secondOperand = new Mock<IMathOperand>();
            IMathOperation mathOperation;
            firstOperand.SetupGet(x => x.Value).Returns(345);
            secondOperand.SetupGet(x => x.Value).Returns(123);

            //Act
            mathOperation = operation.CreateInstance(firstOperand.Object, secondOperand.Object);
            double actual = mathOperation.Calculate();

            //Assert
            firstOperand.Verify(x => x.Value, Times.AtLeastOnce);
            secondOperand.Verify(x => x.Value, Times.AtLeastOnce);
            Assert.AreEqual(firstOperand.Object.Value - secondOperand.Object.Value, actual);
        }

        [TestMethod()]
        public void NullFirstOperandTestException()
        {
            Mock<IMathOperand> mathOperand = new Mock<IMathOperand>();
            mathOperand.SetupGet(m => m.Value).Returns(1);
            Subtraction multiplication = new Subtraction(null, mathOperand.Object);
            multiplication.Calculate();
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestCategory("Exceptions")]
        [TestMethod()]
        public void NullSecondOperandTestException()
        {
            Mock<IMathOperand> mathOperand = new Mock<IMathOperand>();
            mathOperand.SetupGet(m => m.Value).Returns(1);
            Subtraction multiplication = new Subtraction(mathOperand.Object, null);
            multiplication.Calculate();
        }
    }
}