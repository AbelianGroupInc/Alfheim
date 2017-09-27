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
        private TermsChain leftSideRule;
        public Term OutputTerm { get; set; }

        public Rule()
        {
        }

        /// <summary>
        /// Chain begins with at least one term (condition). It is needed to add at least one term
        /// </summary>
        /// <param name="firstTerm"></param>
        public void init(Term firstTerm)
        {
            this.leftSideRule = new TermsChain(firstTerm);
        }

        public Rule(Term firstTerm)
        {
            this.leftSideRule = new TermsChain(firstTerm);
        }

        public Rule(Term firstTerm, Term resultingTerm) : this(firstTerm)
        {
            this.OutputTerm = resultingTerm;
        }
        
        public void Add(OperationType operation, Term term)
        {
            leftSideRule.Add(operation, term);
        }


        public void InsertAfter(OperationType operation, Term term, Term newTerm)
        {
            leftSideRule.InsertAfter(operation, term, newTerm);
        }
        public void Remove(Term term)
        {
            leftSideRule.Remove(term);
        }

        public void RemoveLast()
        {
            leftSideRule.RemoveLast();
        }

        public override string ToString()
        {
            string outputTermString = OutputTerm.Variable.Name + " is " + OutputTerm.Name;

            return "If " + leftSideRule.ToString() + " then " + outputTermString;
        }
    }
}
