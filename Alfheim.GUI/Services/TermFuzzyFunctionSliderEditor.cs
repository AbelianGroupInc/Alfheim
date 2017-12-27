using Alfheim.FuzzyLogic.Variables.Model;
using Alfheim.GUI.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Alfheim.GUI.Services
{
    public class TermFuzzyFunctionSliderEditor : PropertyEditor
    {
        Dictionary<Slider, InRangePoint> mInRangeSliders = new Dictionary<Slider, InRangePoint>();
        List<IPropertyEditor> editors = new List<IPropertyEditor>();

        public override event PropertyChangedEventHandler PropertyChanged;

        public override UIElement[] GenerateUIElements()
        {
            mInRangeSliders.Clear();
            List<UIElement> elements = new List<UIElement>();

            var function = (Target as Term).FuzzyFunction;
            var inRangePoints = InRangePointParser.Parse(function);

            foreach (var point in inRangePoints)
            {
                editors.Add(new SliderPropertyEditor(point.Name));

                editors.Last().Target = function;
                editors.Last().PropertyChanged += OnPropertyChanged;

                var editor = editors.Last().GenerateUIElements();
                var slider = (editor.Last() as Slider);

                mInRangeSliders.Add(slider, point);

                elements.AddRange(editor);
            }

            UpdateInRangeSliders();

            var referencePoints = ReferencePointParser.Parse(function);

            foreach (var point in referencePoints)
            {
                var sliderEditor = (new SliderPropertyEditor(point.Name, false));

                sliderEditor.Target = function;
                sliderEditor.PropertyChanged += ReferencePointPropertyChanged;

                var uiElements = sliderEditor.GenerateUIElements();
                var slider = (uiElements.Last() as Slider);

                slider.Minimum = 0.1;
                slider.Maximum = function.MaxInputValue * 5;
                slider.TickFrequency = (slider.Minimum + slider.Maximum) / 20.0;

                elements.AddRange(uiElements);
            }

            return elements.ToArray();
        }

        private void ReferencePointPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged(Target, e);
        }

        private void UpdateInRangeSliders()
        {
            foreach(var slider in mInRangeSliders)
            {
                slider.Key.Minimum = (double)GetFuzzyFunctionProperty("MinInputValue");
                slider.Key.Maximum = (double)GetFuzzyFunctionProperty("MaxInputValue");

                slider.Key.SelectionStart = (double)GetFuzzyFunctionProperty(slider.Value.LeftPointName);
                slider.Key.SelectionEnd = (double)GetFuzzyFunctionProperty(slider.Value.RightPointName);

                slider.Key.TickFrequency = (slider.Key.Minimum + slider.Key.Maximum) / 20.0;
            }
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateInRangeSliders();
            PropertyChanged(Target, e);
        }

        private object GetFuzzyFunctionProperty(string propertyName)
        {
            var function = (Target as Term).FuzzyFunction;

            return function.GetType().GetProperty(propertyName).GetValue(function);

        }
    }
}
