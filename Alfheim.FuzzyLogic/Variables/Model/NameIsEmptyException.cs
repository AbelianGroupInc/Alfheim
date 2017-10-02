using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Alfheim.FuzzyLogic.Variables.Model
{
    public class NameIsEmptyException : FuzzyLogicException
    {
        public NameIsEmptyException(string message) : base(message)
        {
        }

        public NameIsEmptyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NameIsEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
