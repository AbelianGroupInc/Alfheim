using Alfheim.FuzzyLogic.Variables.Model;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Alfheim.GUI.Views
{
    /// <summary>
    /// Логика взаимодействия для LinguisticVariablePage.xaml
    /// </summary>
    public partial class LinguisticVariablePage : Page
    {
        private Page mOwner;
        private LinguisticVariable mThisVariable;


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
            mXAxis.Separator.Step = ((double)(mThisVariable.MinValue + mThisVariable.MaxValue) / 20.0);
            mXAxis.Separator.StrokeThickness = 2;
        }

        private void OutputListBoxDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void AddTerm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var variable = CreateTerm();

                if (variable != null)
                {
                    mThisVariable.Terms.Add(variable);
                }
            }
            catch(TermNameAlreadyExistsException)
            {
                ShowLinguisticVariableNameAlreadyExistsException();
            }
        }

        private void RemoveTerm_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowLinguisticVariableNameAlreadyExistsException()
        {
            //TODO добавить список констант
            MessageBox.Show((string)this.FindResource("cNameIsExist"),
                    (string)this.FindResource("cError"),
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
        }

        private Term CreateTerm()
        {
            AddTermWindow addVariableWindow = new AddTermWindow();
            addVariableWindow.Owner = Window.GetWindow(this);
            addVariableWindow.ShowDialog();

            return addVariableWindow.Result;
        }
    }
}
