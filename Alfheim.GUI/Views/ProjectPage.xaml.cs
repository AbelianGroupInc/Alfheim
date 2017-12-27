using Alfheim.FuzzyLogic.Rules.Model;
using Alfheim.FuzzyLogic.Rules.Services;
using Alfheim.FuzzyLogic.Variables.Model;
using Alfheim.FuzzyLogic.Variables.Services;
using Alfheim.GUI.Controls;
using Alfheim.GUI.Resources;
using Alfheim.GUI.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для ProjectPage.xaml
    /// </summary>
    public partial class ProjectPage : Page
    {
        LinguisticVariableService mLinguisticVariableService = LinguisticVariableService.Instance;

        public ProjectPage(string ProjectName)
        {
            InitializeComponent();

            mRulesLB.ItemsSource = RulesService.Instance.Rules;
            mOutputsList.ItemsSource = mLinguisticVariableService.OutputLinguisticVariables;
            mInputList.ItemsSource = mLinguisticVariableService.InputLinguisticVariables;
            mProjectNameTB.Text = ProjectName;
        }

        #region Add events

        private void AddInputButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var variable = CreateLinguisticVariable();

                if (variable != null)
                    mLinguisticVariableService.InputLinguisticVariables.Add(variable);
            }
            catch(LinguisticVariableNameAlreadyExistsException)
            {
                ErrorBox.Show(ApplicationStringConstants.NameIsExist);
            }          
        }

        private void AddOutputButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var variable = CreateLinguisticVariable();

                if (variable != null)
                    mLinguisticVariableService.OutputLinguisticVariables.Add(variable);
            }
            catch (LinguisticVariableNameAlreadyExistsException)
            {
                ErrorBox.Show(ApplicationStringConstants.NameIsExist);
            }
        }

        private LinguisticVariable CreateLinguisticVariable()
        {
            AddLinguisticVariableWindow addVariableWindow = new AddLinguisticVariableWindow();
            addVariableWindow.Owner = Window.GetWindow(this);
            addVariableWindow.ShowDialog();

            return addVariableWindow.Result;
        }

        #endregion

        #region Remove events

        private void RemoveOutputButton_Click(object sender, RoutedEventArgs e)
        {
            var item = mOutputsList.SelectedItem as LinguisticVariable;

            if (item != null)
                mLinguisticVariableService.OutputLinguisticVariables.Remove(item);
        }

        private void RemoveInputButton_Click(object sender, RoutedEventArgs e)
        {
            var item = mInputList.SelectedItem as LinguisticVariable;

            if (item != null)
                mLinguisticVariableService.InputLinguisticVariables.Remove(item);
        }


        #endregion

        private void EditRules_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void InputListBoxDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount >= 2)
            {
                EditLinguisticVariable(mInputList);
            }
        }

        private void OutputListBoxDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount >= 2)
            {
                EditLinguisticVariable(mOutputsList);
            }
        }


        private void EditLinguisticVariable(ListBox listBox)
        {
            var item = listBox.SelectedItem as LinguisticVariable;

            (Window.GetWindow(this) as MainWindow).OpenPage(new LinguisticVariablePage(this, item));
        }

        private void RulesListBoxDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void AddRules_Click(object sender, RoutedEventArgs e)
        {
            var nrw = new NewRuleWindow();

            nrw.Owner = Window.GetWindow(this);
            nrw.Show();
        }

        private void RemoveRule_Click(object sender, RoutedEventArgs e)
        {
            var rule = (mRulesLB.SelectedValue as Rule);

            if (rule != null)
                RulesService.Instance.RemoveRule(rule);
        }

        private void StartTest_Click(object sender, RoutedEventArgs e)
        {
            SimpleExecuteWindow sew = new SimpleExecuteWindow();
            sew.Owner = Window.GetWindow(this);

            sew.ShowDialog();
        }
    }
}
