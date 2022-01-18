using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator;
using System;

namespace Calculator.Tests
{
    [TestClass()]
    public class MathOperationTests
    {
        [TestMethod()]
        public void AdditionTest()
        {
            MathOperand[] inputFirstOperand = new MathOperand[] { 1, 25, 255, 153,  0};
            MathOperand[] inputSecondOperand = new MathOperand[] { 1, 25, 255, 107, double.MaxValue};

            double[] expected = new double[] { 2, 50, 510, 260, double.MaxValue };

            double[] actual = new double[inputFirstOperand.Length];
            Addition[] operation = new Addition[inputFirstOperand.Length];

            for (int i = 0; i < inputFirstOperand.Length; i++)
            {
                operation[i] = new Addition(inputFirstOperand[i], inputSecondOperand[i]);
                actual[i] = operation[i].Calculate();
            }

            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod()]
        public void SubtractionTest()
        {
            MathOperand[] inputFirstOperand = new MathOperand[] { 1, 25, 200, 153 };
            MathOperand[] inputSecondOperand = new MathOperand[] { 1, 20, 255, 107 };

            double[] expected = new double[] { 0, 5, -55, 46 };

            double[] actual = new double[inputFirstOperand.Length];
            Subtraction[] operation = new Subtraction[inputFirstOperand.Length];

            for (int i = 0; i < inputFirstOperand.Length; i++)
            {
                operation[i] = new Subtraction(inputFirstOperand[i], inputSecondOperand[i]);
                actual[i] = operation[i].Calculate();
            }

            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod()]
        public void MultiplicationTest()
        {
            MathOperand[] inputFirstOperand = new MathOperand[] { 1, 25, 0, 0.5f };
            MathOperand[] inputSecondOperand = new MathOperand[] { 1, 10, 255, 50 };

            double[] expected = new double[] { 1, 250, 0, 25 };

            double[] actual = new double[inputFirstOperand.Length];
            Multiplication[] operation = new Multiplication[inputFirstOperand.Length];

            for (int i = 0; i < inputFirstOperand.Length; i++)
            {
                operation[i] = new Multiplication(inputFirstOperand[i], inputSecondOperand[i]);
                actual[i] = operation[i].Calculate();
            }

            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod()]
        public void DivisionTest()
        {
            MathOperand[] inputFirstOperand = new MathOperand[] { 1, 25, 0, 1024};
            MathOperand[] inputSecondOperand = new MathOperand[] { 1, 10, 255, 256 };

            double[] expected = new double[] { 1, 2.5f, 0, 4 };

            double[] actual = new double[inputFirstOperand.Length];
            Division[] operation = new Division[inputFirstOperand.Length];

            for (int i = 0; i < inputFirstOperand.Length; i++)
            {
                operation[i] = new Division(inputFirstOperand[i], inputSecondOperand[i]);
                actual[i] = operation[i].Calculate();
            }

            CollectionAssert.AreEqual(expected, actual);
        }


        [ExpectedException(typeof(DivideByZeroException))]
        [TestMethod()]
        public void DivisionTestException()
        {
            Division division = new Division(500, 0);
            division.Calculate();
        }


        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod()]
        public void BinaryExpressionTestException()
        {
            new Addition(25, null);
        }


        [ExpectedException(typeof(OverflowException))]
        [TestMethod()]
        public void AdditionTestException1()
        {
            Addition addition = new Addition(double.MaxValue, 1);
            addition.Calculate();
        }


        [ExpectedException(typeof(OverflowException))]
        [TestMethod()]
        public void AdditionTestException2()
        {
            Addition addition = new Addition(-double.MaxValue * 0.75, -double.MaxValue * 0.75);
            addition.Calculate();
        }


        [ExpectedException(typeof(OverflowException))]
        [TestMethod()]
        public void MultiplicationTestException()
        {
            Multiplication multiplication = new Multiplication(double.MaxValue, 1.1d);
            multiplication.Calculate();
        }
    }
}