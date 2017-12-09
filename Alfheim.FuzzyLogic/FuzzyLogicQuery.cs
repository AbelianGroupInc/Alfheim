using Accord.Math.Integration;
using Alfheim.FuzzyLogic.Functions;
using Alfheim.FuzzyLogic.Rules.Model;
using Alfheim.FuzzyLogic.Rules.Services;
using Alfheim.FuzzyLogic.Variables.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alfheim.FuzzyLogic
{
    /// <summary>
    /// It has intermediate computations for comfortable output.
    /// Each result of each step of computation is stored inside the class for further using.
    /// </summary>
    class FuzzyLogicQuery
    {
        public Dictionary<LinguisticVariable, double> Input { get; set; }
        public Dictionary<Term, double> FuzzificatedValues { get; set; }
        public Dictionary<Rule, double> RulesLeftSideAggregatedValues { get; set; }
        public List<IFuzzyFunction> CroppedFunctions { get; set; }
        public FuzzyFunction JointFunctions { get; set; }
        public double Output { get; set; }

        public FuzzyLogicQuery(Dictionary<LinguisticVariable, double> input)
        {
            this.Input = input;
            
            FuzzificatedValues = new Dictionary<Term, double>();
            RulesLeftSideAggregatedValues = new Dictionary<Rule, double>();
            CroppedFunctions = new List<IFuzzyFunction>();
    }

        public double Execute()
        {
            Fuzzificate();
            Aggregate();
            Activate();
            Join();
            Defuzzificate();

            return Output;
        }
        private void Fuzzificate()
        {
            Dictionary<Term, double> termsMembershipDegree = new Dictionary<Term, double>();

            foreach (KeyValuePair<LinguisticVariable, double> curInputExplicitData in Input)
            {
                foreach (Term term in curInputExplicitData.Key.Terms)
                {
                    termsMembershipDegree.Add(term, term.GetMembershipDegree(curInputExplicitData.Value));
                }
            }

            FuzzificatedValues = termsMembershipDegree;
        }

        private void Aggregate()
        {
            Dictionary<Rule, double> rulesAggregationValue = new Dictionary<Rule, double>();

            foreach (Rule rule in RulesService.Instance.Rules)
            {
                rulesAggregationValue.Add(rule, rule.getMembershipDegree(FuzzificatedValues));
            }

            RulesLeftSideAggregatedValues = rulesAggregationValue;
        }

        private void Activate()
        {
            List<IFuzzyFunction> activatedFunctions = new List<IFuzzyFunction>();
            foreach (var aggregationRule in RulesLeftSideAggregatedValues)
            {
                Term term = aggregationRule.Key.OutputTerm;
                activatedFunctions.Add(new CroppedFunction(term.FuzzyFunction, aggregationRule.Value));
            }

            CroppedFunctions = activatedFunctions;
        }



        private void Join()
        {
            JointFunctions = new JointFunction(CroppedFunctions);
        }

        private void Defuzzificate()
        {
            Func<double, double> numeratorSubFunction = (x) => x * JointFunctions.GetValue(x);
            Func<double, double> denumeratorSubFunction = (x) => JointFunctions.GetValue(x);
            
            double max = JointFunctions.MaxInputValue;
            double min = JointFunctions.MinInputValue;

            // Integrate!
            double numerator = TrapezoidalRule.Integrate(numeratorSubFunction, min, max, steps: 1000);
            double denumerator = TrapezoidalRule.Integrate(denumeratorSubFunction, min, max, steps: 1000);

            Output = numerator / denumerator;
        }
    }
}
