using Microsoft.VisualStudio.TestTools.UnitTesting;
using Alfheim.FuzzyLogic.Rules.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alfheim.FuzzyLogic.Variables.Model;
using Alfheim.FuzzyLogic.Functions;
using Alfheim.FuzzyLogic.Rules.Services;
using Alfheim.FuzzyLogic.Variables.Services;

namespace Alfheim.FuzzyLogic.Rules.Model.Tests
{
    [TestClass()]
    public class RuleTests
    {
        [TestMethod()]
        public void RuleTest()
        {
            RulesService.Clear();
            LinguisticVariableService.Clear();

            LinguisticVariable inputVariable1 = new LinguisticVariable("var1", 1, 10);
            LinguisticVariable inputVariable2 = new LinguisticVariable("var2", 1, 10);
            LinguisticVariable inputVariable3 = new LinguisticVariable("var3", 1, 10);

            LinguisticVariable outputVariable = new LinguisticVariable("outputVar", 1, 10);

            Term term1 = TermsFactory.Instance.CreateTermForVariable("term1", inputVariable1, new TrapezoidalFunction());
            Term term2 = TermsFactory.Instance.CreateTermForVariable("term2", inputVariable2, new TrapezoidalFunction());
            Term term3 = TermsFactory.Instance.CreateTermForVariable("term3", inputVariable3, new TrapezoidalFunction());

            Term outputTerm = TermsFactory.Instance.CreateTermForVariable("outputTerm", outputVariable, new TrapezoidalFunction());

            LinguisticVariableService.Instance.Add(inputVariable1, LinguisticVariableType.Input);
            LinguisticVariableService.Instance.Add(inputVariable2, LinguisticVariableType.Input);
            LinguisticVariableService.Instance.Add(inputVariable3, LinguisticVariableType.Input);

            LinguisticVariableService.Instance.Add(outputVariable, LinguisticVariableType.Output);

            // TODO : Variable input/output checking
            RuleBuilder builder = RuleBuilder.CreateBuilder();
            Rule rule = builder
                .Conditions()
                    .ConditionsOperation(OperationType.And)
                    .Add(ConditionSign.Negation, term1)
                    .Add(ConditionSign.Identity, term2)
                    .Add(ConditionSign.Negation, term3)
                    .And()
                .OutputTerm(outputTerm)
                .Build();

            string ruleString = rule.Stringify;

            Assert.AreEqual(ruleString, "If (var1 is not term1) and (var2 is term2) and (var3 is not term3) then outputVar is outputTerm");
        }

        [TestMethod()]
        [ExpectedException(typeof(ConditionsChainIsEmptyException))]
        public void EmptyLeftSideExcpetionTest()
        {
            LinguisticVariable inputVariable1 = new LinguisticVariable("var1", 1, 10);

            LinguisticVariable outputVariable = new LinguisticVariable("outputVar", 1, 10);

            Term outputTerm = TermsFactory.Instance.CreateTermForVariable("outputTerm", outputVariable, new TrapezoidalFunction());
            
            // TODO : Variable input/output checking
            RuleBuilder builder = RuleBuilder.CreateBuilder();
            Rule rule = builder
                .Conditions()
                    .ConditionsOperation(OperationType.And)
                    .And()
                .OutputTerm(outputTerm)
                .Build();
            
        }
        [TestMethod()]
        [ExpectedException(typeof(UndefinedOperationTypeException))]
        public void OperationTypeExceptionTest()
        {
            LinguisticVariable outputVariable = new LinguisticVariable("outputVar1", 1, 10);

            Term outputTerm = TermsFactory.Instance.CreateTermForVariable("outputVar", outputVariable, new TrapezoidalFunction());

            // TODO : Variable input/output checking
            RuleBuilder builder = RuleBuilder.CreateBuilder();
            Rule rule = builder
                .OutputTerm(outputTerm)
                .Build();
            
        }

