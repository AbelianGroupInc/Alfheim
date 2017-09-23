namespace Alfheim.FuzzyLogic.Functions
{
    public class LinearFunction : IFunction
    {
        public LinearFunction(Point leftPoint, Point rightPoint)
        {
            LeftPoint = leftPoint;
            RightPoint = rightPoint;
        }

        public Point LeftPoint { get; set; }

        public Point RightPoint { get; set; }

        public double GetValue(double x)
        {
            return ((RightPoint.X * LeftPoint.Y - LeftPoint.X *
                RightPoint.Y - x * (LeftPoint.Y - RightPoint.Y))) / 
                (RightPoint.X - LeftPoint.X);
        }
    }
}
