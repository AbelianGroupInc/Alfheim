using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Alfheim.FuzzyLogic
{
    public class FuzzyLogicException : Exception
    {
        public FuzzyLogicException(string message) : base(message)
        {
        }

        public FuzzyLogicException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FuzzyLogicException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
