using Alfheim.FuzzyLogic.Variables.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alfheim.FuzzyLogic.Rules.Model
{
    public class Rule
    {
        private TermsChain ruleLeftSide;
        private Term outputTerm;

        public TermsChain RuleLeftSide {
            get
            {
                return ruleLeftSide;
            }
        }
        public Term OutputTerm
        {
            get
            {
                return outputTerm;
            }
        }

        public Rule(OperationType type)
        {
            ruleLeftSide = new TermsChain(type, this);
        }
        

        public Rule(OperationType type, Term outputTerm) : this(type)
        {
            this.outputTerm = outputTerm;
        }

        public Rule SetOutputTerm(Term term)
        {
            this.outputTerm = term;
            return this;
        }        

        public override string ToString()
        {
            string outputTermString = OutputTerm.Variable.Name + " is " + OutputTerm.Name;

            return "If " + ruleLeftSide.ToString() + " then " + outputTermString;
        }
    }
}
