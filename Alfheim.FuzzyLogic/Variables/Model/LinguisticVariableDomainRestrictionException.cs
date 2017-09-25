using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Alfheim.FuzzyLogic.Variables.Model
{
    public class LinguisticVariableDomainRestrictionException : FuzzyLogicException
    {
        public LinguisticVariableDomainRestrictionException(string message) : base(message)
        {
        }

        public LinguisticVariableDomainRestrictionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LinguisticVariableDomainRestrictionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
