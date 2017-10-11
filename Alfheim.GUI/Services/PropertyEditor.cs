using System.ComponentModel;
using System.Windows;

namespace Alfheim.GUI.Services
{
    public abstract class PropertyEditor : IPropertyEditor
    {
        #region Properties

        public virtual string TargetPropertyName { get; set; }
        public virtual object Target { get; set; }
        protected object TargetProperty
        {
            get
            {
                return Target.GetType()
                    .GetProperty(TargetPropertyName)
                    .GetValue(Target);
            }
            set
            {
                Target.GetType()
                    .GetProperty(TargetPropertyName)
                    .SetValue(Target, value);
            }
        }

        public abstract event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region public methods

        public abstract UIElement[] GenerateUIElements();

        #endregion
    }
}
