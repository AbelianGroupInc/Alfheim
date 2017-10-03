using Microsoft.VisualStudio.TestTools.UnitTesting;
using Alfheim.FuzzyLogic.Variables.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alfheim.FuzzyLogic.Variables.Model;
using Alfheim.FuzzyLogic.Functions;

namespace Alfheim.FuzzyLogic.Variables.Services.Tests
{
    [TestClass()]
    public class LinguisticVariableServiceTests
    {

        [TestMethod()]
        public void TermConstructionTest()
        {
            LinguisticVariable var = new LinguisticVariable("var", 1, 10);

            Term term = TermsFactory.Instance.CreateTermForVariable("term", var, new TriangleFunction());
            Assert.AreEqual(term.FuzzyFunction.MinInputValue, 1);
            Assert.AreEqual(term.FuzzyFunction.MaxInputValue, 10);
        }

        [TestMethod()] 
        public void TermAssigningTest()
        {
            LinguisticVariable var = new LinguisticVariable("var", 1, 10);
            LinguisticVariable var2 = new LinguisticVariable("var2", 1, 10);

            Term term = TermsFactory.Instance.CreateTermForVariable("term", var, new TriangleFunction());
            Term term2 = TermsFactory.Instance.CreateTermForVariable("term2", var2, new TriangleFunction());

            var.Terms[0] = var2.Terms[0];

        }


        [TestMethod()]
        [ExpectedException(typeof(LinguisticVariableDomainRestrictionException))]
        public void LinguisticVariableDomainRestrictionExceptionTest()
        {
            LinguisticVariable var = new LinguisticVariable("var", 10, 1);
            
        }

        [TestMethod()]
        [ExpectedException(typeof(LinguisticVariableDomainRestrictionException))]
        public void LinguisticVariableDomainRestrictionExceptionMinValueSetterTest()
        {
            LinguisticVariable var = new LinguisticVariable("var", 1, 10);
            var.MinValue = 11;
        }

        [TestMethod()]
        [ExpectedException(typeof(LinguisticVariableDomainRestrictionException))]
        public void LinguisticVariableDomainRestrictionExceptionMaxValueSetterTest()
        {
            LinguisticVariable var = new LinguisticVariable("var", 1, 10);
            var.MaxValue = 0;
        }

        [TestMethod()]
        [ExpectedException(typeof(TermNameAlreadyExistsException))]
        public void TheSameTermNameTest()
        {
            LinguisticVariable variable = new LinguisticVariable("var", 1, 10);
            Term term = TermsFactory.Instance.CreateTermForVariable("term", variable, new TrapezoidalFunction());
            Term term2 = TermsFactory.Instance.CreateTermForVariable("term2", variable, new TrapezoidalFunction());
            Term term3 = TermsFactory.Instance.CreateTermForVariable("term3", variable, new TrapezoidalFunction());

            term2.Name = "term";
        }

        [TestMethod()] 
        public void TheSameTermNameTest2()
        {
            LinguisticVariable variable = new LinguisticVariable("var", 1, 10);
            Term term = TermsFactory.Instance.CreateTermForVariable("term", variable, new TrapezoidalFunction());
            Term term2 = TermsFactory.Instance.CreateTermForVariable("term2", variable, new TrapezoidalFunction());
            Term term3 = TermsFactory.Instance.CreateTermForVariable("term3", variable, new TrapezoidalFunction());

            term2.Name = "term4";
            Assert.AreEqual(term2.Name, "term4");
        }

        [TestMethod()]
        [ExpectedException(typeof(LinguisticVariableNameAlreadyExistsException))]
        public void LinguistVariableTypeTest()
        {
            LinguisticVariable variable = new LinguisticVariable("var", 1, 10);
            Term term = TermsFactory.Instance.CreateTermForVariable("term", variable, new TrapezoidalFunction());
            Term term2 = TermsFactory.Instance.CreateTermForVariable("term2", variable, new TrapezoidalFunction());
            Term term3 = TermsFactory.Instance.CreateTermForVariable("term3", variable, new TrapezoidalFunction());

            LinguisticVariableService.Instance.Add(variable, LinguisticVariableType.Input);
            LinguisticVariableService.Instance.Add(variable, LinguisticVariableType.Output);
        }

        [TestMethod()]
        [ExpectedException(typeof(LinguisticVariableNameAlreadyExistsException))]
        public void LinguistVariableTypeTest2()
        {
            LinguisticVariable variable = new LinguisticVariable("var", 1, 10);
            LinguisticVariable variable2 = new LinguisticVariable("var2", 1, 10);
            Term term = TermsFactory.Instance.CreateTermForVariable("term", variable, new TrapezoidalFunction());
            Term term2 = TermsFactory.Instance.CreateTermForVariable("term2", variable, new TrapezoidalFunction());
            Term term3 = TermsFactory.Instance.CreateTermForVariable("term3", variable2, new TrapezoidalFunction());

            LinguisticVariableService.Instance.Add(variable, LinguisticVariableType.Input);
            LinguisticVariableService.Instance.Add(variable2, LinguisticVariableType.Input);

            variable.Name = "var2";
        }

