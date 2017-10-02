using System.Windows;
using System.Windows.Controls;

namespace Alfheim.GUI.Services
{
    public static class TextBlockGenerator
    {
        public static TextBlock GeneratePropertyTextBlock(string propertyName)
        {
            TextBlock textBlock = new TextBlock();

            textBlock.Text = (string)Application.Current.TryFindResource(propertyName);
            textBlock.Style = Application.Current.FindResource("PropertyNameStyle") as Style;

            return textBlock;
        }
    }
}
