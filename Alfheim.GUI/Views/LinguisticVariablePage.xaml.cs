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
            mTitleTB.Text = mThisVariable.Name;

            mXAxis.MinValue = mThisVariable.MinValue;
            mXAxis.MaxValue = mThisVariable.MaxValue;
            mXAxis.Separator.Step = ((double)(mThisVariable.MinValue + mThisVariable.MaxValue) / 20.0);
            mXAxis.Separator.StrokeThickness = 2;
        }
    }
}
