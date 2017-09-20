using System;

namespace Alfheim.FuzzyLogic.FuzzyFunctionAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class InRangePointAttribute : Attribute
    {
        public InRangePointAttribute(string name, string leftPoint, string  rightPoint) 
        {
            Name = name;
            LeftPoint = leftPoint;
            RightPoint = rightPoint;
        }

        public string Name { get; set; }

        public string LeftPoint { get; set; }

        public string RightPoint { get; set; }
    }
}
