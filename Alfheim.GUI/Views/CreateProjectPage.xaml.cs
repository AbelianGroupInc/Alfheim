using Alfheim.GUI.Model;
using Alfheim.GUI.Windows;
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

namespace Alfheim.GUI.Views
{
    /// <summary>
    /// Логика взаимодействия для CreateProjectPage.xaml
    /// </summary>
    public partial class CreateProjectPage : Page
    {
        private Page mOwner;

        public CreateProjectPage(Page owner)
        {
            InitializeComponent();

            mOwner = owner;
        }

        private void CreateProject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var @params = new ProjectParameters(ctrNameTB.Text,
                    Convert.ToDouble(ctrVersionTB.Text, CultureInfo.InvariantCulture));

                PorjectCreator.Create(@params);
                (Window.GetWindow(this) as MainWindow).OpenPage(new ProjectPage(@params.Name));
            }
            catch(Exception exp)
            {
                ErrorBox.Show(exp.Message);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as MainWindow).OpenPage(mOwner);
        }
    }
}
