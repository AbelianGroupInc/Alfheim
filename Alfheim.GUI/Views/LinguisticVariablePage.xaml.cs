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

        public LinguisticVariablePage(Page owner)
        {
            InitializeComponent();

            mOwner = owner;

            mPlot.Series = new SeriesCollection
            {
                new LineSeries
                {
                    LineSmoothness = 0,
                    Values = new ChartValues<double> { 0, 0, 1, 0, 0 },
                    PointGeometry = null
                },
            };  
        }

        private void PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as MainWindow).OpenPage(mOwner);
        }
    }
}
