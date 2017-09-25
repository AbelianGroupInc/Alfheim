using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alfheim.FuzzyLogic.Variables.Model
{
    public class TermsFactory
    {
        private static TermsFactory instance;

        public static TermsFactory Instance
        {
            get
            {
                if (instance == null)
                    instance = new TermsFactory();
                return instance;
            }
        }

        private TermsFactory()
        { }

        public Term CreateTermForVariable(string name, LinguisticVariable variable, IFuzzyFunction function)
        {
            Term term = new Term(name);
            term.Variable = variable;
            term.SetFunction(function);

            variable.Terms.Add(term);

            return term;
        }
    }
}
