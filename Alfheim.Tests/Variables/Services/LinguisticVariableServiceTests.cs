﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public void LinguisticVariableTest()
        {
            LinguisticVariableService service = new LinguisticVariableService();

            LinguisticVariable variable = new LinguisticVariable("var1", 1, 10);

            TriangleFunction function = new TriangleFunction();
            Term term = new Term("term1", variable);

            Term term2 = new Term("term2", variable);
            Term term3 = new Term("term3", variable);

            variable.AddTerm(term);
            variable.AddTerm(term2);
            variable.AddTerm(term3);

            variable.RemoveTerm(term2);

            service.AddLinguisticVariable(variable, LinguisticVariableType.Input);

            LinguisticVariable variable2 = new LinguisticVariable("var2", 1, 10);

            TriangleFunction function2 = new TriangleFunction();
            Term term21 = new Term("term1", variable);

            Term term22 = new Term("term2", variable);
            Term term23 = new Term("term3", variable);

            variable2.AddTerm(term);
            variable2.AddTerm(term2);
            variable2.AddTerm(term3);

            service.AddLinguisticVariable(variable2, LinguisticVariableType.Input);

            LinguisticVariable variable3 = new LinguisticVariable("var3", 1, 10);

            TriangleFunction function3 = new TriangleFunction();
            Term term31 = new Term("term1", variable);

            Term term32 = new Term("term2", variable);
            Term term33 = new Term("term3", variable);

            variable3.AddTerm(term);
            variable3.AddTerm(term2);
            variable3.AddTerm(term3);
             
            service.AddLinguisticVariable(variable3, LinguisticVariableType.Output);

            IEnumerable<LinguisticVariable> variables = service.InputLinguisticVariables;

            Assert.AreEqual(variables.Count(), 2);
            Assert.IsNotNull(service.GetLinguisticVariable("var2"));

            Assert.AreEqual(service.GetLinguisticVariable("var1").Terms.Count(), 2);
            Assert.IsNotNull(service.GetLinguisticVariable("var1").GetTerm("term3"));

            IEnumerable<LinguisticVariable> variablesOutput = service.OutputLinguisticVariables;

            Assert.AreEqual(variablesOutput.Count(), 1);
            Assert.IsNotNull(service.GetLinguisticVariable("var3"));
        }

        [TestMethod()]
        [ExpectedException(typeof(LinguisticVariableNameAlreadyExistsException))]
        public void LinguisticVariableNameAlreadyExistsException()
        {
            LinguisticVariableService service = new LinguisticVariableService();
            service.AddLinguisticVariable(
                     new LinguisticVariable("var2", 1, 10),
                     LinguisticVariableType.Input
                );

            service.AddLinguisticVariable(
                     new LinguisticVariable("var2", 4, 5),
                     LinguisticVariableType.Input
                );
        }

        [TestMethod()]
        [ExpectedException(typeof(LinguisticVariableNameAlreadyExistsException))]
        public void LinguisticVariableNameAlreadyExistsOutputException()
        {
            LinguisticVariableService service = new LinguisticVariableService();
            service.AddLinguisticVariable(
                     new LinguisticVariable("var2", 1, 10),
                     LinguisticVariableType.Output
                );

            service.AddLinguisticVariable(
                     new LinguisticVariable("var2", 4, 5),
                     LinguisticVariableType.Output
                );
        }

        [TestMethod()]
        [ExpectedException(typeof(LinguisticVariableNameAlreadyExistsException))]
        public void LinguisticVariableNameAlreadyExistsInputOutputException()
        {
            LinguisticVariableService service = new LinguisticVariableService();
            service.AddLinguisticVariable(
                     new LinguisticVariable("var2", 1, 10),
                     LinguisticVariableType.Input
                );

            service.AddLinguisticVariable(
                     new LinguisticVariable("var2", 4, 5),
                     LinguisticVariableType.Output
                );
        }

        [TestMethod()]
        [ExpectedException(typeof(TermNameAlreadyExistsException))]
        public void TermNameAlreadyExistsException()
        {
            LinguisticVariable variable = new LinguisticVariable("var1", 1, 10);

            TriangleFunction function = new TriangleFunction();

            variable.AddTerm(
                    new Term("term1", variable)
                );

            variable.AddTerm(
                   new Term("term1", variable)
               );
             
        }

        [TestMethod()]
        public void TermFunctionDomainTest()
        {
            LinguisticVariable variable = new LinguisticVariable("var1", 1, 10);
            Term term = new Term("term1", variable);

            term.SetFunction<TrapezoidalFunction>();

            Assert.AreEqual(term.FuzzyFunction.MinInputValue, 1);
            Assert.AreEqual(term.FuzzyFunction.MaxInputValue, 10);
        }

        [TestMethod()]
        public void GetLinguisticVariableTest()
        {
            LinguisticVariableService service = new LinguisticVariableService();
            service.AddLinguisticVariable(new LinguisticVariable("var1", 1, 10), LinguisticVariableType.Input);
            service.AddLinguisticVariable(new LinguisticVariable("var2", 1, 10), LinguisticVariableType.Input);
            service.AddLinguisticVariable(new LinguisticVariable("var3", 1, 10), LinguisticVariableType.Input);
            service.AddLinguisticVariable(new LinguisticVariable("var4", 1, 10), LinguisticVariableType.Output);
            service.AddLinguisticVariable(new LinguisticVariable("var5", 1, 10), LinguisticVariableType.Output);

            Assert.IsNotNull(service.GetLinguisticVariable("var1"));
            Assert.IsNotNull(service.GetLinguisticVariable("var5"));
        }

        [TestMethod()]
        [ExpectedException(typeof(LinguisticVariableNotFoundException))]
        public void GetLinguisticVariableTest2()
        {
            LinguisticVariableService service = new LinguisticVariableService();
            service.AddLinguisticVariable(new LinguisticVariable("var1", 1, 10), LinguisticVariableType.Input);
            service.AddLinguisticVariable(new LinguisticVariable("var2", 1, 10), LinguisticVariableType.Input);
            service.AddLinguisticVariable(new LinguisticVariable("var3", 1, 10), LinguisticVariableType.Input);
            service.AddLinguisticVariable(new LinguisticVariable("var4", 1, 10), LinguisticVariableType.Output);
            service.AddLinguisticVariable(new LinguisticVariable("var5", 1, 10), LinguisticVariableType.Output);

            Assert.IsNotNull(service.GetLinguisticVariable("var6"));
        }
    }
}