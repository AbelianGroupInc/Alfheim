using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Alfheim.FuzzyLogic.Variables.Model
{
    public class TermIsDefinedInRuleException : FuzzyLogicException
    {
        public TermIsDefinedInRuleException(string message) : base(message)
        {
        }

        public TermIsDefinedInRuleException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TermIsDefinedInRuleException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
