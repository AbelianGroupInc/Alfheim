using Alfheim.FuzzyLogic.Variables.Model;
using Alfheim.FuzzyLogic.Variables.Services;
using Alfheim.GUI.Controls;
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
    /// Логика взаимодействия для ProjectPage.xaml
    /// </summary>
    public partial class ProjectPage : Page
    {
        private LinguisticVariableService mLinguisticVariableService = new LinguisticVariableService();
        private List<LinguisticVariable> mLinguisticVariables = new List<LinguisticVariable>();

        public ProjectPage(string ProjectName)
        {
            InitializeComponent();

            mProjectNameTB.Text = ProjectName;
        }

        private void AddInputButton_Click(object sender, RoutedEventArgs e)
        {
            AddLinguisticVariableWindow addVariableWindow = new AddLinguisticVariableWindow();
            addVariableWindow.Owner = Window.GetWindow(this);
            addVariableWindow.ShowDialog();
            //mInputList.Items.Add(new LinguisticVariable("test", 0, 100));
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
            //RemoveSelectedItem(mInputList);
        }

        public void RemoveSelectedItem(ListBoxWithHeader control)
        {
            if (control != null && control.SelectedItem != null)
                control.Items.Remove(control.SelectedItem);
        }

        private void EditRules_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as MainWindow).OpenPage(new LinguisticVariablePage(this));
        }

        private void ListBoxDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount >= 2)
            {

            }
        }
    }
}
