using Alfheim.FuzzyLogic;
using Alfheim.FuzzyLogic.Functions;
using Alfheim.FuzzyLogic.Variables.Model;
using Alfheim.GUI.Resources;
using Alfheim.GUI.Windows;
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
        LinguisticVariable mCurrentVariable;

        public AddTermWindow(LinguisticVariable linguisticVariable)
        {
            InitializeComponent();
            InitializeListBox();

            mCurrentVariable = linguisticVariable;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Term term = new Term(mNameTB.Text, mCurrentVariable);
                term.SetFunction(GetSelectedFunction());

                mCurrentVariable.Terms.Add(term);
            }
            catch (TermNameAlreadyExistsException)
            {
                ErrorBox.Show(ApplicationStringConstants.NameIsExist);
            }

            Close();
        }

        private void InitializeListBox()
        {
            mFunctionsCB.Items.Add(new KeyValuePair<string, IFuzzyFunction>(
                ApplicationStringConstants.TriangleFunction, new TriangleFunction()));

            mFunctionsCB.Items.Add(new KeyValuePair<string, IFuzzyFunction>(
                ApplicationStringConstants.TrapezoidalFunction, new TrapezoidalFunction()));

            mFunctionsCB.Items.Add(new KeyValuePair<string, IFuzzyFunction>(
                ApplicationStringConstants.GaussianFunction, new GaussianFunction()));

            mFunctionsCB.SelectedIndex = 0;
        }

        private IFuzzyFunction GetSelectedFunction()
        {
            return ((KeyValuePair<string, IFuzzyFunction>)mFunctionsCB.SelectedItem).Value;
        }
    }
}
