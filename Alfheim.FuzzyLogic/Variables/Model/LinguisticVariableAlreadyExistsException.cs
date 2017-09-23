using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Alfheim.FuzzyLogic.Variables.Model
{
    public class LinguisticVariableNameAlreadyExistsException : FuzzyLogicException
    {
        public LinguisticVariableNameAlreadyExistsException(string message) : base(message)
        {
        }

        public LinguisticVariableNameAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LinguisticVariableNameAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