        [TestMethod()]
        [ExpectedException(typeof(OutputTermIsNotSpecifiedException))]
        public void OutputTermExceptionTest()
        {
            LinguisticVariable inputVariable1 = new LinguisticVariable("var1", 1, 10);
            LinguisticVariable inputVariable2 = new LinguisticVariable("var2", 1, 10);
            LinguisticVariable inputVariable3 = new LinguisticVariable("var3", 1, 10);
            
            Term term1 = TermsFactory.Instance.CreateTermForVariable("term1", inputVariable1, new TrapezoidalFunction());
            Term term2 = TermsFactory.Instance.CreateTermForVariable("term2", inputVariable1, new TrapezoidalFunction());
            Term term3 = TermsFactory.Instance.CreateTermForVariable("term3", inputVariable1, new TrapezoidalFunction());
            
            // TODO : Variable input/output checking
            RuleBuilder builder = RuleBuilder.CreateBuilder();
            Rule rule = builder.Conditions()
                    .ConditionsOperation(OperationType.And)
                    .Add(ConditionSign.Negation, term1)
                    .Add(ConditionSign.Identity, term2)
                    .Add(ConditionSign.Negation, term3)
                    .And()
                .Build();
            
        }

        [TestMethod()]
        [ExpectedException(typeof(TermIsDefinedInRuleException))]
        public void DefinedInRuleInputVariableDeletingExceptionTest()
        {
            RulesService.Clear();
            LinguisticVariableService.Clear();
            
            LinguisticVariable inputVariable1 = new LinguisticVariable("var1", 1, 10);
            LinguisticVariable inputVariable2 = new LinguisticVariable("var2", 1, 10);
            LinguisticVariable inputVariable3 = new LinguisticVariable("var3", 1, 10);

            LinguisticVariable outputVariable = new LinguisticVariable("outputVar", 1, 10);

            Term term1 = TermsFactory.Instance.CreateTermForVariable("term1", inputVariable1, new TrapezoidalFunction());
            Term term2 = TermsFactory.Instance.CreateTermForVariable("term2", inputVariable1, new TrapezoidalFunction());
            Term term3 = TermsFactory.Instance.CreateTermForVariable("term3", inputVariable1, new TrapezoidalFunction());

            Term outputTerm = TermsFactory.Instance.CreateTermForVariable("outputTerm", outputVariable, new TrapezoidalFunction());

            LinguisticVariableService.Instance.Add(inputVariable1, LinguisticVariableType.Input);
            LinguisticVariableService.Instance.Add(inputVariable2, LinguisticVariableType.Input);
            LinguisticVariableService.Instance.Add(inputVariable3, LinguisticVariableType.Input);

            LinguisticVariableService.Instance.Add(outputVariable, LinguisticVariableType.Output);

            // TODO : Variable input/output checking
            RuleBuilder builder = RuleBuilder.CreateBuilder();
            Rule rule = builder
                .Conditions()
                    .ConditionsOperation(OperationType.And)
                    .Add(ConditionSign.Negation, term1)
                    .Add(ConditionSign.Identity, term2)
                    .Add(ConditionSign.Negation, term3)
                    .And()
                .OutputTerm(outputTerm)
                .Build();

            RulesService.Instance.AddRule(rule);

            LinguisticVariableService.Instance.Remove(inputVariable1);
        }

        [TestMethod()]
        [ExpectedException(typeof(TermIsDefinedInRuleException))]
        public void DefinedInRuleOutputVariableDeletingExceptionTest()
        {
            RulesService.Clear();
            LinguisticVariableService.Clear();

            LinguisticVariable inputVariable1 = new LinguisticVariable("var1", 1, 10);
            LinguisticVariable inputVariable2 = new LinguisticVariable("var2", 1, 10);
            LinguisticVariable inputVariable3 = new LinguisticVariable("var3", 1, 10);

            LinguisticVariable outputVariable = new LinguisticVariable("outputVar", 1, 10);

            Term term1 = TermsFactory.Instance.CreateTermForVariable("term1", inputVariable1, new TrapezoidalFunction());
            Term term2 = TermsFactory.Instance.CreateTermForVariable("term2", inputVariable1, new TrapezoidalFunction());
            Term term3 = TermsFactory.Instance.CreateTermForVariable("term3", inputVariable1, new TrapezoidalFunction());

            Term outputTerm = TermsFactory.Instance.CreateTermForVariable("outputTerm", outputVariable, new TrapezoidalFunction());

            LinguisticVariableService.Instance.Add(inputVariable1, LinguisticVariableType.Input);
            LinguisticVariableService.Instance.Add(inputVariable2, LinguisticVariableType.Input);
            LinguisticVariableService.Instance.Add(inputVariable3, LinguisticVariableType.Input);

            LinguisticVariableService.Instance.Add(outputVariable, LinguisticVariableType.Output);

            // TODO : Variable input/output checking
            RuleBuilder builder = RuleBuilder.CreateBuilder();
            Rule rule = builder
                .Conditions()
                    .ConditionsOperation(OperationType.And)
                    .Add(ConditionSign.Negation, term1)
                    .Add(ConditionSign.Identity, term2)
                    .Add(ConditionSign.Negation, term3)
                    .And()
                .OutputTerm(outputTerm)
                .Build();

            RulesService.Instance.AddRule(rule);

            LinguisticVariableService.Instance.Remove(outputVariable);
        }

