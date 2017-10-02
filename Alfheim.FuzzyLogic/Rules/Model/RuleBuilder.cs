using Alfheim.FuzzyLogic.Variables.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alfheim.FuzzyLogic.Rules.Model
{
    /// <summary>
    /// Common usage
    /// RuleBuilder builder = RuleBuilder.CreateBuilder();
    /// Rule rule = builder
    ///    .Conditions()
    ///        .ConditionsOperation(OperationType.And)
    ///        .Add(ConditionSign.Negation, term1)
    ///        .Add(ConditionSign.Identity, term2)
    ///        .Add(ConditionSign.Negation, term3)
    ///        .And()
    ///    .OutputTerm(outputTerm)
    ///    .Build();
    /// </summary>
    public class RuleBuilder
    {
        private Rule currentRule;
        private TermsChainBuilder conditionsBuilder;
        //public Rule CurrentRule { get; set; }
        //public TermsChainBuilder ConditionsBuilder { get; set; }

        private RuleBuilder()
        {
            this.currentRule = new Rule();
            this.conditionsBuilder = new TermsChainBuilder(this, currentRule.RuleConditions);
        }

        public static RuleBuilder CreateBuilder()
        {
            return new RuleBuilder();
        }

        public TermsChainBuilder Conditions()
        {
            return this.conditionsBuilder;
        }

        public RuleBuilder OutputTerm(Term term)
        {
            this.currentRule.SetOutputTerm(term);

            return this;
        }
         

        public Rule Build()
        {
            Verify();

            return this.currentRule;
        }

        private void Verify()
        {
            if (this.currentRule.RuleConditions.Type == OperationType.Undefined)
                throw new UndefinedOperationTypeException("Type was not specified");

            if (this.currentRule.RuleConditions.Nodes.Count() == 0)
                throw new ConditionsChainIsEmptyException("Conditions chain must not be empty");

            if (this.currentRule.OutputTerm == null)
                throw new OutputTermIsNotSpecifiedException("Output term is not specified");
        }
        

        public class TermsChainBuilder
        {
            RuleBuilder ruleBuilder;
            TermsChain chain;
            public TermsChainBuilder(RuleBuilder builder, TermsChain ruleChain)
            {
                this.ruleBuilder = builder;
                this.chain = ruleChain;
            }

            public TermsChainBuilder ConditionsOperation(OperationType type)
            {
                chain.Type = type;

                return this;
            }

            public TermsChainBuilder Add(ConditionSign sign, Term term)
            {
                chain.Add(sign, term);

                return this;
            }
            public TermsChainBuilder Remove(Term term)
            {
                chain.Remove(term);

                return this;
            }

            public TermsChainBuilder Insert(int index, ConditionSign sign, Term term)
            {
                chain.Insert(index, sign, term);

                return this;
            }

            public RuleBuilder And()
            {
                return this.ruleBuilder;
            }

            
        }
    }
}
