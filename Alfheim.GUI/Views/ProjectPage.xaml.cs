using Alfheim.FuzzyLogic.Variables.Model;
using Alfheim.FuzzyLogic.Variables.Services;
using Alfheim.GUI.Controls;
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
        LinguisticVariableService mLinguisticVariableService = new LinguisticVariableService();

        public ProjectPage(string ProjectName)
        {
            InitializeComponent();

            mInputList.ItemsSource = mLinguisticVariableService.InputLinguisticVariables;
            mProjectNameTB.Text = ProjectName;
        }

        private void AddInputButton_Click(object sender, RoutedEventArgs e)
        {
            AddLinguisticVariableWindow addVariableWindow = new AddLinguisticVariableWindow();
            addVariableWindow.Owner = Window.GetWindow(this);
            addVariableWindow.ShowDialog();

            try
            {
                if (addVariableWindow.ShowResult == true)
                    mLinguisticVariableService.InputLinguisticVariables.Add(addVariableWindow.Result);
            }
            catch(LinguisticVariableNameAlreadyExistsException)
            {
                MessageBox.Show((string)this.FindResource("mAdd"), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void AddOutputButton_Click(object sender, RoutedEventArgs e)
        {
            mOutputsList.Items.Add(DateTime.Now.Ticks);
        }

        private void RemoveOutputButton_Click(object sender, RoutedEventArgs e)
        {
            RemoveSelectedItem(mOutputsList);
        }

        private void RemoveInputButton_Click(object sender, RoutedEventArgs e)
        {
            var item = mInputList.SelectedItem as LinguisticVariable;

            if(item != null)
                mLinguisticVariableService.InputLinguisticVariables.Remove(item);
        }

        public void RemoveSelectedItem(ListBoxWithHeader control)
        {
            if (control != null && control.SelectedItem != null)
                control.Items.Remove(control.SelectedItem);
        }

        private void EditRules_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ListBoxDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = mInputList.SelectedItem as LinguisticVariable;

            if (e.ClickCount >= 2)
            {
                (Window.GetWindow(this) as MainWindow).OpenPage(new LinguisticVariablePage(this, item));
            }
        }
    }
}
