using Alfheim.FuzzyLogic.FuzzyFunctionAttributes;
using System;

namespace Alfheim.FuzzyLogic.Functions
{
    public class GaussianFunction : FuzzyFunction
    {
        private const double cDefaultIndentation = 0.5;
        private const double cDefaultSteepness = 1;

        #region Constructors

        public GaussianFunction() { }

        public GaussianFunction(double maxInputValue) : base(maxInputValue) { }

        public GaussianFunction(double minInputValue, double maxInputValue) 
            : base(minInputValue, maxInputValue) { }

        public GaussianFunction(double minInputValue, double maxInputValue,
            double minOutputValue, double maxOutputValue) : base(minInputValue, maxInputValue,
                minOutputValue, maxOutputValue) { }

        #endregion

        #region Properties

        [InRangePoint("LeftPoint", "MinInputValue", "MaxInputValue")]
        public double Center { get; set; }

        [ReferencePoint("Steepness")]
        public double Steepness { get; set; }

        #endregion

        protected override double Function(double x)
        {
            return Math.Exp(- Math.Pow((x - Center) / Steepness, 2));
        }

        protected override void InitProperties()
        {
            Center = cDefaultIndentation * (MinInputValue + MaxInputValue);
            Steepness = cDefaultSteepness;
        }
    }
}
