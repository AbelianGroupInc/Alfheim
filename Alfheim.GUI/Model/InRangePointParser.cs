﻿using Alfheim.FuzzyLogic;
using Alfheim.FuzzyLogic.FuzzyFunctionAttributes;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Alfheim.GUI.Model
{
    public static class InRangePointParser
    {
        public static IEnumerable<InRangePoint> Parse(IFuzzyFunction function)
        {
            return function.GetType()
                .GetProperties()
                .Where(property => property.GetCustomAttributes(typeof(InRangePointAttribute)).Any())
                .Select(property => new KeyValuePair<string, InRangePointAttribute>(property.Name,
                    property.GetCustomAttribute<InRangePointAttribute>()))
                .Select(attribute => new InRangePoint(attribute.Key, attribute.Value.LeftPoint, attribute.Value.RightPoint)); 
        }
    }
}