        [TestMethod()]
        [ExpectedException(typeof(WrongLinguisticVariableTypeException))]
        public void IsLinguisticVariableInputExceptionTest()
        {
            RulesService.Clear();
            LinguisticVariableService.Clear();

            LinguisticVariable inputVariable1 = new LinguisticVariable("var1", 1, 10);
            LinguisticVariable inputVariable2 = new LinguisticVariable("var2", 1, 10);
            LinguisticVariable inputVariable3 = new LinguisticVariable("var3", 1, 10);

            LinguisticVariable outputVariable = new LinguisticVariable("outputVar", 1, 10);

            Term term1 = TermsFactory.Instance.CreateTermForVariable("term1", inputVariable1, new TrapezoidalFunction());
            Term term2 = TermsFactory.Instance.CreateTermForVariable("term2", inputVariable2, new TrapezoidalFunction());
            Term term3 = TermsFactory.Instance.CreateTermForVariable("term3", inputVariable3, new TrapezoidalFunction());

            Term outputTerm = TermsFactory.Instance.CreateTermForVariable("outputTerm", outputVariable, new TrapezoidalFunction());

            LinguisticVariableService.Instance.Add(inputVariable1, LinguisticVariableType.Input);
            LinguisticVariableService.Instance.Add(inputVariable2, LinguisticVariableType.Output);
            LinguisticVariableService.Instance.Add(inputVariable3, LinguisticVariableType.Input);

            LinguisticVariableService.Instance.Add(outputVariable, LinguisticVariableType.Output);

            // TODO : Variable input/output checking
            RuleBuilder builder = RuleBuilder.CreateBuilder();
            Rule rule = builder
                .Conditions()
                    .ConditionsOperation(OperationType.And)
                    .Add(ConditionSign.Negation, term1)
                    .Add(ConditionSign.Identity, term2)
                    .Add(ConditionSign.Negation, term3)
                    .And()
                .OutputTerm(outputTerm)
                .Build();
            
        }

        [TestMethod()]
        [ExpectedException(typeof(WrongLinguisticVariableTypeException))]
        public void IsLinguisticVariableOutputExcpetionTest()
        {
            RulesService.Clear();
            LinguisticVariableService.Clear();

            LinguisticVariable inputVariable1 = new LinguisticVariable("var1", 1, 10);
            LinguisticVariable inputVariable2 = new LinguisticVariable("var2", 1, 10);
            LinguisticVariable inputVariable3 = new LinguisticVariable("var3", 1, 10);

            LinguisticVariable outputVariable = new LinguisticVariable("outputVar", 1, 10);

            Term term1 = TermsFactory.Instance.CreateTermForVariable("term1", inputVariable1, new TrapezoidalFunction());
            Term term2 = TermsFactory.Instance.CreateTermForVariable("term2", inputVariable2, new TrapezoidalFunction());
            Term term3 = TermsFactory.Instance.CreateTermForVariable("term3", inputVariable3, new TrapezoidalFunction());

            Term outputTerm = TermsFactory.Instance.CreateTermForVariable("outputTerm", outputVariable, new TrapezoidalFunction());

            LinguisticVariableService.Instance.Add(inputVariable1, LinguisticVariableType.Input);
            LinguisticVariableService.Instance.Add(inputVariable2, LinguisticVariableType.Input);
            LinguisticVariableService.Instance.Add(inputVariable3, LinguisticVariableType.Input);

            LinguisticVariableService.Instance.Add(outputVariable, LinguisticVariableType.Input);

            // TODO : Variable input/output checking
            RuleBuilder builder = RuleBuilder.CreateBuilder();
            Rule rule = builder
                .Conditions()
                    .ConditionsOperation(OperationType.And)
                    .Add(ConditionSign.Negation, term1)
                    .Add(ConditionSign.Identity, term2)
                    .Add(ConditionSign.Negation, term3)
                    .And()
                .OutputTerm(outputTerm)
                .Build();

        }
    }
}