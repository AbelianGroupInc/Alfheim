using Alfheim.GUI.Model;
using Alfheim.GUI.Windows;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Alfheim.GUI.Views
{
    /// <summary>
    /// Логика взаимодействия для OpenProjectPage.xaml
    /// </summary>
    public partial class OpenProjectPage : Page
    {
        Page mOwner;

        public OpenProjectPage(Page owner)
        {
            InitializeComponent();

            mOwner = owner;
            FillListox();
        }

        private void FillListox()
        {
            var projects = Directory.GetFiles(Properties.Settings.Default.ProjectsFolder, "*.fis", SearchOption.AllDirectories)
                                .Select(f => System.IO.Path.GetFileNameWithoutExtension(f));

            foreach (var project in projects)
                ctrProjectsLB.Items.Add(project);
        }

        private void OpenProject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string projectName = (string)ctrProjectsLB.SelectedItem;
                string filePath = Directory.GetFiles(Properties.Settings.Default.ProjectsFolder,
                    $"{projectName}.fis", SearchOption.AllDirectories).FirstOrDefault();

                if (String.IsNullOrEmpty(filePath))
                    throw new ArgumentException(nameof(filePath));

                ProjectService.OpenProject(filePath);
                (Window.GetWindow(this) as MainWindow).OpenPage(new ProjectPage(projectName));
            }
            catch(Exception exp)
            {
                ErrorBox.Show(exp.Message);
            }
}

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();

                ofd.Filter = "Fis files|*.fis";
                ofd.Title = "Select a fis File";

                if (ofd.ShowDialog() == true)
                {
                    string fileName = System.IO.Path.GetFileNameWithoutExtension(ofd.FileName);

                    ProjectService.OpenProject(ofd.FileName);
                    (Window.GetWindow(this) as MainWindow).OpenPage(new ProjectPage(fileName));
                }
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
