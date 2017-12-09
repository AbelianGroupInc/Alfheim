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

            if (term.Variable.Type == LinguisticVariableType.Input)
                throw new WrongLinguisticVariableTypeException(
                        "Linguistic variable with name: " +
                        term.Variable.Name +
                        "must have output type"
                    );
        }

        public double getMembershipDegree(Dictionary<Term, double> fuzzificatedValues)
        {
            return ruleConditions.getMembershipDegree(fuzzificatedValues);
        }

        public string Stringify()
        {
            if (OutputTerm == null)
                throw new OutputTermIsNotSpecifiedException("Output term is not specified");

            string outputTermString = OutputTerm.Variable.Name + " is " + OutputTerm.Name;

            return "If " + ruleConditions.Stringify() + " then " + outputTermString;
        }

        public override string ToString()
        {
            return "[" +
                GetType() +
                "\n\tRuleConditions: " +
                RuleConditions.ToString() +
                "\n\tOutputTerm: " +
                OutputTerm.ToString() +
                "]";
        }
    }
}
