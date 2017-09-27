using Alfheim.FuzzyLogic;
using Alfheim.FuzzyLogic.Variables.Model;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Definitions.Series;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Alfheim.GUI.Services
{
    public class PlotBindingService
    {

        #region Members

        private Dictionary<Term, ISeriesView> mTermDictionary = new Dictionary<Term, ISeriesView>();
        private CartesianChart mPlot;
        private LinguisticVariable mVariable;

        #endregion

        #region Public methods

        public PlotBindingService(CartesianChart plot, LinguisticVariable variable)
        {
            mPlot = plot;
            mVariable = variable;
            
            InitHendlers();
            InitTerms();
        }

        private void InitTerms()
        {
            foreach(var term in mVariable.Terms)
                AddTermOnPlot(term);
        }

        #endregion

        #region Private methods

        private ChartValues<ObservablePoint> GetFunctionResult(IFuzzyFunction func, int smooth)
        {
            var result = new ChartValues<ObservablePoint>();

            double step = (func.MinInputValue + func.MaxInputValue) / (double)smooth;

            for (double x = func.MinInputValue; x <= func.MaxInputValue; x += step)
                result.Add(new ObservablePoint(x, func.GetValue(x)));

            return result;
        }

        private void InitHendlers()
        {
            mVariable.Terms.CollectionChanged += OnCollectionChanged;
        }

        #region Hendlers

        private void OnAdded(IEnumerable<Term> newItems)
        {
            foreach (var item in newItems)
                AddTermOnPlot(item);
        }

        private void AddTermOnPlot(Term term)
        {
            mPlot.Series.Add(
                new LineSeries
                {
                    LineSmoothness = 0,
                    Values = GetFunctionResult(term.FuzzyFunction, 200),// (int)(mPlot.ActualWidth / 2)),
                    PointGeometry = null,
                    Title = term.Name
                });

            mTermDictionary.Add(term, mPlot.Series.Last());
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var collection = (sender as FuzzyLogicObservableCollection<Term>);

            if (collection == null)
                return;

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    OnAdded(e.NewItems.Cast<Term>());
                    break;
                case NotifyCollectionChangedAction.Remove:
                    
                    break;

                case NotifyCollectionChangedAction.Replace:
                    
                    break;
            }
        }

        #endregion
        #endregion
    }
}
