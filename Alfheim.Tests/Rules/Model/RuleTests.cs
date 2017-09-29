using Microsoft.VisualStudio.TestTools.UnitTesting;
using Alfheim.FuzzyLogic.Rules.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alfheim.FuzzyLogic.Variables.Model;
using Alfheim.FuzzyLogic.Functions;

namespace Alfheim.FuzzyLogic.Rules.Model.Tests
{
    [TestClass()]
    public class RuleTests
    {
        [TestMethod()]
        public void RuleTest()
        {
            LinguisticVariable inputVariable1 = new LinguisticVariable("var1", 1, 10);
            LinguisticVariable inputVariable2 = new LinguisticVariable("var2", 1, 10);
            LinguisticVariable inputVariable3 = new LinguisticVariable("var3", 1, 10);

            LinguisticVariable outputVariable = new LinguisticVariable("outputVar1", 1, 10);

            Term term1 = TermsFactory.Instance.CreateTermForVariable("term1", inputVariable1, new TrapezoidalFunction());
            Term term2 = TermsFactory.Instance.CreateTermForVariable("term2", inputVariable1, new TrapezoidalFunction());
            Term term3 = TermsFactory.Instance.CreateTermForVariable("term3", inputVariable1, new TrapezoidalFunction());

            Term outputTerm = TermsFactory.Instance.CreateTermForVariable("outputVar1", inputVariable1, new TrapezoidalFunction());

            OperationType type = OperationType.And;
            Rule rule = new Rule(type);

            // TODO : Variable input/output checking
            rule
               .RuleLeftSide
                   .Add(ConditionSign.Negation, term1)
                   .Add(ConditionSign.Identity, term2)
                   .Add(ConditionSign.Negation, term3)
                .And()
                .SetOutputTerm(outputTerm);

            string ruleString = rule.ToString();
            
            Assert.AreEqual(ruleString, "If (var1 is not term1) and (var1 is term2) and (var1 is not term3) then var1 is outputVar1");
        }
        
    }
}