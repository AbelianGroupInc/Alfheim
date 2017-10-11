using System;

namespace Alfheim.FuzzyLogic.FuzzyFunctionAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class InRangePointAttribute : Attribute
    {
        public InRangePointAttribute(string leftPoint, string  rightPoint) 
        {
            LeftPoint = leftPoint;
            RightPoint = rightPoint;
        }

        public string LeftPoint { get; set; }

        public string RightPoint { get; set; }
    }
}
