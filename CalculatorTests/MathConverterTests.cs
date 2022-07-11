using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathExpression.MathOperations;
using System;
using Moq;

namespace MathExpression.Tests
{
    [TestClass()]
    public class MathConverterTests
    {
        Mock<IMathOperand> mathOperand;
        IMathOperation[][] operationArray;
        Mock<IMathNumberSystem> mathNumberSystem;
        Mock<IMathOperation>[] mathOperation;

        [TestInitialize]
        public void MathConverterTestsStart()
        {
            mathOperand = new Mock<IMathOperand>();
            operationArray = new IMathOperation[2][];
            operationArray[0] = new IMathOperation[2];
            operationArray[1] = new IMathOperation[2];

            mathOperation = new Mock<IMathOperation>[6];
            mathOperation[0] = new Mock<IMathOperation>();
            mathOperation[1] = new Mock<IMathOperation>();
            mathOperation[2] = new Mock<IMathOperation>();
            mathOperation[3] = new Mock<IMathOperation>();
            mathOperation[4] = new Mock<IMathOperation>();
            mathOperation[5] = new Mock<IMathOperation>();

            mathNumberSystem = new Mock<IMathNumberSystem>();
        }

        [TestMethod()]
        public void ExpressionCheckParseTest()
        {
            //Arrange
            string expression = "25-10";
            Mock<IMathOperand> @return = new Mock<IMathOperand>();

            mathOperation[0].Setup(x => x.CheckSymbol(expression, 2)).Returns(false);
            mathOperation[1].Setup(x => x.CheckSymbol(expression, 2)).Returns(true);
            mathOperation[0].Setup(x => x.OperationParse(It.IsAny<IMathExpression>(),
                                                         It.IsAny<IMathOperand>(), expression, 0, 2,
                                                                                   expression.Length-1)).Returns(@return.Object);
            mathOperation[1].Setup(x => x.OperationParse(It.IsAny<IMathExpression>(), 
                                                         It.IsAny<IMathOperand>(), expression, 0, 2, 
                                                                                   expression.Length-1)).Returns(@return.Object);

            operationArray[0] = new IMathOperation[] { mathOperation[0].Object, mathOperation[1].Object };
            MathConverter mathConverter = new MathConverter(mathOperand.Object, mathNumberSystem.Object, operationArray);

            //Act
            mathConverter.ExpressionParse(expression, 0, expression.Length-1);

            //Assert
            mathOperation[0].Verify(x => x.CheckSymbol(expression, 2), Times.Once);
            mathOperation[1].Verify(x => x.CheckSymbol(expression, 2), Times.Once);
            mathOperation[0].Verify(x => x.OperationParse(It.IsAny<IMathExpression>(), It.IsAny<IMathOperand>(), expression, 0, 2, expression.Length - 1), Times.Never);
            mathOperation[1].Verify(x => x.OperationParse(It.IsAny<IMathExpression>(), It.IsAny<IMathOperand>(), expression, 0, 2, expression.Length - 1), Times.Once);
        }

        [TestMethod()]
        public void BracketsSkipExpressionParseTest()
        {
            //Arrange
            string expression = "7-(25-10)*4";
            Mock<IMathOperand> @return = new Mock<IMathOperand>();

            mathOperation[0].Setup(x => x.CheckSymbol(expression, 5)).Returns(true);
            mathOperation[1].Setup(x => x.CheckSymbol(expression, 1)).Returns(true);
            mathOperation[0].Setup(x => x.OperationParse(It.IsAny<IMathExpression>(),
                                                         It.IsAny<IMathOperand>(), expression, 0, 5,
                                                                                   expression.Length-1)).Returns(@return.Object);
            mathOperation[1].Setup(x => x.OperationParse(It.IsAny<IMathExpression>(),
                                                         It.IsAny<IMathOperand>(), expression, 0, 1,
                                                                                   expression.Length-1)).Returns(@return.Object);

            operationArray[0] = new IMathOperation[] { mathOperation[0].Object, mathOperation[1].Object };
            MathConverter mathConverter = new MathConverter(mathOperand.Object, mathNumberSystem.Object, operationArray);

            //Act
            mathConverter.ExpressionParse(expression, 0, expression.Length - 1);

            //Assert
            mathOperation[0].Verify(x => x.CheckSymbol(expression, 5), Times.Never);
            mathOperation[1].Verify(x => x.CheckSymbol(expression, 1), Times.Once);
            mathOperation[0].Verify(x => x.OperationParse(It.IsAny<IMathExpression>(), It.IsAny<IMathOperand>(), expression, 0, 5, expression.Length - 1), Times.Never);
            mathOperation[1].Verify(x => x.OperationParse(It.IsAny<IMathExpression>(), It.IsAny<IMathOperand>(), expression, 0, 1, expression.Length - 1), Times.Once);
        }

