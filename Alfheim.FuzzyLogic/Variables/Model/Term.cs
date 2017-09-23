using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alfheim.FuzzyLogic.Variables.Model
{
    public class Term
    {
        public string Name { get; set; }
        public IFuzzyFunction FuzzyFunction { get; }
        public LinguisticVariable Variable { get;}
        
        public Term(string name, IFuzzyFunction function, LinguisticVariable variable)
        {
            Name = name;
            SetFunction(function);
            Variable = variable;
        }

        public void SetFunction(IFuzzyFunction function)
        {
            // TODO 
        }
    }
}
