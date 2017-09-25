using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Alfheim.FuzzyLogic.Variables.Model
{
    public class LinguisticVariableIsNotSpecifiedException : FuzzyLogicException
    {
        public LinguisticVariableIsNotSpecifiedException(string message) : base(message)
        {
        }

        public LinguisticVariableIsNotSpecifiedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LinguisticVariableIsNotSpecifiedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
