using System;
using System.Collections.Generic;
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

namespace Alfheim.GUI.Controls
{
    /// <summary>
    /// Логика взаимодействия для TextBlockWithTitle.xaml
    /// </summary>
    public partial class TextBlockWithTitle : UserControl
    {
        public TextBlockWithTitle()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(TextBlockWithTitle), new UIPropertyMetadata(string.Empty));

        public string Text
        {
            get
            {
                return mTextBox.Text;
            }
            set
            {
                mTextBox.Text = value;
            }
        }

        public string Title
        {
            get
            {
                return (string)this.GetValue(TitleProperty);
            }
            set
            {
                this.SetValue(TitleProperty, value);
            }
        }
    }
}
