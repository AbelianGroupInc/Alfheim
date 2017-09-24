﻿using Alfheim.FuzzyLogic.Variables.Model;
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
                ShowLinguisticVariableNameAlreadyExistsException();
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
                ShowLinguisticVariableNameAlreadyExistsException();
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

        private void ShowLinguisticVariableNameAlreadyExistsException()
        {
            //TODO добавить список констант
            MessageBox.Show((string)this.FindResource("cNameIsExist"),
                    (string)this.FindResource("cError"),
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
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
