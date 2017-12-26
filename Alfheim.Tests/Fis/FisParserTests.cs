using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text.RegularExpressions;
using Alfheim.FuzzyLogic;
using Alfheim.FuzzyLogic.Variables.Model;
using Alfheim.FuzzyLogic.Functions;
using Alfheim.FuzzyLogic.Variables.Services;
using Alfheim.FuzzyLogic.Rules.Model;
using Alfheim.FuzzyLogic.Rules.Services;
using Alfheim.FuzzyLogic.Fis;

namespace Alfheim.Tests
{
    /// <summary>
    /// Summary description for FisParser
    /// </summary>
    [TestClass]
    public class FisParserTests
    {
        public FisParserTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
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

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void FisParsingTest()
        {
            //FisParser.Parse("D:\\Study\\Projects\\5.1\\Крапивный\\!FIS\\Lab4-my.fis");

            IEnumerable<Rule> rules = RulesService.Instance.Rules;

            Assert.IsNotNull(RulesService.Instance.Rules);
        }
       
    }
}
