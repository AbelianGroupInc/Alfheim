using Alfheim.FuzzyLogic;
using Alfheim.FuzzyLogic.Functions;
using Alfheim.FuzzyLogic.Variables.Model;
using MahApps.Metro.Controls;
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
using System.Windows.Shapes;

namespace Alfheim.GUI
{
    /// <summary>
    /// Логика взаимодействия для AddTermWindow.xaml
    /// </summary>
    public partial class AddTermWindow : MetroWindow
    {
        public AddTermWindow()
        {
            InitializeComponent();
            InitListBox();
        }

        public bool ShowResult { get; private set; } = false;

        public Term Result { get; private set; }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            ShowResult = true;

            Result = new Term(mNameTB.Text, null);

            Close();
        }

        private void InitListBox()
        {
            mFunctionsCB.Items.Add(new KeyValuePair<string, IFuzzyFunction>(
                (string)FindResource("cTriangleFunction"), new TriangleFunction()));

            mFunctionsCB.Items.Add(new KeyValuePair<string, IFuzzyFunction>(
                (string)FindResource("cTrapezoidalFunction"), new TrapezoidalFunction()));

            mFunctionsCB.Items.Add(new KeyValuePair<string, IFuzzyFunction>(
                (string)FindResource("cGaussianFunction"), new GaussianFunction()));

            mFunctionsCB.SelectedIndex = 0;
        }
    }
}
