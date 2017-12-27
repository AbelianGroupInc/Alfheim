using Alfheim.FuzzyLogic.Variables.Model;
using Alfheim.FuzzyLogic.Variables.Services;
using System.Windows;
using System.Windows.Controls;

namespace Alfheim.GUI.Services
{
    public class ListBoxForLV
    {
        private StackPanel mCurrentSP = new StackPanel();
        private ComboBox lbComboBox = new ComboBox();
        private ComboBox tComboBox = new ComboBox();

        public ListBoxForLV()
        {
            Init();
        }

        public StackPanel CurrentSP
        {
            get
            {
                return mCurrentSP;
            }
        }

        public LinguisticVariable LinguisticVariable
        {
            get
            {
                return (lbComboBox.SelectedValue as LinguisticVariable);
            }
        }

        public Term Term
        {
            get
            {
                return (tComboBox.SelectedValue as Term);
            }
        }


        private void Init()
        {
            TextBlock tb1 = new TextBlock();
            tb1.Style = Application.Current.FindResource("Header") as Style;
            tb1.Text = "LinguisticVariable";

            TextBlock tb2 = new TextBlock();
            tb2.Style = Application.Current.FindResource("Header") as Style;
            tb2.Text = "Term";

            lbComboBox.Style = Application.Current.FindResource("ComboBoxFlatStyle") as Style;
            lbComboBox.SelectionChanged += LbComboBox_SelectionChanged;
            lbComboBox.ItemsSource = LinguisticVariableService.Instance.InputLinguisticVariables;
            lbComboBox.DisplayMemberPath = "Name";

            tComboBox.Style = Application.Current.FindResource("ComboBoxFlatStyle") as Style;
            tComboBox.DisplayMemberPath = "Name";

            mCurrentSP.Children.Add(tb1);
            mCurrentSP.Children.Add(lbComboBox);

            mCurrentSP.Children.Add(tb2);
            mCurrentSP.Children.Add(tComboBox);
        }


        private void LbComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tComboBox.ItemsSource = (lbComboBox.SelectedValue as LinguisticVariable).Terms;
        }
    }
}
