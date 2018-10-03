using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;

namespace Controls
{
    public class ComboboxDataGridColumn : DataGridTextColumn
    {
        #region Generate Elements
        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            TextBlock box = new TextBlock();
            box.Padding = new Thickness(4, 2, 4, 2);
            BindingOperations.SetBinding(box, TextBlock.TextProperty, new Binding(DisplaymemberPath));
            return box;
        }
        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            _grid = ((ComboboxDataGridColumn)cell.Column).DataGridOwner;

            _box = new ComboBox { DisplayMemberPath = DisplaymemberPathCombobox, Style = (Style)App.Current.FindResource("SearchComboboxStyle") };

            if (ItemSourceInParentDataContext)
                BindingOperations.SetBinding(_box, ComboBox.ItemsSourceProperty, new Binding($"DataContext.{ItemsourcePath}") { Source = _grid });
            else
                BindingOperations.SetBinding(_box, ComboBox.ItemsSourceProperty, new Binding(ItemsourcePath));

            BindingOperations.SetBinding(_box, ComboBox.SelectedItemProperty, Binding);

            _box.IsDropDownOpen = true;
            _box.ApplyTemplate();

            TextBox searchBox = (TextBox)_box.Template.FindName("PART_Filterbox", _box);
            if (!SearchEnabled)
                searchBox.Visibility = Visibility.Collapsed;
            else
            {
                BindingOperations.SetBinding(searchBox, TextBox.TextProperty, new Binding(nameof(_searchString))
                {
                    Mode = BindingMode.TwoWay,
                    Delay = 250,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                    Source = this
                });

                Dispatcher.BeginInvoke((Action)delegate
                {
                    searchBox.Focus();
                }, DispatcherPriority.ApplicationIdle);
            }

            _box.DropDownClosed += ComboBoxDropDownClosed;
            return _box;
        }
        #endregion

        #region Events
        private void ComboBoxDropDownClosed(object sender, EventArgs e)
        {
            CollectionViewSource.GetDefaultView(_box.ItemsSource).Filter = null;
            _grid.CommitEdit(DataGridEditingUnit.Cell, true);
            _searchString = "";
        }

        private void FilterChanged()
        {
            var sourceView = CollectionViewSource.GetDefaultView(_box.ItemsSource);
            if (sourceView.Filter == null)
            {
                sourceView.Filter = p => String.IsNullOrEmpty(_searchString) ||
                    (Reflection.GetValue(p, SearchMemberPath, out object filterProperty, out _) ?
                    ((string)filterProperty).ToLower().Contains(_searchString.ToLower()) : false);
            }
            else
            {
                sourceView.Refresh();
            }
        }
        #endregion

        #region DependencyProperties
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

        public bool SearchEnabled { get { return (bool)GetValue(SearchEnabledProperty); } set { SetValue(SearchEnabledProperty, value); } }
        public static readonly DependencyProperty SearchEnabledProperty =
            DependencyProperty.Register("SearchEnabled", typeof(bool), typeof(ComboboxDataGridColumn), new PropertyMetadata(true));
        #endregion

        #region Fields + Properties
        public string _searchString { get { return _searchField; } set { _searchField = value; FilterChanged(); } }
        DataGrid _grid;
        ComboBox _box;
        private string _searchField;
        #endregion
    }
}
