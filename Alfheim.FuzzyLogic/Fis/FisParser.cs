using Alfheim.FuzzyLogic.Functions;
using Alfheim.FuzzyLogic.Rules.Model;
using Alfheim.FuzzyLogic.Rules.Services;
using Alfheim.FuzzyLogic.Variables.Model;
using Alfheim.FuzzyLogic.Variables.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Alfheim.FuzzyLogic.Fis
{
    public class FisParser
    {
        public FisParser()
        {

        }

        public static void Parse(String filename)
        {
            List<List<Term>> InputNamesToIndeces = new List<List<Term>>();
            List<List<Term>> OutputNamesToIndeces = new List<List<Term>>();
            byte[] fisTextFileBytes = File.ReadAllBytes(filename);
            string fisTextFileText = Encoding.GetEncoding("Windows-1251").GetString(fisTextFileBytes);

            Regex rgx = new Regex("\\[System\\][^\\[]*", RegexOptions.IgnoreCase);
            MatchCollection matches = rgx.Matches(fisTextFileText);

            string systemText = matches[0].Value;
            parseSystem(systemText);

            parseInputs(fisTextFileText, InputNamesToIndeces);
            parseOutputs(fisTextFileText, OutputNamesToIndeces);
            parseRules(fisTextFileText, InputNamesToIndeces, OutputNamesToIndeces);

        }

        private static void parseRules(string fisTextFileText, List<List<Term>> InputNamesToIndeces, List<List<Term>> OutputNamesToIndeces)
        {
            Regex rgx =
                new Regex("\\[Rules\\](.|\\n)* ",
                RegexOptions.IgnoreCase
                );

            MatchCollection matches = rgx.Matches(fisTextFileText);
            if (matches.Count != 0)
            {
                string rules = matches[0].Value;

                Regex singleRuleRegex =
                    new Regex("([^,\\n]+), ([\\d\\s]+)",
                    RegexOptions.IgnoreCase
                    );

                MatchCollection singleRuleRegexMatches = singleRuleRegex.Matches(fisTextFileText);
                foreach (Match match in singleRuleRegexMatches)
                {
                    // Correct
                    string inputArrayString = match.Groups[1].Value;
                    string outputArrayString = match.Groups[2].Value;

                    string[] inputArray = inputArrayString.Split(' ');
                    string[] outputArray = outputArrayString.Split(' ');

                    RuleBuilder builder = RuleBuilder.CreateBuilder();
                    RuleBuilder.TermsChainBuilder tcb = builder
                        .Conditions()
                            .ConditionsOperation(OperationType.And); // IMPORTANT

                    for (int i = 0; i < inputArray.Length; i++)
                    {
                        int currentLinguisticVariableTermIndex = 1;
                        Int32.TryParse(inputArray[i], out currentLinguisticVariableTermIndex);
                        tcb
                            .Add(ConditionSign.Identity, InputNamesToIndeces[i][currentLinguisticVariableTermIndex - 1]);

                        //.And()
                    }

                    int outputTermIndex = 1;
                    Int32.TryParse(outputArray[0], out outputTermIndex);
                    Term outputTerm = OutputNamesToIndeces[0][outputTermIndex - 1];

                    Rule newRule = tcb
                        .And()
                        .OutputTerm(outputTerm)
                        .Build();

                    RulesService.Instance.AddRule(newRule);
                }
            }
        }

        private static void parseOutputs(string fisTextFileText, List<List<Term>> OutputNamesToIndeces)
        {
            Regex rgx =
                new Regex("\\[Output(.*?)\\]\nName='(.*?)'\\nRange=\\[(.*?) (.*?)]\\nNumMFs=(.*?)\\n(MF(\\d*)='(.*)':'(.*)',\\[(.*?)\\]\\n)*",
                RegexOptions.IgnoreCase
                );

            MatchCollection matches = rgx.Matches(fisTextFileText);
            ParseBody(rgx, matches, OutputNamesToIndeces, false);
        }



        private static void parseInputs(string fisTextFileText, List<List<Term>> InputNamesToIndeces)
        {
            Regex rgx =
                new Regex("\\[Input(.*?)\\]\nName='(.*?)'\\nRange=\\[(.*?) (.*?)]\\nNumMFs=(.*?)\\n(MF(\\d*)='(.*)':'(.*)',\\[(.*?)\\]\\n)*",
                RegexOptions.IgnoreCase
                );

            MatchCollection matches = rgx.Matches(fisTextFileText);

            ParseBody(rgx, matches, InputNamesToIndeces, true);
        }
        private static void ParseBody(Regex rgx, MatchCollection matches, List<List<Term>> namesToIndeces, bool isInput)
        {
            int curInputIndex = 0;
            foreach (Match match in matches)
            {
                string wholeInputText = match.Value;
                Int32.TryParse(match.Groups[1].Value, out curInputIndex);

                double minValue = 0;
                Double.TryParse(match.Groups[3].Value, out minValue);
                double maxValue = 1;
                Double.TryParse(match.Groups[4].Value, out maxValue);

                LinguisticVariable variable = new LinguisticVariable(
                    match.Groups[2].Value,
                    minValue,
                    maxValue
                );
                namesToIndeces.Add(new List<Term>());
                Regex termsRegex =
                    new Regex("MF(\\d*)='(.*)':'(.*)',\\[(.*?)\\]",
                    RegexOptions.IgnoreCase
                    );

                MatchCollection termsMatches = termsRegex.Matches(wholeInputText);

                List<Term> terms = new List<Term>();

                foreach (Match termMatch in termsMatches)
                {
                    // Correct
                    Term term = TermsFactory.Instance.CreateTermForVariable(
                        termMatch.Groups[2].Value, variable, defineFunction(termMatch)
                        );
                    namesToIndeces[curInputIndex - 1].Add(term);
                }
                if (isInput)
                    LinguisticVariableService.Instance.InputLinguisticVariables.Add(variable);
                else
                    LinguisticVariableService.Instance.OutputLinguisticVariables.Add(variable);
            }
        }

        // TODO : Add constant function and trapeziodal
        private static IFuzzyFunction defineFunction(Match match)
        {
            string value = match.Groups[3].Value;

            string[] functionParameters = match.Groups[4].Value.Split(' ');

            NumberStyles numberStyles = NumberStyles.AllowParentheses |
                NumberStyles.AllowTrailingSign |
                NumberStyles.Float |
                NumberStyles.AllowThousands |
                NumberStyles.AllowExponent;

            if (value == "gaussmf")
            {
                GaussianFunction function = new GaussianFunction();
                function.Center = Double.Parse(functionParameters[1].ToUpper(), numberStyles, CultureInfo.InvariantCulture);
                function.Steepness = Double.Parse(functionParameters[0].ToUpper(), numberStyles, CultureInfo.InvariantCulture);

                return function;
            }
            else if (value == "trimf")
            {
                TriangleFunction function = new TriangleFunction();
                function.LeftPoint = Double.Parse(functionParameters[0].ToUpper(), numberStyles, CultureInfo.InvariantCulture);
                function.MiddlePoint = Double.Parse(functionParameters[1].ToUpper(), numberStyles, CultureInfo.InvariantCulture);
                function.RightPoint = Double.Parse(functionParameters[2].ToUpper(), numberStyles, CultureInfo.InvariantCulture);

                return function;
            }
            else if (value == "constant")
            {
                ConstantFunction function = new ConstantFunction();

                function.ConstValue = Double.Parse(functionParameters[0].ToUpper(), numberStyles);

                return function;
            }
            else if (value == "trapmf")
            {
                TrapezoidalFunction function = new TrapezoidalFunction();

                function.LeftBottomPoint = Double.Parse(functionParameters[0].ToUpper(), numberStyles, CultureInfo.InvariantCulture);
                function.LeftTopPoint = Double.Parse(functionParameters[1].ToUpper(), numberStyles, CultureInfo.InvariantCulture);
                function.RightTopPoint = Double.Parse(functionParameters[2].ToUpper(), numberStyles, CultureInfo.InvariantCulture);
                function.RightBottomPoint = Double.Parse(functionParameters[3].ToUpper(), numberStyles, CultureInfo.InvariantCulture);

                return function;
            }
            else
                throw new FuzzyLogicException("Unknown function");

        }

        private static void parseSystem(string systemText)
        {
            Regex rgx = new Regex("Name='(.*)'", RegexOptions.IgnoreCase);
            MatchCollection matches = rgx.Matches(systemText);

            FuzzyProject.Instance.Name = matches[0].Groups[1].Value;

            Regex rgx2 = new Regex("Version=(.*)", RegexOptions.IgnoreCase);
            MatchCollection matches2 = rgx2.Matches(systemText);
            FuzzyProject.Instance.Version = Double.Parse(matches2[0].Groups[1].Value, CultureInfo.InvariantCulture);
        }
    }
}
