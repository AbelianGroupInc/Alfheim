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
    /// Логика взаимодействия для AddLinguisticVariable.xaml
    /// </summary>
    public partial class AddLinguisticVariableWindow : MetroWindow
    {
        public AddLinguisticVariableWindow()
        {
            InitializeComponent();
        }

        public bool ShowResult { get; private set; } = false;

        public LinguisticVariable Result { get; private set; }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            ShowResult = true;

            Result = new LinguisticVariable(
                mNameTB.Text,
                Convert.ToDouble(mMinValueTB.Text),
                Convert.ToDouble(mMaxValueTB.Text));

            Close();
        }
    }
}
