using Alfheim.FuzzyLogic;
using Alfheim.FuzzyLogic.Functions;
using LiveCharts;
using LiveCharts.Defaults;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alfheim.GUI.Services
{
    public static class FuzzyFunctionToChartValuesConvertor
    {
        private const int cNumberOfSmoothnessFuzzyFunctionPoints = 50;
        private const int cNumberOfPiecewiseLinearFuzzyFunctionPoints = 2000;
        private const int cNumberOfHybridFuzzyFunctionPoints = 200;
        private const double cEps = 1E-7;

        private static ChartValues<ObservablePoint> GetFunctionResult(IFuzzyFunction func)
        {
            var result = new ChartValues<ObservablePoint>();
            var points = FuzzyFunctionToChartValuesConvertor.GetValues(func);

            result.AddRange(points);

            return result;
        }

        public static ChartValues<ObservablePoint> GetValues(IFuzzyFunction function)
        {
            var chartValues = new ChartValues<ObservablePoint>();
            ObservablePoint[] points;

            if (function.Type == FuzzyFunctionType.Smoothness)
                points = GetSmoothnessFunctionValues(function);
            else if (function.Type == FuzzyFunctionType.PiecewiseLinear)
                points = GetPiecewiseLinearFunctionValues(function);
            else
                points = GetHybridFunctionValues(function);

            chartValues.AddRange(points);

            return chartValues;
        }

        private static ObservablePoint[] GetHybridFunctionValues(IFuzzyFunction function)
        {
            return GetValues(function, cNumberOfHybridFuzzyFunctionPoints);
        }

        private static ObservablePoint[] GetPiecewiseLinearFunctionValues(IFuzzyFunction function)
        {
            return OptimazeValues(GetValues(function, cNumberOfPiecewiseLinearFuzzyFunctionPoints));
        }

        private static ObservablePoint[] GetSmoothnessFunctionValues(IFuzzyFunction function)
        {
            return GetValues(function, cNumberOfSmoothnessFuzzyFunctionPoints);
        }

        private static ObservablePoint[] GetValues(IFuzzyFunction function, int numberOfPoints)
        {
            List<ObservablePoint> points = new List<ObservablePoint>();

            double step = (function.MaxInputValue - function.MinInputValue) / (double)numberOfPoints;
            for (double x = function.MinInputValue; x <= function.MaxInputValue; x += step)
                points.Add(new ObservablePoint(x, function.GetValue(x)));

            return points.ToArray();
        }

        private static ObservablePoint[] OptimazeValues(ObservablePoint[] points)
        {
            List<ObservablePoint> result = new List<ObservablePoint>();

            result.Add(points.First());

            for (int i = 1; i < points.Count() - 1; i++)
            {
                Point middle = GetMiddlePoint(points[i - 1], points[i + 1]);

                bool isEqual = points[i] == points[i - 1] && points[i] == points[i + 1];
                bool isMiddle = Math.Abs(points[i].X - middle.X) < cEps && Math.Abs(points[i].Y - middle.Y) < cEps;

                if (!isEqual && !isMiddle)
                    result.Add(points[i]);
            }

            result.Add(points.Last());

            return result.ToArray();
        }

        private static Point GetMiddlePoint(ObservablePoint a, ObservablePoint b)
        {
            return Point.MakePoint((a.X + b.X) / 2.0, (a.Y + b.Y) / 2.0);
        }
    }
}
