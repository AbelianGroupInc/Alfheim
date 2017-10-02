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
using System.ComponentModel;

namespace Alfheim.GUI.Services
{
    public class TermPropertyViewService
    {
        StackPanel mPanel;
        Term mThisTerm;
        IPropertyEditor mNameEditor;
        IPropertyEditor mFunctionEditor;
        TermFuzzyFunctionSliderEditor sliders = new TermFuzzyFunctionSliderEditor();

        public TermPropertyViewService(StackPanel panel)
        {
            mPanel = panel;

            InitEditors();
            App.LanguageChanged += AppLanguageChanged;
        }

        public void ShowTermProperties(Term term)
        {
            mThisTerm = term;

            mPanel.Children.Clear();
  
            mNameEditor.Target = term;
            mFunctionEditor.Target = term;

            foreach (var element in mNameEditor.GenerateUIElements())
                mPanel.Children.Add(element);

            foreach (var element in mFunctionEditor.GenerateUIElements())
                mPanel.Children.Add(element);

            sliders.Target = term;

            foreach (var element in sliders.GenerateUIElements())
                mPanel.Children.Add(element);
        }

        private void InitEditors()
        {
            mNameEditor = new TextBoxPropertyEditor("Name");
            mNameEditor.PropertyChanged += OnTermPropertyChanged;

            mFunctionEditor = new ComboBoxPropertyEditor("FuzzyFunction", GenerateFuzzyFunctionsDictionary());
            mFunctionEditor.PropertyChanged += OnTermPropertyChanged;

            sliders.PropertyChanged += OnTermPropertyChanged;
        }    

        private static Dictionary<string, object> GenerateFuzzyFunctionsDictionary()
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            result[ApplicationStringConstants.TriangleFunction] = new TriangleFunction();
            result[ApplicationStringConstants.TrapezoidalFunction] = new TrapezoidalFunction();
            result[ApplicationStringConstants.GaussianFunction] = new GaussianFunction();

            return result;
        }

        #region Events

        public event PropertyChangedEventHandler TermChanged;

        #endregion

        #region Hendlers

        private void OnTermPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            TermChanged(sender, e);
        }

        private void AppLanguageChanged(object sender, EventArgs e)
        {
            if(mThisTerm != null)
                ShowTermProperties(mThisTerm);
        }

        #endregion
    }
}
