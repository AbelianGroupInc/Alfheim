using Alfheim.FuzzyLogic;
using Alfheim.FuzzyLogic.Variables.Model;
using Alfheim.FuzzyLogic.Variables.Services;
using Alfheim.GUI.Controls;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace Alfheim.GUI
{
    /// <summary>
    /// Логика взаимодействия для SimpleExecuteWindow.xaml
    /// </summary>
    public partial class SimpleExecuteWindow : MetroWindow
    {
        private Dictionary<TextBlockWithTitle, LinguisticVariable> dic = 
            new Dictionary<TextBlockWithTitle, LinguisticVariable>();

        public SimpleExecuteWindow()
        {
            InitializeComponent();
            InitSP();
        }

        private void InitSP()
        {
            foreach(var @var in LinguisticVariableService.Instance.InputLinguisticVariables)
            {
                TextBlockWithTitle tbwt = new TextBlockWithTitle();

                tbwt.Style = Application.Current.FindResource("DefaultTextBlockWithTitle") as Style;
                tbwt.Title = var.Name;

                dic.Add(tbwt, @var);
            }

            foreach(var @var in dic)
                ctrMainSP.Children.Add(var.Key);
        }

        private void Execute_Click(object sender, RoutedEventArgs e)
        {
            FuzzyLogicQuery query = new FuzzyLogicQuery(CreateQueryParams());
            ctrResultTB.Text = $"{LinguisticVariableService.Instance.OutputLinguisticVariables.FirstOrDefault().Name ?? "Result"}: {query.Execute()}";
        }

        private Dictionary<LinguisticVariable, double> CreateQueryParams()
        {
            Dictionary<LinguisticVariable, double> result = new Dictionary<LinguisticVariable, double>();

            foreach (var @var in dic)
                result.Add(var.Value, Convert.ToDouble(var.Key.Text, CultureInfo.InvariantCulture));

            return result;
        }
    }
}
