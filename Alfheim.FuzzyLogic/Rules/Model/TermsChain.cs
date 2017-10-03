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

        public OperationType Type { get; set; }

        public TermsChain()
        {
            nodes = new List<TermsChainNode>();
            Type = OperationType.Undefined;
        }

        public TermsChain(OperationType type)
        {
            nodes = new List<TermsChainNode>();
            Type = type; 
        }

        public TermsChain Add(ConditionSign sign,Term term)
        {
            TermsChainNode node = new TermsChainNode(sign, term);
            this.nodes.Add(node);

            TermVariableIsInputCheck(term);

            return this;
        }

        public void TermVariableIsInputCheck(Term term)
        {
            if (term.Variable == null)
                throw new LinguisticVariableIsNotSpecifiedException("Variable for term + " + term.Name + " must be specified");

            if (term.Variable.Type == LinguisticVariableType.Output)
                throw new WrongLinguisticVariableTypeException(
                        "Linguistic variable with name: " + 
                        term.Variable.Name + 
                        "must have input type"
                    );
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

        private void TypeIsNotUndefinedCheck()
        {
            if (Type == OperationType.Undefined)
                throw new UndefinedOperationTypeException("Type was not specified");
        }

        public string Stringify()
        {
            TypeIsNotUndefinedCheck();

            if (Nodes.Count() == 0)
                throw new ConditionsChainIsEmptyException("Conditions chain must not be empty");

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

        public bool Contains(Term term)
        {
            return Nodes.FirstOrDefault(curNode => curNode.ThisTerm.Equals(term)) != null;
        }

        public override string ToString()
        {
            return "[" +
                GetType() +
                "\n\t Type: " +
                this.Type.ToString() +
                "\n\t Nodes: " +
                this.Nodes.ToString() +
                "]";
        }
    }
}
