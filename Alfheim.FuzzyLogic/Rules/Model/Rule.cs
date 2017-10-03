using Alfheim.FuzzyLogic.Variables.Model;
using Alfheim.FuzzyLogic.Variables.Services;
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
            TermVariableIsOutputCheck(term);

            this.outputTerm = term;
            return this;
        }
        
        public void TermVariableIsOutputCheck(Term term)
        {
            if (term.Variable == null)
                throw new LinguisticVariableIsNotSpecifiedException(
                    "Variable of term with name: " +
                    term.Name +
                    " must be specified"
                    );

            LinguisticVariable variable = term.Variable;
            

            //TODO Is Variable specified
        }

        public override string ToString()
        {
            if (OutputTerm == null)
                throw new OutputTermIsNotSpecifiedException("Output term is not specified");

            string outputTermString = OutputTerm.Variable.Name + " is " + OutputTerm.Name;

            return "If " + ruleConditions.ToString() + " then " + outputTermString;
        }
    }
}
