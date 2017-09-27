using Alfheim.FuzzyLogic;
using Alfheim.FuzzyLogic.Functions;
using Alfheim.FuzzyLogic.FuzzyFunctionAttributes;
using Alfheim.FuzzyLogic.Variables.Model;
using Alfheim.GUI.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Alfheim.GUI.Services
{
    public class TremPropertyViewService
    {
        StackPanel mPanel;

        public TremPropertyViewService(Application app, StackPanel panel)
        {
            mPanel = panel;
            App.LanguageChanged += AppLanguageChanged;
        }

        private void ShowTermProperties(Term term)
        {

        }

        private TextBlock CreatePropertyNameTextBlock(string propertyName)
        {
            TextBlock textBlock = new TextBlock();

            textBlock.Text = propertyName;
            return textBlock;
        }

        private TextBox CreatePropertyTextBox(string propertyValue)
        {
            TextBox textBox = new TextBox();

            textBox.Text = propertyValue;

            return textBox;
        }

        private ComboBox CreateFunctionComboBox(IFuzzyFunction termFunction)
        {
            ComboBox comboBox = new ComboBox();

            comboBox.Items.Add(new KeyValuePair<string, IFuzzyFunction>(
                ApplicationStringConstants.TriangleFunction, new TriangleFunction()));

            comboBox.Items.Add(new KeyValuePair<string, IFuzzyFunction>(
                ApplicationStringConstants.TrapezoidalFunction, new TrapezoidalFunction()));

            comboBox.Items.Add(new KeyValuePair<string, IFuzzyFunction>(
                ApplicationStringConstants.GaussianFunction, new GaussianFunction()));


            foreach (var function in comboBox.Items)
            {
                if (((KeyValuePair<string, IFuzzyFunction>)function).Value.GetType() == termFunction.GetType())
                {
                    comboBox.SelectedItem = function;
                    break;
                }
            }

            comboBox.DisplayMemberPath = "Key";

            return comboBox;
        }

        private Slider CreatePropertySlider(IFuzzyFunction term, InRangePointAttribute inRangePoint, double value)
        {
            Slider slider = new Slider();

            slider.Minimum = term.MinInputValue;
            slider.Maximum = term.MaxInputValue;

            slider.IsSelectionRangeEnabled = true;
            slider.SelectionStart = (double)term.GetType().GetProperty(inRangePoint.LeftPoint).GetValue(term);
            slider.SelectionEnd = (double)term.GetType().GetProperty(inRangePoint.RightPoint).GetValue(term);

            slider.IsSnapToTickEnabled = false;
            slider.TickPlacement = System.Windows.Controls.Primitives.TickPlacement.BottomRight;
            slider.TickFrequency = (slider.Minimum + slider.Maximum) / 20.0;

            slider.Value = value;

            return slider;
        }

        private void Update()
        {

        }

        #region Private methods



        #endregion

        #region Hendlers

        private void AppLanguageChanged(object sender, EventArgs e)
        {
            Update();
        }

        #endregion
    }
}
