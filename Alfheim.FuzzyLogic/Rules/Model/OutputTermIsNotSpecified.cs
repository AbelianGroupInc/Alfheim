using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Alfheim.FuzzyLogic.Rules.Model
{
    public class OutputTermIsNotSpecified : RulesException
    {
        public OutputTermIsNotSpecified(string message) : base(message)
        {
        }

        public OutputTermIsNotSpecified(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OutputTermIsNotSpecified(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
