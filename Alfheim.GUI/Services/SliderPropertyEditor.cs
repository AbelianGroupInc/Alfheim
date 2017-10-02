using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Alfheim.GUI.Services
{
    public class SliderPropertyEditor : PropertyEditor
    {
        public SliderPropertyEditor(string targetPropertyName)
        {
            TargetPropertyName = targetPropertyName;
        }

        public override event PropertyChangedEventHandler PropertyChanged;

        public override UIElement[] GenerateUIElements()
        {
            return new UIElement[]
            {
                TextBlockGenerator.GeneratePropertyTextBlock(TargetPropertyName) ,
                GeneratePropertySlider()
            };
        }

        #region private methods

        private Slider GeneratePropertySlider()
        {
            Slider slider = new Slider();

            slider.Style = Application.Current.FindResource("SliderFlatStyle") as Style;

            slider.IsSelectionRangeEnabled = true;

            slider.AutoToolTipPlacement = System.Windows.Controls.Primitives.AutoToolTipPlacement.TopLeft;
            slider.AutoToolTipPrecision = 2;
            slider.IsSnapToTickEnabled = false;
            slider.TickPlacement = System.Windows.Controls.Primitives.TickPlacement.BottomRight;

            slider.Value = (double)TargetProperty;
            slider.ValueChanged += OnPropertyChanged;

            return slider;
        }

        #region Handlers

        private void OnPropertyChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var slider = (sender as Slider);

            if (slider == null)
                return;

            if (e.NewValue < slider.SelectionStart
                || e.NewValue > slider.SelectionEnd)
                slider.Value = e.OldValue;

            TargetProperty = slider.Value;
            PropertyChanged(Target, new PropertyChangedEventArgs(TargetPropertyName));
        }

        #endregion
        #endregion
    }
}
