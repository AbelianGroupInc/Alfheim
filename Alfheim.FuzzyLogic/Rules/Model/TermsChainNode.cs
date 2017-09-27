using Alfheim.FuzzyLogic.Variables.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alfheim.FuzzyLogic.Rules.Model
{
    class TermsChainNode
    {
        public Term ThisTerm { get; set; }
        public NextRuleCondition ChainRuleNextCondition { get; set; }

        public TermsChainNode()
        {
        }

        public TermsChainNode(Term thisTerm)
        {
            ThisTerm = thisTerm;
        }
    }
}
