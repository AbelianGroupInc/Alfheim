//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Alfheim.FuzzyLogic.Rules.Model
//{
//    class TermsChainEnumerator : IEnumerator<TermsChainListNode>
//    {
//        private TermsChainList chain;
//        private int position;    

//        private TermsChainListNode CurrentNode { get; set; }

//        object IEnumerator.Current {
//            get
//            {
//                return CurrentNode;
//            }
//        }

//        public TermsChainListNode Current
//        {
//            get
//            {
//                return CurrentNode;
//            }
//        }

//        public TermsChainEnumerator(TermsChainList chain)
//        {
//            this.chain = chain;
//            CurrentNode = chain.Head;
//        }

//        public bool MoveNext()
//        {
//            if (CurrentNode.ChainRuleNextCondition == null)
//                return false;
            
//            CurrentNode = CurrentNode.ChainRuleNextCondition.Node;
//            return true;
//        }

//        public void Reset()
//        {
//            CurrentNode = chain.Head;
//        }

//        public void Dispose()
//        {
            
//        }
//    }
//}
