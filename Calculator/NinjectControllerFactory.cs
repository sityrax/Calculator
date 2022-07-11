using MathExpression.MathOperations;
using MathExpression.MathNumbers;
using System.Collections.Generic;
using MathExpression;

namespace CalculatorUser
{
    sealed class NinjectControllerFactory : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<IMathOperand>().To<MathOperand>();
            Bind<IMathNumberSystem>().To<DecimalNumberSystem>();
            Bind<IReadOnlyList<IMathOperation[]>>().ToConstant(
                           new IMathOperation[][]
                           {
                            new IMathOperation[] { new Addition(),
                                                   new Subtraction()},
                            new IMathOperation[] { new Multiplication(),
                                                   new Division()}
                           });
        }
    }
}
