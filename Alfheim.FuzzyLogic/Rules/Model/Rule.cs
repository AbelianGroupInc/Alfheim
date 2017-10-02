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
        private TermsChain ruleConditions;
        private Term outputTerm;
        
        public TermsChain RuleConditions {
            get
            {
                return ruleConditions;
            }
        }
        public Term OutputTerm
        {
            get
            {
                return outputTerm;
            }
        }

        public Rule()
        {
            ruleConditions = new TermsChain();
        }

        public Rule(OperationType type)
        {
            ruleConditions = new TermsChain(type);
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
            if (OutputTerm == null)
                throw new OutputTermIsNotSpecified("Output term is not specified");

            string outputTermString = OutputTerm.Variable.Name + " is " + OutputTerm.Name;

            return "If " + ruleConditions.ToString() + " then " + outputTermString;
        }
    }
}
