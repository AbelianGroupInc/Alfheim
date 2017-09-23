using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alfheim.FuzzyLogic.Rules.Model
{
    class NextRuleCondition
    {
        public OperationType Operation { get; set; }
        public TermsChainNode Node { get; set; }

        public NextRuleCondition(OperationType nodeOperation, TermsChainNode nextNode)
        {
            Operation = nodeOperation;
            Node = nextNode;
        }
    }
}
