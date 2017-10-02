using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Alfheim.FuzzyLogic.Rules
{
    public class RulesException : FuzzyLogicException
    {
        public RulesException(string message) : base(message)
        {
        }

        public RulesException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RulesException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
