using Alfheim.FuzzyLogic.Variables.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Alfheim.FuzzyLogic.Rules.Model
{

    // TODO : Test
    class TermsChain : IEnumerable <TermsChainNode>
    {
        public int Length { get; set; }
        public TermsChainNode Head { get; set; }
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

            if (term == Head.ThisTerm)
                Head = currentNodeNextRuleCondition.Node;
            else
            {
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

            return new TermsChainEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            IEnumerator<TermsChainNode> enumerator = GetEnumerator();
            if (enumerator.Current != null)
            {
                builder.Append(enumerator.Current.ThisTerm.Name);

                if (enumerator.Current.ChainRuleNextCondition != null)
                {
                    AppendNextConditionToStringBuilder(builder, enumerator.Current.ChainRuleNextCondition);

                    while (enumerator.MoveNext() != false)
                    {
                        TermsChainNode node = enumerator.Current;
                        AppendNextConditionToStringBuilder(builder, enumerator.Current.ChainRuleNextCondition);
                    }

                }

            }

             return builder.ToString();
        }

        private void AppendNextConditionToStringBuilder(StringBuilder builder, NextRuleCondition condition)
        {
            builder
                .Append(" ")
                .Append(condition.Operation.ToString())
                .Append(" (")
                .Append(condition.Node.ThisTerm.Variable.Name)
                .Append(" is ")
                .Append(condition.Node.ThisTerm.Name)
                .Append(") ");
        }
    }
}
