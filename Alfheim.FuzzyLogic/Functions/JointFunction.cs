using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alfheim.FuzzyLogic.Functions
{
    public class JointFunction : FuzzyFunction
    {
        public List<IFuzzyFunction> JointFunctions { get; set; }

        public JointFunction(List<IFuzzyFunction> jointFunctions)
        {
            this.JointFunctions = jointFunctions; 
        }
        public new double MinInputValue
        {
            get
            {
                return JointFunctions.Min(f => f.MinInputValue);
            }
            set
            {
                JointFunctions.ForEach(f => MinInputValue = value);
            }
        }
        public new double MaxInputValue
        {
            get
            {
                return JointFunctions.Max(f => f.MaxInputValue);
            }
            set
            {
                JointFunctions.ForEach(f => MaxInputValue = value);
            }
        }

        public new double MaxOutputValue
        {
            get
            {
                return JointFunctions.Max(f => f.MaxOutputValue); 
            }
            set
            {
                JointFunctions.ForEach(f => MaxOutputValue = value);
            }
        }
        public new double MinOutputValue
        {
            get
            {
                return JointFunctions.Min(f => f.MinOutputValue);
            }
            set
            {
                JointFunctions.ForEach(f => MinOutputValue = value);
            }
        }
        public new FuzzyFunctionType Type
        {
            get
            {
                return FuzzyFunctionType.Undefined;
            }
        }

        protected double GetValue(double x)
        {
            
            return JointFunctions.Max(f => f.GetValue(x));
        }

        protected override double Function(double x)
        {
            return GetValue(x);
        }

        protected override void InitProperties()
        {
        }
    }
}