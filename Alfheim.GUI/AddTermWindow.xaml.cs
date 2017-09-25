using Alfheim.FuzzyLogic;
using Alfheim.FuzzyLogic.Functions;
using Alfheim.FuzzyLogic.Variables.Model;
using Alfheim.GUI.Resources;
using Alfheim.GUI.Windows;
using MahApps.Metro.Controls;
using System.Collections.Generic;
using System.Windows;

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
                TermsFactory.Instance.CreateTermForVariable(mNameTB.Text, mCurrentVariable, GetSelectedFunction());

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
