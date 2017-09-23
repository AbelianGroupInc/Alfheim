using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Alfheim.FuzzyLogic.Rules.Model
{
    public class TermInRuleNotFoundException : FuzzyLogicException
    {
        public TermInRuleNotFoundException(string message) : base(message)
        {
        }

        public TermInRuleNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TermInRuleNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
