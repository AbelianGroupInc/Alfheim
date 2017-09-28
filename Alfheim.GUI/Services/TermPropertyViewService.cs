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
using Alfheim.GUI.Controls;
using Alfheim.GUI.Model;

namespace Alfheim.GUI.Services
{
    public class TermPropertyViewService
    {
        StackPanel mPanel;
        Term mThisTerm;

        public TermPropertyViewService(StackPanel panel)
        {
            mPanel = panel;
            App.LanguageChanged += AppLanguageChanged;
        }

        public void ShowTermProperties(Term term)
        {
            mThisTerm = term;

            mPanel.Children.Clear();

            GenerateNameProperty();
            GenerateFunctionProperty();

            var points = InRangePointParser.Parse(mThisTerm);

            foreach (var point in points)
            {
                mPanel.Children.Add(GeneratePropertTextBlock(point.Name));
                mPanel.Children.Add(CreatePropertySlider(point));
            }
        }

        private Slider CreatePropertySlider(InRangePoint inRangePoint)
        {
            Slider slider = new Slider();

            slider.Style = Application.Current.FindResource("SliderFlatStyle") as Style;
            slider.Minimum = mThisTerm.FuzzyFunction.MinInputValue;
            slider.Maximum = mThisTerm.FuzzyFunction.MaxInputValue;

            slider.IsSelectionRangeEnabled = true;
            slider.SelectionStart = (double)GetFuzzyFunctionProperty(inRangePoint.LeftPointName);
            slider.SelectionEnd = (double)GetFuzzyFunctionProperty(inRangePoint.RightPointName);

            slider.AutoToolTipPlacement = System.Windows.Controls.Primitives.AutoToolTipPlacement.TopLeft;
            slider.AutoToolTipPrecision = 2;
            slider.IsSnapToTickEnabled = false;
            slider.TickPlacement = System.Windows.Controls.Primitives.TickPlacement.BottomRight;
            slider.TickFrequency = (slider.Minimum + slider.Maximum) / 20.0;

            slider.Value = (double)GetFuzzyFunctionProperty(inRangePoint.Name);

            return slider;
        }

        #region Private methods

        private ComboBox CreateFunctionComboBox(IFuzzyFunction termFunction)
        {
            ComboBox comboBox = new ComboBox();

            comboBox.Style = Application.Current.FindResource("ComboBoxFlatStyle") as Style;

            comboBox.Items.Add(new KeyValuePair<string, IFuzzyFunction>(
                ApplicationStringConstants.TriangleFunction, new TriangleFunction()));

            comboBox.Items.Add(new KeyValuePair<string, IFuzzyFunction>(
                ApplicationStringConstants.TrapezoidalFunction, new TrapezoidalFunction()));

            comboBox.Items.Add(new KeyValuePair<string, IFuzzyFunction>(
                ApplicationStringConstants.GaussianFunction, new GaussianFunction()));


            foreach (var function in comboBox.Items)
                if (((KeyValuePair<string, IFuzzyFunction>)function).Value.GetType() == termFunction.GetType())
                {
                    comboBox.SelectedItem = function;
                    break;
                }

            comboBox.DisplayMemberPath = "Key";

            return comboBox;
        }

        private void GenerateFunctionProperty()
        {
            mPanel.Children.Add(GeneratePropertTextBlock((string)Application.Current.FindResource("cFunctions")));
            mPanel.Children.Add(CreateFunctionComboBox(mThisTerm.FuzzyFunction));
        }

        private void GenerateNameProperty()
        {
            mPanel.Children.Add(GeneratePropertTextBlock((string)Application.Current.FindResource("cTermName")));
            mPanel.Children.Add(GeneratePropertTextBox("Name"));
        }

        private TextBlock GeneratePropertTextBlock(string propertyName)
        {
            TextBlock textBlock = new TextBlock();

            textBlock.Text = propertyName;
            textBlock.Style = Application.Current.FindResource("PropertyName") as Style;

            return textBlock;
        }

        private TextBlockWithTitle GeneratePropertTextBox(string propertyName)
        {
            TextBlockWithTitle textBlock = new TextBlockWithTitle();

            textBlock.Text = (string)GetTermProperty(propertyName);
            textBlock.Title = propertyName;
            textBlock.Style = Application.Current.FindResource("DefaultTextBlockWithTitle") as Style;

            return textBlock;
        }

        private object GetTermProperty(string propertyName)
        {
            return mThisTerm.GetType().GetProperty(propertyName).GetValue(mThisTerm);
        }

        private object GetFuzzyFunctionProperty(string propertyName)
        {
            return mThisTerm.FuzzyFunction.GetType().GetProperty(propertyName).GetValue(mThisTerm.FuzzyFunction);
        }

        #endregion

        #region Hendlers

        private void AppLanguageChanged(object sender, EventArgs e)
        {
            if(mThisTerm != null)
                ShowTermProperties(mThisTerm);
        }

        #endregion
    }
}
