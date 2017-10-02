using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Alfheim.FuzzyLogic.Rules.Model
{
    public class ConditionsChainIsEmptyException : RulesException
    {
        public ConditionsChainIsEmptyException(string message) : base(message)
        {
        }

        public ConditionsChainIsEmptyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ConditionsChainIsEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
