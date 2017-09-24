using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Alfheim.GUI.Controls
{
    /// <summary>
    /// Логика взаимодействия для ListBoxWithHeader.xaml
    /// </summary>
    public partial class ListBoxWithHeader : UserControl
    {
        public ListBoxWithHeader()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string),
            typeof(ListBoxWithHeader), new UIPropertyMetadata(string.Empty));

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(ListBoxWithHeader));


        public ItemCollection Items
        {
            get
            {
                return mListBox.Items;
            }
        }

        public object SelectedItem
        {
            get
            {
                return mListBox.SelectedItem;
            }
            set
            {
                mListBox.SelectedItem = value;
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

        public IEnumerable ItemsSource
        {
            get
            {
                return (IEnumerable)this.GetValue(ItemsSourceProperty);
            }
            set
            {
                this.SetValue(ItemsSourceProperty, value);
            }
        }
    }
}
