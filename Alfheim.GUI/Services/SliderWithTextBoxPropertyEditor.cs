using Alfheim.GUI.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Alfheim.GUI.Services
{
    public class SliderWithTextBoxPropertyEditor : PropertyEditor
    {
        public SliderWithTextBoxPropertyEditor(string targetPropertyName)
        {
            TargetPropertyName = targetPropertyName;
        }

        public override UIElement[] GenerateUIElements()
        {
            return new UIElement[]
            {
                TextBlockGenerator.GeneratePropertyTextBlock(TargetPropertyName) ,
                GeneratePropertyTextBox()
            };
        }

        public override event PropertyChangedEventHandler PropertyChanged;

        #region private methods

        private TextBlockWithTitle GeneratePropertyTextBox()
        {
            TextBlockWithTitle textBlock = new TextBlockWithTitle();

            textBlock.Text = (string)TargetProperty;
            textBlock.Title = (string)Application.Current.FindResource(TargetPropertyName);
            textBlock.Style = Application.Current.FindResource("DefaultTextBlockWithTitle") as Style;

            textBlock.TextChanged += TextBlockTextChanged;

            return textBlock;
        }

        #region Handlers

        private void TextBlockTextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var textBox = (sender as TextBox);

            if (textBox == null)
                return;

            TargetProperty = textBox.Text;
            PropertyChanged(Target, new PropertyChangedEventArgs(TargetPropertyName));
        }

        #endregion
        #endregion
    }
}
