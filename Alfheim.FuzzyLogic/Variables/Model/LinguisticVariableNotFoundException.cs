using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Alfheim.FuzzyLogic.Variables.Model
{
    public class LinguisticVariableNotFoundException : FuzzyLogicException
    {
        public LinguisticVariableNotFoundException(string message) : base(message)
        {
        }

        public LinguisticVariableNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LinguisticVariableNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
