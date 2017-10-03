using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Alfheim.FuzzyLogic.Variables.Model;
using Alfheim.FuzzyLogic.Functions;

namespace Alfheim.Tests.Variables.Model
{
    /// <summary>
    /// Сводное описание для TermTets
    /// </summary>
    [TestClass]
    public class TermTests
    {
        public TermTests()
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


    }
}
