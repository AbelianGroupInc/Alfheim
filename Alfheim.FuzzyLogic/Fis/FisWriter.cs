using Alfheim.FuzzyLogic.Functions;
using Alfheim.FuzzyLogic.Rules.Services;
using Alfheim.FuzzyLogic.Variables.Model;
using Alfheim.FuzzyLogic.Variables.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alfheim.FuzzyLogic.Fis
{
    public class FisWriter
    {
        public FisWriter()
        {

        }

        public static void Write(string filename)
        {
            var culture = CultureInfo.CurrentCulture;
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            StringBuilder stringBuilder = new StringBuilder();
            Dictionary<LinguisticVariable, int> variablesToIndeces = new Dictionary<LinguisticVariable, int>();

            stringBuilder
                .AppendLine("[System]")
                .AppendLine($"Name='{FuzzyProject.Instance.Name}'")
                .AppendLine($"Type='mamdani'")
                .AppendLine($"Version={FuzzyProject.Instance.Version?.ToString() ?? "1.0"}")
                .AppendLine($"NumInputs={LinguisticVariableService.Instance.InputLinguisticVariables.Count}")
                .AppendLine($"NumOutputs={LinguisticVariableService.Instance.OutputLinguisticVariables.Count}")
                .AppendLine($"NumRules={RulesService.Instance.Rules.Count()}")
                .AppendLine($"AndMethod='min'")
                .AppendLine($"OrMethod='max'")
                .AppendLine($"ImpMethod='min'")
                .AppendLine($"AggMethod='max'")
                .AppendLine($"DefuzzMethod='centroid'")

                .AppendLine();

            WriteVariables(stringBuilder, true);
            WriteVariables(stringBuilder, false);
            WriteRules(stringBuilder);

            CultureInfo.CurrentCulture = culture;
            File.WriteAllText(filename + ".fis", stringBuilder.ToString());
        }

        private static  void WriteVariables(StringBuilder builder, bool isInput)
        {
            IEnumerable<LinguisticVariable> variables = isInput ?
                LinguisticVariableService.Instance.InputLinguisticVariables :
                LinguisticVariableService.Instance.OutputLinguisticVariables;

            int count = 1;
            foreach (LinguisticVariable var in variables)
            {
                builder
                    .AppendLine(isInput ? $"[Input{count}]" : $"[Output{count}]")
                    .AppendLine($"Name='{var.Name}'")
                    .AppendLine($"Range=[{var.MinValue} {var.MaxValue}]")
                    .AppendLine($"NumMFs={var.Terms.Count}");

                int termCount = 1;
                foreach (var term in var.Terms)
                {
                    StringBuilder functionPropertiesArray = new StringBuilder();
                    string functionName = "";
                    if (term.FuzzyFunction is TrapezoidalFunction)
                    {
                        var rf = term.FuzzyFunction as TrapezoidalFunction;
                        functionPropertiesArray
                            .Append($"{rf.LeftBottomPoint} {rf.LeftTopPoint} {rf.RightTopPoint} {rf.RightBottomPoint}");
                        functionName = "trapmf";
                    }
                    else if (term.FuzzyFunction is TriangleFunction)
                    {
                        var tf = term.FuzzyFunction as TriangleFunction;
                        functionPropertiesArray
                            .Append($"{tf.LeftPoint} {tf.MiddlePoint} {tf.RightPoint}");
                        functionName = "trimf";
                    }
                    else if (term.FuzzyFunction is GaussianFunction)
                    {
                        var gf = term.FuzzyFunction as GaussianFunction;
                        functionPropertiesArray
                            .Append($"{gf.Steepness} {gf.Center}");
                        functionName = "gaussmf";
                    }
                    else if (term.FuzzyFunction is ConstantFunction)
                    {
                        var cf = term.FuzzyFunction as ConstantFunction;
                        functionPropertiesArray
                            .Append($"{cf.ConstValue}");

                        functionName = "constant";
                    }

                    builder
                        .AppendLine($"MF{termCount}='{term.Name}':'{functionName}',[{functionPropertiesArray.ToString()}]");

                    termCount++;
                }

                builder
                    .AppendLine();
                count++;
            }

        }

        private static void WriteRules(StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine("[Rules]");
            foreach (var rule in RulesService.Instance.Rules)
            {
                StringBuilder inputIndeces = new StringBuilder();
                foreach (var var in LinguisticVariableService.Instance.InputLinguisticVariables)
                {
                    Term curTerm = rule.RuleConditions
                        .Nodes
                        .FirstOrDefault(n => n.ThisTerm.Variable.Equals(var))
                        .ThisTerm;

                    if (inputIndeces.Length != 0)
                    {
                        inputIndeces.Append(" ");
                    }

                    if (curTerm == null)
                    {
                        inputIndeces.Append("0");
                    }
                    else
                    {
                        inputIndeces.Append(
                            (var.Terms.ToList().FindIndex(t => t == curTerm) + 1).ToString()
                            );
                    }

                }

                stringBuilder
                    .Append(inputIndeces.ToString())
                    .Append(", ");

                StringBuilder outputIndeces = new StringBuilder();

                foreach (var var in LinguisticVariableService.Instance.OutputLinguisticVariables)
                {

                    if (rule.OutputTerm.Variable.Equals(var))
                    {

                        outputIndeces.Append(
                            (var.Terms.ToList().FindIndex(t => t == rule.OutputTerm) + 1).ToString()
                            );
                        outputIndeces.Append(" ");
                    }

                }

                stringBuilder
                    .Append(outputIndeces)
                    .Append("(1) : 1\n");
            }
        }
    }
}
