using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Alfheim.GUI.Services
{
    public class ComboBoxPropertyEditor : PropertyEditor
    {
        private Dictionary<string, object> mValues;

        public ComboBoxPropertyEditor(Dictionary<string, object> values)
        {
            mValues = values;
        }

        public ComboBoxPropertyEditor(string targetPropertyName, Dictionary<string, object> values) : this(values)
        {
            TargetPropertyName = targetPropertyName;  
        }

        public override UIElement[] GenerateUIElements()
        {
            return new UIElement[]
            {
                TextBlockGenerator.GeneratePropertyTextBlock(TargetPropertyName) ,
                GeneratePropertyComboBox()
            };
        }

        public override event PropertyChangedEventHandler PropertyChanged;

        #region private methods

        private ComboBox GeneratePropertyComboBox()
        {
            ComboBox comboBox = new ComboBox();

            comboBox.Style = Application.Current.FindResource("ComboBoxFlatStyle") as Style;
            comboBox.DisplayMemberPath = "Key";

            foreach (var value in mValues)
                comboBox.Items.Add(value);

            foreach (var item in comboBox.Items)
                if (((KeyValuePair<string, object>)item).Value.GetType() == TargetProperty.GetType())
                {
                    comboBox.SelectedItem = item;
                    break;
                }

            comboBox.SelectionChanged += OnPropertyChanged;
            return comboBox;
        }        

        #region Handlers

        private void OnPropertyChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (sender as ComboBox);

            if (comboBox == null)
                return;

            TargetProperty = ((KeyValuePair<string, object>)comboBox.SelectedItem).Value;
            PropertyChanged(Target, new PropertyChangedEventArgs(TargetPropertyName));
        }

        #endregion
        #endregion
    }
}
