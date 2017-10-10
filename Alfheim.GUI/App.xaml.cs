using Alfheim.GUI.Model;
using Alfheim.GUI.Resources;
using Alfheim.GUI.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace Alfheim.GUI
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static List<CultureInfo> m_Languages = new List<CultureInfo>();

        public static event EventHandler LanguageChanged;
        public static event EventHandler ChartQualityChanged;

        #region Properties

        public static QualityPreset ChartQuality
        {
            get
            {
                return (QualityPreset)GUI.Properties.Settings.Default.ChartQuality;
            }
        }

        public static List<CultureInfo> Languages
        {
            get
            {
                return m_Languages;
            }
        }
        public static CultureInfo Language
        {
            get
            {
                return System.Threading.Thread.CurrentThread.CurrentUICulture;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                if (value == System.Threading.Thread.CurrentThread.CurrentUICulture)
                    return;

                System.Threading.Thread.CurrentThread.CurrentUICulture = value;

                ResourceDictionary dict = new ResourceDictionary();

                switch (value.Name)
                {
                    case "ru-RU":
                        dict.Source = new Uri(String.Format("Resources/lang.{0}.xaml", value.Name), UriKind.Relative);
                        break;
                    default:
                        dict.Source = new Uri("Resources/lang.xaml", UriKind.Relative);
                        break;
                }

                ResourceDictionary oldDict = (from d in Application.Current.Resources.MergedDictionaries
                                              where d.Source != null && d.Source.OriginalString.StartsWith("Resources/lang.")
                                              select d).First();
                if (oldDict != null)
                {
                    int ind = Application.Current.Resources.MergedDictionaries.IndexOf(oldDict);
                    Application.Current.Resources.MergedDictionaries.Remove(oldDict);
                    Application.Current.Resources.MergedDictionaries.Insert(ind, dict);
                }
                else
                {
                    Application.Current.Resources.MergedDictionaries.Add(dict);
                }

                LanguageChanged(Application.Current, new EventArgs());
            }
        }

        #endregion

        public App()
        {
            InitializeComponent();

            ApplicationStringResourcesController.Instance.Application = this;
            App.LanguageChanged += App_LanguageChanged;

            InitializeLanguages();
        }

        public static void SetChartQuality(QualityPreset preset)
        {
            GUI.Properties.Settings.Default.ChartQuality = (int)preset;
            ChartQualityChanged(Application.Current, new EventArgs());
        }

        #region Public methods

        private void InitializeLanguages()
        {
            m_Languages.Clear();

            m_Languages.Add(new CultureInfo("en-US"));
            m_Languages.Add(new CultureInfo("ru-RU"));

            Language = Alfheim.GUI.Properties.Settings.Default.DefaultLanguage;
        }

        private void App_LanguageChanged(Object sender, EventArgs e)
        {
            Alfheim.GUI.Properties.Settings.Default.DefaultLanguage = Language;
            Alfheim.GUI.Properties.Settings.Default.Save();
        }

        #endregion
    }
}
