using System.Windows;
using System.Windows.Controls;

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

        #region DependencyProperties

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(TextBlockWithTitle), new UIPropertyMetadata(string.Empty));

        #endregion

        #region Events

        public event TextChangedEventHandler TextChanged
        {
            add
            {
                mTextBox.TextChanged += value;
            }
            remove
            {
                mTextBox.TextChanged -= value;
            }
        }

        #endregion

        #region Properties

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

        #endregion
    }
}
