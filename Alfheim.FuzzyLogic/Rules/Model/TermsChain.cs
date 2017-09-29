using Alfheim.FuzzyLogic.Variables.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alfheim.FuzzyLogic.Rules.Model
{
    public class TermsChain
    {
        private List<TermsChainNode> nodes; 

        public IEnumerable<TermsChainNode> Nodes
        {
            get
            {
                return nodes;
            }
        }

        public Rule ThisRule { get; }

        public OperationType Type { get; set; }

        public TermsChain(OperationType type, Rule rule)
        {
            nodes = new List<TermsChainNode>();
            Type = type;
            this.ThisRule = rule;
        }

        public TermsChain Add(ConditionSign sign,Term term)
        {
            TermsChainNode node = new TermsChainNode(sign, term);
            this.nodes.Add(node);

            return this;
        }

        public TermsChain Remove(Term term)
        {
            TermsChainNode node = null;
            foreach (TermsChainNode curNode in nodes)
                if (curNode.ThisTerm == term)
                    node = curNode;

            if (node == null)
                throw new TermInRuleNotFoundException("Term with name : " + term.Name + " not found");

            this.nodes.Remove(node);

            return this;
        }

        public TermsChain Insert(int index, ConditionSign sign, Term term)
        {
            TermsChainNode node = new TermsChainNode(sign, term); 
            this.nodes.Insert(index, node);

            return this;
        }

        public int IndexOf(ConditionSign sign, Term term)
        {
            TermsChainNode node = new TermsChainNode(sign, term);
            return this.nodes.IndexOf(node);
        }

        public Rule And()
        {
            return this.ThisRule;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < nodes.Count - 1; i++)
                builder
                    .Append(nodes[i])
                    .Append(" ")
                    .Append(Type.ToString().ToLower())
                    .Append(" ");

            builder
                .Append(nodes[nodes.Count - 1]);

            return builder.ToString();
        }
    }
}
