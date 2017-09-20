namespace Alfheim.FuzzyLogic.Functions
{
    public class Point
    {
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }

        public double Y { get; set; }

        public static Point MakePoint(double x, double y)
        {
            return new Point(x, y);
        }
    }
}
