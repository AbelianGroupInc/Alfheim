using Alfheim.FuzzyLogic.Variables.Model;
using Alfheim.GUI.Model;
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
    public class TermFuzzyFunctionSliderEditor : PropertyEditor
    {
        Dictionary<Slider, InRangePoint> mSliders = new Dictionary<Slider, InRangePoint>();
        List<IPropertyEditor> editors = new List<IPropertyEditor>();

        public override event PropertyChangedEventHandler PropertyChanged;

        public override UIElement[] GenerateUIElements()
        {
            mSliders.Clear();
            List<UIElement> elements = new List<UIElement>();

            var function = (Target as Term).FuzzyFunction;
            var points = InRangePointParser.Parse(function);

            foreach (var point in points)
            {
                editors.Add(new SliderPropertyEditor(point.Name));

                editors.Last().Target = function;
                editors.Last().PropertyChanged += OnPropertyChanged;

                var editor = editors.Last().GenerateUIElements();
                var slider = (editor.Last() as Slider);

                mSliders.Add(slider, point);

                elements.AddRange(editor);
            }

            UpdateSliders();
            return elements.ToArray();
        }

        private void UpdateSliders()
        {
            foreach(var slider in mSliders)
            {
                slider.Key.Minimum = (double)GetFuzzyFunctionProperty("MinInputValue");
                slider.Key.Maximum = (double)GetFuzzyFunctionProperty("MaxInputValue");

                slider.Key.SelectionStart = (double)GetFuzzyFunctionProperty(slider.Value.LeftPointName);
                slider.Key.SelectionEnd = (double)GetFuzzyFunctionProperty(slider.Value.RightPointName);

                slider.Key.TickFrequency = (slider.Key.Minimum + slider.Key.Maximum) / 20.0;
            }
        }

        private void OnPropertyChanged(object sender, EventArgs e)
        {
            UpdateSliders();
            PropertyChanged(Target, new PropertyChangedEventArgs("FuzzyFunction"));
        }

        private object GetFuzzyFunctionProperty(string propertyName)
        {
            var function = (Target as Term).FuzzyFunction;

            return function.GetType().GetProperty(propertyName).GetValue(function);

        }
    }
}
