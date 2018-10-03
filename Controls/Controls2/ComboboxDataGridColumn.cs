using Controls.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Controls
{
    public class ComboboxDataGridColumn : DataGridTextColumn
    {
        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            TextBlock box = new TextBlock();
            BindingOperations.SetBinding(box, TextBlock.TextProperty, new Binding(DisplaymemberPath));
            return box;
        }
        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            ComboBox cb = new ComboBox();
            cb.Style = (Style)App.Current.FindResource("SearchComboboxStyle");
            cb.DisplayMemberPath = DisplaymemberPathCombobox;

            if (ItemSourceInParentDataContext)
                BindingOperations.SetBinding(cb, ComboBox.ItemsSourceProperty, new Binding($"DataContext.{ItemsourcePath}") { Source = ((ComboboxDataGridColumn)cell.Column).DataGridOwner });
            else
                BindingOperations.SetBinding(cb, ComboBox.ItemsSourceProperty, new Binding(ItemsourcePath));

            BindingOperations.SetBinding(cb, ComboBox.SelectedItemProperty, Binding);
            cb.IsDropDownOpen = true;

            cb.ApplyTemplate();
            TextBox filterBox = (TextBox)cb.Template.FindName("PART_Filterbox", cb);
            filterBox.TextChanged += FilterChanged;
            filterBox.Tag = cb.ItemsSource;

            return cb;
        }

        private void TemplateApplied()
        {
            throw new NotImplementedException();
        }

        private void FilterChanged(object sender, TextChangedEventArgs e)
        {
            var sourceView = CollectionViewSource.GetDefaultView(((TextBox)sender).Tag);
            if (sourceView.Filter == null)
                sourceView.Filter = p => ((BaseObject)p).Name.ToLower().Contains(((TextBox)sender).Text.ToLower());
            else
                sourceView.Refresh();
        }

        public string DisplaymemberPath { get { return (string)GetValue(DisplaymemberPathProperty); } set { SetValue(DisplaymemberPathProperty, value); } }
        public static readonly DependencyProperty DisplaymemberPathProperty =
            DependencyProperty.Register("DisplaymemberPath", typeof(string), typeof(ComboboxDataGridColumn), new PropertyMetadata("Name"));

        public string DisplaymemberPathCombobox { get { return (string)GetValue(DisplaymemberPathComboboxProperty); } set { SetValue(DisplaymemberPathComboboxProperty, value); } }
        public static readonly DependencyProperty DisplaymemberPathComboboxProperty =
            DependencyProperty.Register("DisplaymemberPathCombobox", typeof(string), typeof(ComboboxDataGridColumn), new PropertyMetadata("Name"));

        public string SearchMemberPath { get { return (string)GetValue(SearchMemberPathProperty); } set { SetValue(SearchMemberPathProperty, value); } }
        public static readonly DependencyProperty SearchMemberPathProperty =
            DependencyProperty.Register("SearchMemberPath", typeof(string), typeof(ComboboxDataGridColumn), new PropertyMetadata("Name"));

        public string ItemsourcePath { get { return (string)GetValue(ItemsourcePathProperty); } set { SetValue(ItemsourcePathProperty, value); } }
        public static readonly DependencyProperty ItemsourcePathProperty =
            DependencyProperty.Register("ItemsourcePath", typeof(string), typeof(ComboboxDataGridColumn), new PropertyMetadata(null));

        public bool ItemSourceInParentDataContext { get { return (bool)GetValue(ItemSourceInParentDataContextProperty); } set { SetValue(ItemSourceInParentDataContextProperty, value); } }
        public static readonly DependencyProperty ItemSourceInParentDataContextProperty =
            DependencyProperty.Register("ItemSourceInParentDataContext", typeof(bool), typeof(ComboboxDataGridColumn), new PropertyMetadata(true));

    }
}
