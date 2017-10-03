using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Alfheim.FuzzyLogic.Rules.Model
{
    public class WrongLinguisticVariableTypeException : FuzzyLogicException
    {
        public WrongLinguisticVariableTypeException(string message) : base(message)
        {
        }

        public WrongLinguisticVariableTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WrongLinguisticVariableTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
