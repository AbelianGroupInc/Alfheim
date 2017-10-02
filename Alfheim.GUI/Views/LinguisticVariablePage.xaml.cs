using Alfheim.FuzzyLogic;
using Alfheim.FuzzyLogic.Functions;
using Alfheim.FuzzyLogic.FuzzyFunctionAttributes;
using Alfheim.FuzzyLogic.Variables.Model;
using Alfheim.GUI.Resources;
using Alfheim.GUI.Services;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro;
using System.ComponentModel;

namespace Alfheim.GUI.Views
{
    /// <summary>
    /// Логика взаимодействия для LinguisticVariablePage.xaml
    /// </summary>
    public partial class LinguisticVariablePage : Page
    {
        private Page mOwner;
        private LinguisticVariable mThisVariable;

        private PlotBindingService mPlotBindingService;
        private TermPropertyViewService mTermPropertyViewService;

        public LinguisticVariablePage(Page owner, LinguisticVariable variable)
        {
            InitializeComponent();

            mOwner = owner;
            mThisVariable = variable;
            Init();
        }

        private void PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as MainWindow).OpenPage(mOwner);
        }

        private void Init()
        {
            mTermList.ItemsSource = mThisVariable.Terms;
            mTitleTB.Text = mThisVariable.Name;

            mXAxis.MinValue = mThisVariable.MinValue;
            mXAxis.MaxValue = mThisVariable.MaxValue;
            mXAxis.Separator.Step = ((double)(mThisVariable.MinValue + mThisVariable.MaxValue) / 10.0);
            mXAxis.Separator.StrokeThickness = 2;

            mPlotBindingService = new PlotBindingService(mPlot, mThisVariable);

            mTermPropertyViewService = new TermPropertyViewService(mStackPanel);
            mTermPropertyViewService.TermChanged += OnTermChanged;
        }

        private void OutputListBoxDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void AddTerm_Click(object sender, RoutedEventArgs e)
        {
            CreateTerm();
        }

        private void RemoveTerm_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CreateTerm()
        {
            AddTermWindow addVariableWindow = new AddTermWindow(mThisVariable);
            addVariableWindow.Owner = Window.GetWindow(this);
            addVariableWindow.ShowDialog();
        }

        private void TermList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var term = (mTermList.SelectedItem as Term);

            if (term == null)
                return;

            mTermPropertyViewService.ShowTermProperties(term);
        }

        private void OnTermChanged(object sender, PropertyChangedEventArgs e)
        {
            Term term = (sender as Term);

            if (term == null)
                return;

            mPlotBindingService.UpdateTerm(term, e.PropertyName);

            mTermList.Items.Refresh();
        }
    }
}
