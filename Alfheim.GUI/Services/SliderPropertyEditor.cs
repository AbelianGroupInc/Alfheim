using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Alfheim.GUI.Services
{
    public class SliderPropertyEditor : PropertyEditor
    {
        private bool mIsSelectionRangeEnabled = true;

        public SliderPropertyEditor(string targetPropertyName, bool isSelectionRangeEnabled = true)
        {
            TargetPropertyName = targetPropertyName;
            mIsSelectionRangeEnabled = isSelectionRangeEnabled;
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

            slider.IsSelectionRangeEnabled = mIsSelectionRangeEnabled;

            slider.AutoToolTipPlacement = System.Windows.Controls.Primitives.AutoToolTipPlacement.TopLeft;
            slider.AutoToolTipPrecision = 2;
            slider.IsSnapToTickEnabled = false;
            slider.TickPlacement = System.Windows.Controls.Primitives.TickPlacement.BottomRight;

            slider.Minimum = (double)TargetProperty - 1;
            slider.Maximum = (double)TargetProperty + 1;
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

            if (slider.IsSelectionRangeEnabled && (e.NewValue < slider.SelectionStart
                || e.NewValue > slider.SelectionEnd))
                slider.Value = e.OldValue;

            TargetProperty = slider.Value;
            PropertyChanged(Target, new PropertyChangedEventArgs(TargetPropertyName));
        }

        #endregion
        #endregion
    }
}
