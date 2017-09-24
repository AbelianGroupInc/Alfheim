using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alfheim.FuzzyLogic.Rules.Model
{
    class TermsChainEnumerator : IEnumerator<TermsChainNode>
    {
        private TermsChain chain;
        private int position;    

        private TermsChainNode CurrentNode { get; set; }

        object IEnumerator.Current {
            get
            {
                return CurrentNode;
            }
        }

        public TermsChainNode Current
        {
            get
            {
                return CurrentNode;
            }
        }

        public TermsChainEnumerator(TermsChain chain)
        {
            this.chain = chain;
            CurrentNode = chain.Head;
        }

        public bool MoveNext()
        {
            if (CurrentNode.ChainRuleNextCondition == null)
                return false;
            
            CurrentNode = CurrentNode.ChainRuleNextCondition.Node;
            return true;
        }

        public void Reset()
        {
            CurrentNode = chain.Head;
        }

        public void Dispose()
        {
            
        }
    }
}
