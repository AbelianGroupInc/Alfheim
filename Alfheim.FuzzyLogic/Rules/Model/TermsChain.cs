using Alfheim.FuzzyLogic.Variables.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Alfheim.FuzzyLogic.Rules.Model
{
    class TermsChain : IEnumerable <TermsChainNode>
    {
        public int Length { get; set; }
        private TermsChainNode Head { get; set; }
        private TermsChainNode Tail { get; set; }

        public TermsChain(Term firstTerm)
        {
            TermsChainNode newNode = new TermsChainNode(firstTerm);

            Head = newNode;
            Length = 1;
        }

        public void Add(OperationType operation, Term term)
        {
            TermsChainNode newNode = new TermsChainNode(term);

            NextRuleCondition condition = Tail.ChainRuleNextCondition; 

            condition.Node = newNode;
            condition.Operation = operation;
            Tail = newNode;

            Length++;
        }

        public TermsChainNode FindNode(Term term)
        {
            TermsChainNode current = Head;
            while (current != null)
            {
                if (current.ThisTerm == term)
                    return current;
                else
                    current = current.ChainRuleNextCondition.Node;
            }

            return current;
        }

        public void InsertAfter(OperationType operation, Term term, Term newTerm)
        {
            TermsChainNode newNode = new TermsChainNode(newTerm);
            TermsChainNode node = FindNode(term);
            if (node != null)
            {
                newNode.ChainRuleNextCondition.Node = node.ChainRuleNextCondition.Node;
                NextRuleCondition nodeNextRuleCondition = node.ChainRuleNextCondition;

                nodeNextRuleCondition.Operation = operation;
                nodeNextRuleCondition.Node = newNode;
            }
            else
            {
                throw new TermInRuleNotFoundException("Term with name : " + term.Name + " not found");
            }
        }

        public void Remove(Term term)
        {
            TermsChainNode current = Head;

            NextRuleCondition currentNodeNextRuleCondition = current.ChainRuleNextCondition;
            NextRuleCondition nextNodeNextRuleCondition = current.ChainRuleNextCondition.Node.ChainRuleNextCondition;

            while (currentNodeNextRuleCondition.Node != null)
            {
                if (currentNodeNextRuleCondition.Node.ThisTerm == term)
                {
                    currentNodeNextRuleCondition.Operation = currentNodeNextRuleCondition.Operation;
                    currentNodeNextRuleCondition.Node = nextNodeNextRuleCondition.Node;
                    break;
                }
                else
                    current = currentNodeNextRuleCondition.Node;
            }
        }
        public void RemoveLast()
        {
            TermsChainNode current = Head;
            while (current.ChainRuleNextCondition.Node != null)
            {
                if (current.ChainRuleNextCondition.Node == Tail)
                {
                    Tail = current;
                    current.ChainRuleNextCondition = null;
                    break;
                }
                else
                    current = current.ChainRuleNextCondition.Node;
            }
        }

        public IEnumerator<TermsChainNode> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