        [TestMethod()] 
        public void LinguistVariableTypeTest3()
        {
            LinguisticVariableService.Clear();

            LinguisticVariable variable = new LinguisticVariable("var4", 1, 10);

            Assert.AreEqual(variable.Type, LinguisticVariableType.Undefined);

            LinguisticVariable variable2 = new LinguisticVariable("var5", 1, 10);
            Term term = TermsFactory.Instance.CreateTermForVariable("term", variable, new TrapezoidalFunction());
            Term term2 = TermsFactory.Instance.CreateTermForVariable("term2", variable, new TrapezoidalFunction());
            Term term3 = TermsFactory.Instance.CreateTermForVariable("term3", variable2, new TrapezoidalFunction());

            LinguisticVariableService.Instance.Add(variable, LinguisticVariableType.Input);
            LinguisticVariableService.Instance.Add(variable2, LinguisticVariableType.Output);

            Assert.AreEqual(variable.Type, LinguisticVariableType.Input);
            Assert.AreEqual(variable2.Type, LinguisticVariableType.Output);

            Assert.AreEqual(LinguisticVariableService.Instance.InputLinguisticVariables.Count, 1);
            Assert.AreEqual(LinguisticVariableService.Instance.OutputLinguisticVariables.Count, 1);

            variable.Type = LinguisticVariableType.Input;
            Assert.AreEqual(variable.Type, LinguisticVariableType.Input);
            Assert.AreEqual(LinguisticVariableService.Instance.InputLinguisticVariables.Count, 1);
            Assert.AreEqual(LinguisticVariableService.Instance.OutputLinguisticVariables.Count, 1);

            variable.Type = LinguisticVariableType.Output;
            Assert.AreEqual(variable.Type, LinguisticVariableType.Output);
            Assert.AreEqual(LinguisticVariableService.Instance.InputLinguisticVariables.Count, 0);
            Assert.AreEqual(LinguisticVariableService.Instance.OutputLinguisticVariables.Count, 2);

            variable2.Type = LinguisticVariableType.Input;
            Assert.AreEqual(variable.Type, LinguisticVariableType.Output);
            Assert.AreEqual(variable2.Type, LinguisticVariableType.Input);
            Assert.AreEqual(LinguisticVariableService.Instance.InputLinguisticVariables.Count, 1);
            Assert.AreEqual(LinguisticVariableService.Instance.OutputLinguisticVariables.Count, 1);
        }

        [TestMethod()]
        [ExpectedException(typeof(NameIsEmptyException))]
        public void TermNameEmptyExceptionTest()
        {
            Term term = new Term("");
        }

        [TestMethod()]
        [ExpectedException(typeof(NameIsEmptyException))]
        public void TermNameEmptyExceptionTest2()
        {
            Term term = new Term("asdf");

            term.Name = "";
        }

        [TestMethod()]
        [ExpectedException(typeof(NameIsEmptyException))]
        public void LinguisticVariableEmptyNameExceptionTest()
        {
            LinguisticVariable var = new LinguisticVariable("", 0, 10);
        }

        [TestMethod()]
        [ExpectedException(typeof(NameIsEmptyException))]
        public void LinguisticVariableEmptyNameExceptionTest2()
        {
            LinguisticVariable var = new LinguisticVariable("asd", 0, 10);

            var.Name = "";
        }

        [TestMethod()]
        public void LinguisticVariableEqualsTest()
        {
            LinguisticVariable variable = new LinguisticVariable("var", 1, 10);
            LinguisticVariable equalVariable = new LinguisticVariable("var", 1, 10);

            Term term = TermsFactory.Instance.CreateTermForVariable("term", variable, new TrapezoidalFunction());
            Term term2 = TermsFactory.Instance.CreateTermForVariable("term2", variable, new TriangleFunction());
            Term term3 = TermsFactory.Instance.CreateTermForVariable("term3", variable, new GaussianFunction());

            Term equalTerm = TermsFactory.Instance.CreateTermForVariable("term", equalVariable, new TrapezoidalFunction());
            Term equalTerm2 = TermsFactory.Instance.CreateTermForVariable("term2", equalVariable, new TriangleFunction());
            Term equalTerm3 = TermsFactory.Instance.CreateTermForVariable("term3", equalVariable, new GaussianFunction());

            Assert.IsTrue(variable.Equals(equalVariable));
            
            LinguisticVariable notEqualVariable = new LinguisticVariable("var", 1, 10);
            Term notEqualTerm = TermsFactory.Instance.CreateTermForVariable("term", notEqualVariable, new TrapezoidalFunction());
            Term notEqualTerm2 = TermsFactory.Instance.CreateTermForVariable("term2", notEqualVariable, new TriangleFunction());
            Term notEqualTerm3 = TermsFactory.Instance.CreateTermForVariable("term4", notEqualVariable, new GaussianFunction());

            Assert.IsFalse(variable.Equals(notEqualVariable));
        }

        [TestMethod()]
        public void GetLinguisticVariableTypeTest()
        {
            LinguisticVariable variable = new LinguisticVariable("var", 1, 10);
            LinguisticVariable variable2 = new LinguisticVariable("var2", 1, 10);
            LinguisticVariable variable3 = new LinguisticVariable("var3", 1, 10);
            Term term = TermsFactory.Instance.CreateTermForVariable("term", variable, new TrapezoidalFunction());
            Term term2 = TermsFactory.Instance.CreateTermForVariable("term2", variable, new TrapezoidalFunction());
            Term term3 = TermsFactory.Instance.CreateTermForVariable("term3", variable2, new TrapezoidalFunction());
            Term term4 = TermsFactory.Instance.CreateTermForVariable("term4", variable3, new TrapezoidalFunction());

            LinguisticVariableService.Instance.Add(variable, LinguisticVariableType.Input);
            LinguisticVariableService.Instance.Add(variable2, LinguisticVariableType.Input);

            LinguisticVariableService.Instance.Add(variable3, LinguisticVariableType.Output);

            Assert.AreEqual(LinguisticVariableService.Instance.GetLinguisticVariableType(variable), LinguisticVariableType.Input);
            Assert.AreEqual(LinguisticVariableService.Instance.GetLinguisticVariableType(variable2), LinguisticVariableType.Input);
            Assert.AreEqual(LinguisticVariableService.Instance.GetLinguisticVariableType(variable3), LinguisticVariableType.Output);
        }
    }
}