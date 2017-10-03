using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Alfheim.FuzzyLogic.Variables.Model;
using Alfheim.FuzzyLogic.Variables.Services;
using Alfheim.FuzzyLogic.Functions;

namespace Alfheim.Tests.Variables.Model
{
    /// <summary>
    /// Сводное описание для LinguisticVariableTests
    /// </summary>
    [TestClass]
    public class LinguisticVariableTests
    {
        public LinguisticVariableTests()
        {
            //
            // TODO: добавьте здесь логику конструктора
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Получает или устанавливает контекст теста, в котором предоставляются
        ///сведения о текущем тестовом запуске и обеспечивается его функциональность.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Дополнительные атрибуты тестирования
        //
        // При написании тестов можно использовать следующие дополнительные атрибуты:
        //
        // ClassInitialize используется для выполнения кода до запуска первого теста в классе
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // ClassCleanup используется для выполнения кода после завершения работы всех тестов в классе
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // TestInitialize используется для выполнения кода перед запуском каждого теста 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // TestCleanup используется для выполнения кода после завершения каждого теста
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

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
    }
}
