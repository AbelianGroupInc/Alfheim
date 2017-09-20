using Alfheim.FuzzyLogic.FuzzyFunctionAttributes;

namespace Alfheim.FuzzyLogic.Functions
{
    public class TriangleFunction : FuzzyFunction
    {
        private const double cDefaultIndentation = 0.5;

        public TriangleFunction() : base()
        {
            MiddlePoint = cDefaultIndentation * (MinInputValue + MaxInputValue);
            LeftPoint = cDefaultIndentation * (MinInputValue + MiddlePoint);
            RightPoint = cDefaultIndentation * (MiddlePoint + MaxInputValue);
        }

        #region Properties

        [InRangePoint("LeftPoint", "MinInputValue", "MiddlePoint")]
        public double LeftPoint { get; set; }

        [InRangePoint("MiddlePoint", "LeftPoint", "RightPoint")]
        public double MiddlePoint { get; set; }

        [InRangePoint("RightPoint", "MiddlePoint", "MaxInputValue")]
        public double RightPoint { get; set; }

        #endregion

        protected override double Function(double x)
        {
            return x <= MiddlePoint ? 
                LeftFunction(x) : RightFunction(x);
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
