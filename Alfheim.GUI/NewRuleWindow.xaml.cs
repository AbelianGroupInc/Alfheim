using Alfheim.FuzzyLogic.Rules.Model;
using Alfheim.FuzzyLogic.Rules.Services;
using Alfheim.FuzzyLogic.Variables.Model;
using Alfheim.FuzzyLogic.Variables.Services;
using Alfheim.GUI.Services;
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
    /// Логика взаимодействия для NewRuleWindow.xaml
    /// </summary>
    public partial class NewRuleWindow : MetroWindow
    {
        private List<ListBoxForLV> list = new List<ListBoxForLV>();

        public NewRuleWindow()
        {
            InitializeComponent();
            ctrOutputLVCB.ItemsSource = LinguisticVariableService.Instance.OutputLinguisticVariables;
        }


        private void AddLV_Click(object sender, RoutedEventArgs e)
        {
            ctrLVLB.Children.Add(GetLVForm());  
        }

        private Border GetLVForm()
        {
            Border result = new Border();
            result.Style = Application.Current.FindResource("BorderForSimpleTextBlock") as Style;

            ListBoxForLV lbflv = new ListBoxForLV();
            list.Add(lbflv); 
            result.Child = lbflv.CurrentSP;
            return result;
        }

        private void OutputLVCB_Changed(object sender, SelectionChangedEventArgs e)
        {
            ctrOutputTermCB.ItemsSource =  (ctrOutputLVCB.SelectedValue as LinguisticVariable).Terms;
        }

        private void AddRule_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RuleBuilder builder = RuleBuilder.CreateBuilder();
                builder.Conditions()
                    .ConditionsOperation((OperationType)Enum.Parse(typeof(OperationType), (ctrOpeartionTypesCB.SelectedValue as TextBlock).Text));

                list.ForEach(temp => builder.Conditions().Add(ConditionSign.Identity, temp.Term));

                Rule rule = builder.OutputTerm((ctrOutputTermCB.SelectedValue as Term))
                    .Build();
                RulesService.Instance.AddRule(rule);
                Close();
            }
            catch(Exception exp)
            {
                ErrorBox.Show(exp.Message);
            }
        }
    }
}
