using Alfheim.FuzzyLogic.FuzzyFunctionAttributes;

namespace Alfheim.FuzzyLogic.Functions
{
    public class TriangleFunction : FuzzyFunction
    {
        private const double cDefaultIndentation = 0.5;

        #region Constructors

        public TriangleFunction() { }
        public TriangleFunction(double maxInputValue) : base(maxInputValue) { }

        public TriangleFunction(double minInputValue, double maxInputValue)
            : base(minInputValue, maxInputValue) { }

        public TriangleFunction(double minInputValue, double maxInputValue,
            double minOutputValue, double maxOutputValue) : base(minInputValue, maxInputValue,
                minOutputValue, maxOutputValue) { }

        #endregion

        #region Properties

        [InRangePoint("MinInputValue", "MiddlePoint")]
        public double LeftPoint { get; set; }

        [InRangePoint("LeftPoint", "RightPoint")]
        public double MiddlePoint { get; set; }

        [InRangePoint("MiddlePoint", "MaxInputValue")]
        public double RightPoint { get; set; }

        #endregion

        protected override double Function(double x)
        {
            return x < MiddlePoint ? 
                LeftFunction(x) : RightFunction(x);
        }

        protected override void InitProperties()
        {
            MiddlePoint = cDefaultIndentation * (MinInputValue + MaxInputValue);
            LeftPoint = cDefaultIndentation * (MinInputValue + MiddlePoint);
            RightPoint = cDefaultIndentation * (MiddlePoint + MaxInputValue);
        }

        #region Private methods

        private double LeftFunction(double x)
        {
            LinearFunction function = new LinearFunction(Point.MakePoint(LeftPoint, MinOutputValue),
                Point.MakePoint(MiddlePoint, MaxOutputValue));

            return function.GetValue(x);
        }

        private double RightFunction(double x)
        {
            LinearFunction function = new LinearFunction(Point.MakePoint(MiddlePoint, MaxOutputValue),
                Point.MakePoint(RightPoint, MinOutputValue));

            return function.GetValue(x);
        }

        #endregion
    }
}
