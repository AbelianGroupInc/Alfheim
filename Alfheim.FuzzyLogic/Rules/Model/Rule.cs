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
        public Term ResultingTerm { get; set; }

        public Rule(Term firstTerm)
        {
            this.leftSideRule = new TermsChain(firstTerm);
        }

        public Rule(Term firstTerm, Term resultingTerm) : this(firstTerm)
        {
            this.ResultingTerm = resultingTerm;
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
            return "TODO";
        }
    }
}
