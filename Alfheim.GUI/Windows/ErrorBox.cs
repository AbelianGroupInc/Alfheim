using Alfheim.GUI.Resources;
using System.Windows;

namespace Alfheim.GUI.Windows
{
    internal static class ErrorBox
    {
        internal static void Show(string errorMessage)
        {
            MessageBox.Show(errorMessage,
                    ApplicationStringConstants.Error,
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
        }
    }
}
