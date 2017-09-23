using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Alfheim.FuzzyLogic.Variables.Model
{
    public class TermNotFoundException : FuzzyLogicException
    {
        public TermNotFoundException(string message) : base(message)
        {
        }

        public TermNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TermNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
