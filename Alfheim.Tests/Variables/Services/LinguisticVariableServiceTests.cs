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