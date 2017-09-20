using System;

namespace Alfheim.FuzzyLogic.FuzzyFunctionAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ReferencePointAttribute : Attribute
    {
        public ReferencePointAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
