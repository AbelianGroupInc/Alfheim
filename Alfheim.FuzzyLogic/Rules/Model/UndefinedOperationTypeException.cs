using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Alfheim.FuzzyLogic.Rules.Model
{
    public class UndefinedOperationTypeException : RulesException
    {
        public UndefinedOperationTypeException(string message) : base(message)
        {
        }

        public UndefinedOperationTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UndefinedOperationTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
