using Alfheim.FuzzyLogic.FuzzyFunctionAttributes;

namespace Alfheim.FuzzyLogic.Functions
{
    public class TrapezoidalFunction : FuzzyFunction
    {
        private const double cLowdentation = 0.25;
        private const double cHightIndentation = 0.75;

        #region Constructors

        public TrapezoidalFunction() { }

        public TrapezoidalFunction(double maxInputValue) : base(maxInputValue) { }

        public TrapezoidalFunction(double minInputValue, double maxInputValue) 
            : base(minInputValue, maxInputValue) { }

        public TrapezoidalFunction(double minInputValue, double maxInputValue,
            double minOutputValue, double maxOutputValue) : base(minInputValue, maxInputValue,
                minOutputValue, maxOutputValue) { }

        #endregion

        #region Properties

        [InRangePoint("LeftBottomPoint", "MinInputValue", "LeftTopPoint")]
        public double LeftBottomPoint { get; set; }

        [InRangePoint("LeftTopPoint", "LeftBottomPoint", "RightTopPoint")]
        public double LeftTopPoint { get; set; }

        [InRangePoint("RightBottomPoint", "RightTopPoint", "MaxInputValue")]
        public double RightBottomPoint { get; set; }

        [InRangePoint("RightTopPoint", "LeftTopPoint", "RightBottomPoint")]
        public double RightTopPoint { get; set; }

        #endregion

        protected override void InitProperties()
        {
            Type = FuzzyFunctionType.PiecewiseLinear;

            LeftBottomPoint = cLowdentation * (MinInputValue + MaxInputValue);
            RightBottomPoint = cHightIndentation * (MinInputValue + MaxInputValue);

            LeftTopPoint = cLowdentation * (LeftBottomPoint + RightBottomPoint);
            RightTopPoint = cHightIndentation * (LeftBottomPoint + RightBottomPoint);
        }

        protected override double Function(double x)
        {
            return x < RightTopPoint ?
                LeftFunction(x) : RightFunction(x);
        }

        #region Private methods

        private double LeftFunction(double x)
        {
            LinearFunction function = new LinearFunction(Point.MakePoint(LeftBottomPoint, MinOutputValue),
                Point.MakePoint(LeftTopPoint, MaxOutputValue));

            return function.GetValue(x);
        }

        private double RightFunction(double x)
        {
            LinearFunction function = new LinearFunction(Point.MakePoint(RightTopPoint, MaxOutputValue),
                Point.MakePoint(RightBottomPoint, MinOutputValue));

            return function.GetValue(x);
        }

        #endregion
    }
}
