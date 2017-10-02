using System.ComponentModel;
using System.Windows;

namespace Alfheim.GUI.Services
{
    public interface IPropertyEditor : INotifyPropertyChanged
    {
        string TargetPropertyName { get; set; }
        object Target { get; set; }
        
        UIElement[] GenerateUIElements();
    }
}