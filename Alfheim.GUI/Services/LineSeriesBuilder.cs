using Alfheim.FuzzyLogic.Variables.Model;
using LiveCharts.Wpf;

namespace Alfheim.GUI.Services
{
    public static class LineSeriesBuilder
    {
        public static LineSeries CreateLineSeries(Term term)
        {
            return new LineSeries
            {
                LineSmoothness = term.FuzzyFunction.Type  == 
                    FuzzyLogic.Functions.FuzzyFunctionType.Smoothness? 1 : 0,
                Values = FuzzyFunctionToChartValuesConvertor.GetValues(term.FuzzyFunction),
                PointGeometry = null,
                Title = term.Name
            };
        }
    }
}