        [TestMethod()]
        public void BracketsExpressionParseTest()
        {
            //Arrange
            string expression = "7-(25*(24+65)-(34+2)-10)*4";
            Mock<IMathOperand> @return = new Mock<IMathOperand>();
            mathNumberSystem.Setup(x => x.NumberCheck(expression, It.IsAny<int>(), It.IsAny<int>())).Returns(0);

            mathOperation[0].Setup(x => x.CheckSymbol(expression, 24)).Returns(false);
            mathOperation[1].Setup(x => x.CheckSymbol(expression, 1)).Returns(false);
            mathOperation[2].Setup(x => x.CheckSymbol(expression, 20)).Returns(false);
            mathOperation[3].Setup(x => x.CheckSymbol(expression, 5)).Returns(false);
            mathOperation[4].Setup(x => x.CheckSymbol(expression, 9)).Returns(false);
            mathOperation[5].Setup(x => x.CheckSymbol(expression, 17)).Returns(true);
            mathOperation[5].Setup(x => x.OperationParse(It.IsAny<IMathExpression>(),
                                                         It.IsAny<IMathOperand>(), expression, 0, 17,
                                                                                   expression.Length-1)).Returns(@return.Object);
            operationArray[0] = new IMathOperation[] { mathOperation[0].Object,
                                                       mathOperation[1].Object,
                                                       mathOperation[2].Object };
            operationArray[1] = new IMathOperation[] { mathOperation[3].Object,
                                                       mathOperation[4].Object,
                                                       mathOperation[5].Object };
            MathConverter mathConverter = new MathConverter(mathOperand.Object, mathNumberSystem.Object, operationArray);

            //Act
            mathConverter.ExpressionParse(expression, 0, expression.Length - 1);

            //Assert
            mathOperation[0].Verify(x => x.CheckSymbol(expression, 24), Times.Once);
            mathOperation[1].Verify(x => x.CheckSymbol(expression, 1),  Times.Once);
            mathOperation[2].Verify(x => x.CheckSymbol(expression, 20), Times.Once);
            mathOperation[3].Verify(x => x.CheckSymbol(expression, 5),  Times.Once);
            mathOperation[4].Verify(x => x.CheckSymbol(expression, 9),  Times.Never);
            mathOperation[5].Verify(x => x.CheckSymbol(expression, 17), Times.Once);
            mathOperation[5].Verify(x => x.OperationParse(It.IsAny<IMathExpression>(), It.IsAny<IMathOperand>(), expression, 15, 17, 18), Times.Once);
        }

