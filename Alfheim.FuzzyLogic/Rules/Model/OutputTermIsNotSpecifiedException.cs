using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Alfheim.FuzzyLogic.Rules.Model
{
    public class OutputTermIsNotSpecifiedException : RulesException
    {
        public OutputTermIsNotSpecifiedException(string message) : base(message)
        {
        }

        public OutputTermIsNotSpecifiedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OutputTermIsNotSpecifiedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
