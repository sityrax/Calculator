#define NINJECT

using System;
using Ninject;
using System.Linq;
using MathExpression;
using static System.Console;
using System.Collections.Generic;
using MathExpression.MathNumbers;
using MathExpression.MathOperations;

namespace CalculatorUser
{
    class Program
    {
        static void Main(string[] args)
        {
#if NINJECT
            #region ArgumentsCreation
            StandardKernel kernel = new StandardKernel(new NinjectControllerFactory());

            IMathOperand       mathOperand      = kernel.Get<IMathOperand>();
            IMathNumberSystem  mathNumberSystem = kernel.Get<IMathNumberSystem>();
            IMathOperation[][] mathOperations   = kernel.Get<IReadOnlyList<IMathOperation[]>>().ToArray();
            #endregion

#else
            IMathOperand mathOperand = new MathOperand();
            IMathNumberSystem mathNumberSystem = new DecimalNumberSystem();
            IMathOperation[][] mathOperations = new IMathOperation[][]
            {
                new IMathOperation[] { new Addition(),
                                       new Subtraction()},
                new IMathOperation[] { new Multiplication(),
                                       new Division()}
            };
#endif

            while (true)
            {
                string input = CheckInput(args);
                string calculationResult;
                try
                {
                    calculationResult = Calculate(mathOperand, mathNumberSystem, mathOperations, input).ToString();
                }
                catch (Exception x)
                {
                    calculationResult = string.Concat(x.Message, "\nPleace, fix the problem and try again.");
                }
                WriteLine(calculationResult);
            }
        }

        static string CheckInput(string[] args)
        {
            if (args.Length == 0)
                return ReadLine();
            else
                return args[0];
        }

        public static double Calculate(IMathOperand mathOperndInstance, IMathNumberSystem mathNumberSystem, IMathOperation[][] mathOperations, string input)
        {
            MathConverter mathConverter = new MathConverter(mathOperndInstance, mathNumberSystem, mathOperations);
            IMathOperand mathOperand = mathConverter.StringToMathConvert(input);
            double expressionResult = mathOperand.Value;
            return expressionResult;
        }
    }
}
