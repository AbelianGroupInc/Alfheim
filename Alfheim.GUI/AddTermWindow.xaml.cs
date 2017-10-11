using Alfheim.FuzzyLogic;
using Alfheim.FuzzyLogic.Functions;
using Alfheim.FuzzyLogic.Variables.Model;
using Alfheim.GUI.Resources;
using Alfheim.GUI.Windows;
using MahApps.Metro.Controls;
using System.Collections.Generic;
using System.Windows;
using System.Linq;

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

            mCurrentVariable = linguisticVariable;
            InitializeListBox();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var term = TermsFactory.Instance.CreateTermForVariable(mNameTB.Text, mCurrentVariable, GetSelectedFunction());

                Close();
            }
            catch (TermNameAlreadyExistsException)
            {
                ErrorBox.Show(ApplicationStringConstants.NameIsExist);
            }
        }

        private void InitializeListBox()
        {
            mFunctionsCB.Items.Add(new KeyValuePair<string, IFuzzyFunction>(
                ApplicationStringConstants.TriangleFunction, new TriangleFunction(mCurrentVariable.MinValue, mCurrentVariable.MaxValue)));

            mFunctionsCB.Items.Add(new KeyValuePair<string, IFuzzyFunction>(
                ApplicationStringConstants.TrapezoidalFunction, new TrapezoidalFunction(mCurrentVariable.MinValue, mCurrentVariable.MaxValue)));

            mFunctionsCB.Items.Add(new KeyValuePair<string, IFuzzyFunction>(
                ApplicationStringConstants.GaussianFunction, new GaussianFunction(mCurrentVariable.MinValue, mCurrentVariable.MaxValue)));

            mFunctionsCB.SelectedIndex = 0;
        }

        private IFuzzyFunction GetSelectedFunction()
        {
            return ((KeyValuePair<string, IFuzzyFunction>)mFunctionsCB.SelectedItem).Value;
        }
    }
}
