using Alfheim.FuzzyLogic.Variables.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alfheim.FuzzyLogic.Rules.Model
{
    public class TermsChainNode
    {
        public ConditionSign Sign { get; set; }
        public Term ThisTerm { get; set; }

        public TermsChainNode(ConditionSign type, Term thisTerm)
        {
            Sign = type;
            ThisTerm = thisTerm;
        }

        public TermsChainNode(Term thisTerm)
        {
            Sign = ConditionSign.Identity;
            ThisTerm = thisTerm;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder
                .Append("(")
                .Append(ThisTerm.Variable.Name)
                .Append(" is ");

            if (Sign == ConditionSign.Negation)
                builder
                    .Append("not ");

            builder
                .Append(ThisTerm.Name)
                .Append(")");

            return builder.ToString();
        }
    }
}
