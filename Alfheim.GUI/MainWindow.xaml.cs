using Alfheim.GUI.Model;
using Alfheim.GUI.Views;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Alfheim.GUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            InitLanguageMenu();
            InitChartsQuality();

            OpenPage(new StartPage());
            App.ChartQualityChanged += ChartQualityChanged;
            App.LanguageChanged += LanguageChanged;   
        }

        private void ChartQualityChanged(object sender, EventArgs e)
        {
            QualityPreset preset = App.ChartQuality;

            foreach (MenuItem i in mQualitySettings.Items)
            {
                QualityPreset p = (QualityPreset)i.Tag;
                i.IsChecked = p.Equals(preset);
            }
        }

        private void LanguageChanged(Object sender, EventArgs e)
        {
            CultureInfo currLang = App.Language;

            foreach (MenuItem i in mLanguageMenu.Items)
            {
                CultureInfo ci = i.Tag as CultureInfo;
                i.IsChecked = ci != null && ci.Equals(currLang);
            }
        }

        private void ChangeLanguageClick(Object sender, EventArgs e)
        {
            MenuItem mi = sender as MenuItem;

            if (mi != null)
            {
                CultureInfo lang = mi.Tag as CultureInfo;

                if (lang != null)
                    App.Language = lang;
            }
        }

        private void InitChartsQuality()
        {
            foreach (QualityPreset preset in Enum.GetValues(typeof(QualityPreset)))
            {
                MenuItem menu = new MenuItem();

                menu.Header = Application.Current.FindResource(preset.ToString());
                menu.Tag = preset;
                menu.IsChecked = preset.Equals(App.ChartQuality);
                menu.Click += ChartQualityClick;
                mQualitySettings.Items.Add(menu);
            }
        }

        private void ChartQualityClick(object sender, RoutedEventArgs e)
        {
            MenuItem mi = sender as MenuItem;

            if (mi != null)
            {
                QualityPreset quality = (QualityPreset)mi.Tag;
                App.SetChartQuality(quality);
            }
        }

        private void InitLanguageMenu()
        {
            CultureInfo currentLanguage = App.Language;
            mLanguageMenu.Items.Clear();

            foreach (var lang in App.Languages)
            {
                MenuItem menuLang = new MenuItem();

                menuLang.Header = lang.DisplayName;
                menuLang.Tag = lang;
                menuLang.IsChecked = lang.Equals(currentLanguage);
                menuLang.Click += ChangeLanguageClick;
                mLanguageMenu.Items.Add(menuLang);
            }
        }

        public void OpenPage(Page page)
        {          
            mMainFrame.Content = page;
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            OpenPage(new StartPage());
        }
    }
}
