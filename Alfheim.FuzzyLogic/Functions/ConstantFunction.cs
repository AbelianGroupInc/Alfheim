using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alfheim.FuzzyLogic.Functions
{
    public class ConstantFunction : FuzzyFunction
    {

        public double ConstValue { get; set; }

        public ConstantFunction()
        {

        }

        public ConstantFunction(int value)
        {
            ConstValue = value;
        }

        protected override double Function(double x)
        {
            return ConstValue;
        }

        protected override void InitProperties()
        {
            Type = FuzzyFunctionType.PiecewiseLinear;
        }
    }
}