        [TestMethod()]
        public void NumberCheckExpressionParseTest()
        {
            //Arrange
            string[] expressionCollection = new string[5];
            expressionCollection[0] = "7,98";
            expressionCollection[1] = "457";
            expressionCollection[2] = "=.55687";
            expressionCollection[3] = "= . 589,54";
            expressionCollection[4] = " =-457";
            Mock<IMathOperand> @return = new Mock<IMathOperand>();
            mathOperation[0] = new Mock<IMathOperation>();
            mathOperation[0].Setup(x => x.CheckSymbol(It.IsIn(expressionCollection), It.IsAny<int>())).Returns(false);

            #region NumberCheck
            mathNumberSystem.Setup(x => x.NumberCheck(expressionCollection[0], 0, 3)).Returns(4);
            mathNumberSystem.Setup(x => x.NumberCheck(expressionCollection[1], 0, 2)).Returns(3);
            mathNumberSystem.Setup(x => x.NumberCheck(expressionCollection[2], 0, 6)).Returns(5);
            mathNumberSystem.Setup(x => x.NumberCheck(expressionCollection[3], 0, 9)).Returns(6);
            mathNumberSystem.Setup(x => x.NumberCheck(expressionCollection[4], 0, 5)).Returns(3);
            #endregion

            #region NumberParse
            mathNumberSystem.Setup(x => x.NumberParse(It.IsAny<IMathExpression>(), It.IsAny<IMathOperand>(), expressionCollection[0], 4, 0, 3)).Returns(@return.Object);
            mathNumberSystem.Setup(x => x.NumberParse(It.IsAny<IMathExpression>(), It.IsAny<IMathOperand>(), expressionCollection[1], 3, 0, 2)).Returns(@return.Object);
            mathNumberSystem.Setup(x => x.NumberParse(It.IsAny<IMathExpression>(), It.IsAny<IMathOperand>(), expressionCollection[2], 5, 2, 7)).Returns(@return.Object);
            mathNumberSystem.Setup(x => x.NumberParse(It.IsAny<IMathExpression>(), It.IsAny<IMathOperand>(), expressionCollection[3], 6, 4, 9)).Returns(@return.Object);
            mathNumberSystem.Setup(x => x.NumberParse(It.IsAny<IMathExpression>(), It.IsAny<IMathOperand>(), expressionCollection[4], 3, 3, 5)).Returns(@return.Object);
            #endregion

            operationArray[0] = new IMathOperation[] { mathOperation[0].Object };
            operationArray[1] = new IMathOperation[] { mathOperation[0].Object };
            MathConverter mathConverter = new MathConverter(mathOperand.Object, mathNumberSystem.Object, operationArray);

            //Act
            foreach (var exp in expressionCollection)
            {
                mathConverter.ExpressionParse(exp, 0, exp.Length - 1);
            }

            //Assert
            #region Assert
            mathNumberSystem.Verify(x => x.NumberCheck(expressionCollection[0], 0, 3), Times.Once);
            mathNumberSystem.Verify(x => x.NumberCheck(expressionCollection[1], 0, 2), Times.Once);
            mathNumberSystem.Verify(x => x.NumberCheck(expressionCollection[2], 0, 6), Times.Once);
            mathNumberSystem.Verify(x => x.NumberCheck(expressionCollection[3], 0, 9), Times.Once);
            mathNumberSystem.Verify(x => x.NumberCheck(expressionCollection[4], 0, 5), Times.Once);
            mathNumberSystem.Verify(x => x.NumberParse(It.IsAny<IMathExpression>(), It.IsAny<IMathOperand>(), expressionCollection[0], 4, 0, 3), Times.Once);
            mathNumberSystem.Verify(x => x.NumberParse(It.IsAny<IMathExpression>(), It.IsAny<IMathOperand>(), expressionCollection[1], 3, 0, 2), Times.Once);
            mathNumberSystem.Verify(x => x.NumberParse(It.IsAny<IMathExpression>(), It.IsAny<IMathOperand>(), expressionCollection[2], 5, 0, 6), Times.Once);
            mathNumberSystem.Verify(x => x.NumberParse(It.IsAny<IMathExpression>(), It.IsAny<IMathOperand>(), expressionCollection[3], 6, 0, 9), Times.Once);
            mathNumberSystem.Verify(x => x.NumberParse(It.IsAny<IMathExpression>(), It.IsAny<IMathOperand>(), expressionCollection[4], 3, 0, 5), Times.Once);
            #endregion
        }

        [TestMethod()]
        public void StringToMathConvertTest()
        {
            //Arrange
            string expression = " 7-(25*(24+65) -(34+ 2)-10) *4";
            string expected = "7-(25*(24+65)-(34+2)-10)*4";
            Mock<IMathOperand> @return = new Mock<IMathOperand>();

            mathOperation[0].Setup(x => x.CheckSymbol(expected, It.IsAny<int>())).Returns(true);
            mathOperation[0].Setup(x => x.OperationParse(It.IsAny<IMathExpression>(),
                                                         It.IsAny<IMathOperand>(), expected, 0, expected.Length - 1,
                                                                                   expected.Length - 1)).Returns(@return.Object);
            operationArray[0] = new IMathOperation[] { mathOperation[0].Object, };
            MathConverter mathConverter = new MathConverter(mathOperand.Object, mathNumberSystem.Object, operationArray);

            //Act
            mathConverter.StringToMathConvert(expression);

            //Assert
            mathOperation[0].Verify(x => x.CheckSymbol(expected, It.IsAny<int>()), Times.Once);
        }
    }
}