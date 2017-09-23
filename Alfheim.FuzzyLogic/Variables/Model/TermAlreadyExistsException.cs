using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Alfheim.FuzzyLogic.Variables.Model
{
    public class TermNameAlreadyExistsException : FuzzyLogicException
    {
        public TermNameAlreadyExistsException(string message) : base(message)
        {
        }

        public TermNameAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TermNameAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
