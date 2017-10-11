using Alfheim.FuzzyLogic;
using Alfheim.FuzzyLogic.FuzzyFunctionAttributes;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Alfheim.GUI.Model
{
    public static class ReferencePointParser
    {
        public static IEnumerable<ReferencePoint> Parse(IFuzzyFunction function)
        {
            return function.GetType()
                .GetProperties()
                .Where(property => property.GetCustomAttributes(typeof(ReferencePointAttribute)).Any())
                .Select(property => new ReferencePoint(property.Name));
        }
    }
}
