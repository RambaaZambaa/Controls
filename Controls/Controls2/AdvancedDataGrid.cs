using Controls.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Controls
{
    public class AdvancedDataGrid : DataGrid
    {
        List<ColumnFilter> FilterColumns = new List<ColumnFilter>();
        public AdvancedDataGrid() : base()
        {
            Loaded += DataGridLoaded;
        }

        public bool HeaderTextWrapping { get { return (bool)GetValue(HeaderTextWrappingProperty); } set { SetValue(HeaderTextWrappingProperty, value); } }
        public static readonly DependencyProperty HeaderTextWrappingProperty =
            DependencyProperty.Register("HeaderTextWrapping", typeof(bool), typeof(AdvancedDataGrid), new PropertyMetadata(false));



        public bool FilterbarVisible
        {
            get { return (bool)GetValue(FilterbarVisibleProperty); }
            set { SetValue(FilterbarVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FilterbarVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterbarVisibleProperty =
            DependencyProperty.Register("FilterbarVisible", typeof(bool), typeof(AdvancedDataGrid), new PropertyMetadata(false));



        private TextBlock GenerateHeader(string text)
        {
            return new TextBlock { Text = text, TextWrapping = HeaderTextWrapping ? TextWrapping.Wrap : TextWrapping.NoWrap };
        }

        private void DataGridLoaded(object sender, RoutedEventArgs e)
        {
            foreach (DataGridColumn item in Columns)
            {
                Type type = item.GetType();
                if (type == typeof(DataGridTextColumn) /*|| type == typeof(DatagridAdvancedTextColumn) || type == typeof(DataGridComboboxColumn)*/)
                {
                    DataGridTextColumn textColumn = (DataGridTextColumn)item;
                    Binding binding = (Binding)textColumn.Binding;
                    string memberPath = binding.Path.Path;
                    //string memberPath = type == typeof(DataGridComboboxColumn) ? ((DataGridComboboxColumn)item).DisplaymemberPath : binding.Path.Path;

                    ColumnFilter Filter = new ColumnFilter { Path = memberPath, Column = item, Grid = this, Type = ColumnType.Text, Filter = string.Empty };
                    FilterColumns.Add(Filter);
                    TextBoxWatermark box = new TextBoxWatermark() { Watermark = $"Filter: {(string)item.Header}", Background = Brushes.White };
                    BindingOperations.SetBinding(box, TextBoxWatermark.TextProperty, new Binding("Filter")
                    {
                        Source = Filter,
                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                        Mode = BindingMode.TwoWay,
                        Delay = 250
                    });
                    BindingOperations.SetBinding(box, TextBoxWatermark.VisibilityProperty, new Binding("FilterActive")
                    { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, Converter = new BooleanToVisiblityConverter(), Source = Filter });

                    Grid grid = new Grid();
                    grid.Children.Add(GenerateHeader((string)item.Header));
                    grid.Children.Add(box);
                    item.Header = grid;
                }
                //else if (type == typeof(DataGridAdvancedCheckBoxColumn))
                //{
                //    DataGridAdvancedCheckBoxColumn column = (DataGridAdvancedCheckBoxColumn)item;
                //    Binding binding = (Binding)column.Binding;
                //    string memberPath = binding.Path.Path;

                //    ColumnFilter Filter = new ColumnFilter { Path = memberPath, Column = item, Grid = this, Type = ColumnType.Bool, Filter = new Boolean?() };
                //    FilterColumns.Add(Filter);
                //    CheckBox box = new CheckBox()
                //    {
                //        IsThreeState = true,
                //        Background = Brushes.White,
                //        Content = " Filter ",
                //        Padding = new Thickness(5, 0, 0, 0),
                //        VerticalContentAlignment =
                //        VerticalAlignment.Center
                //    };
                //    Viewbox viewBox = new Viewbox { MaxHeight = 32, Child = box };

                //    BindingOperations.SetBinding(box, CheckBox.IsCheckedProperty, new Binding("Filter")
                //    {
                //        Source = Filter,
                //        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                //        Mode = BindingMode.TwoWay,
                //        Delay = 250
                //    });
                //    BindingOperations.SetBinding(box, CheckBox.VisibilityProperty, new Binding("FilterActive")
                //    { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, Converter = new BooleanToVisiblityConverter(), Source = Filter });


                //    Grid grid = new Grid();
                //    var header = GenerateHeader((string)item.Header);
                //    BindingOperations.SetBinding(header, TextBlock.VisibilityProperty, new Binding("FilterActive")
                //    {
                //        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                //        Converter = new BooleanToVisiblityConverter(),
                //        ConverterParameter = "Reverse",
                //        Source = Filter
                //    });
                //    grid.Children.Add(header);
                //    grid.Children.Add(viewBox);
                //    item.Header = grid;
                //}
                //else if (type == typeof(DataGridIntegerColumn))
                //{
                //    DataGridIntegerColumn column = (DataGridIntegerColumn)item;
                //    Binding binding = (Binding)column.Binding;
                //    string memberPath = binding.Path.Path;

                //    ColumnFilter Filter = new ColumnFilter { Path = memberPath, Column = item, Grid = this, Type = ColumnType.Integer, Filter = string.Empty };
                //    FilterColumns.Add(Filter);
                //    TextBoxWatermark box = new TextBoxWatermark()
                //    {
                //        Watermark = $"Filter: {(string)item.Header}",
                //        Background = Brushes.White,
                //        BorderThickness = new Thickness(2),
                //        ToolTip = "Ganzzahl-Filter: Größer als/ Kleiner als: > >= < <= , direkt Zahleintragung und von bis (Zahl1_Zahl2) zulässig."
                //    };
                //    BindingOperations.SetBinding(box, TextBoxWatermark.TextProperty, new Binding("Filter")
                //    {
                //        Source = Filter,
                //        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                //        Mode = BindingMode.TwoWay,
                //        Delay = 500
                //    });
                //    BindingOperations.SetBinding(box, TextBoxWatermark.BorderBrushProperty, new Binding("InvalidFilter")
                //    {
                //        Source = Filter,
                //        Converter = new BoolToBrush(),
                //        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                //    });
                //    BindingOperations.SetBinding(box, TextBoxWatermark.VisibilityProperty, new Binding("FilterActive")
                //    { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, Converter = new BooleanToVisiblityConverter(), Source = Filter });

                //    Grid grid = new Grid();
                //    grid.Children.Add(GenerateHeader((string)item.Header));
                //    grid.Children.Add(box);
                //    item.Header = grid;
                //}
            }

            ICollectionView items = CollectionViewSource.GetDefaultView(ItemsSource);
            foreach (var item in FilterColumns)
            {
                items.Filter = p => ApplyFilter(p);
            }

            if (FilterColumns.Count > 0)
            {
                Button b = new Button { Content = "Filter", Background = Brushes.Transparent };
                b.Click += FilterSwitchClicked;
                Columns[0].Header = b;
            }
        }

        private void FilterSwitchClicked(object sender, RoutedEventArgs e)
        {
            foreach (var item in FilterColumns)
            {
                item.FilterActive = !item.FilterActive;
            }
        }

        private bool ApplyFilter(object p)
        {
            foreach (ColumnFilter item in FilterColumns)
            {
                if (item.Filter != null)
                {
                    if (Reflection.GetValue(p, item.Path, out object value, out _))
                    {
                        switch (item.Type)
                        {
                            case ColumnType.Text:
                                if (!StringFilter(value.ToString(), (string)item.Filter))
                                    return false;
                                break;
                            case ColumnType.Bool:
                                if (!BoolFilter((bool)value, (bool?)item.Filter))
                                    return false;
                                break;
                            case ColumnType.Integer:
                                if (!IntFilter((int)value, (string)item.Filter, item))
                                    return false;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            return true;
        }

        private bool StringFilter(string value, string filter)
        {
            return value.ToLower().Contains(filter.ToLower());
        }

        private bool BoolFilter(bool value, bool? filter)
        {
            return filter == null || value == filter;
        }

        private bool IntFilter(int value, string filterExpression, ColumnFilter column)
        {

            if (string.IsNullOrEmpty(filterExpression))
            {
                column.InvalidFilter = false;
                return true;
            }

            filterExpression = StringHelper.RemoveAllWhitespace(filterExpression);
            int lenght = filterExpression.Length;
            int filterInt;

            if (int.TryParse(filterExpression, out filterInt))
            {
                column.InvalidFilter = false;
                return filterInt == value;
            }
            else if (lenght > 2 && filterExpression.Contains('_'))
            {
                var parts = filterExpression.Split('_');
                if (parts.Length == 2)
                {
                    if (int.TryParse(parts[0], out int i1) && int.TryParse(parts[1], out int i2) && i1 < i2)
                    {
                        column.InvalidFilter = false;
                        return value >= i1 && value <= i2;
                    }
                }
            }
            else if (filterExpression.StartsWith("<") && lenght > 1 && int.TryParse(filterExpression.Remove(0, 1), out filterInt))
            {
                column.InvalidFilter = false;
                return value < filterInt;
            }
            else if (filterExpression.StartsWith("<=") && lenght > 2 && int.TryParse(filterExpression.Remove(0, 2), out filterInt))
            {
                column.InvalidFilter = false;
                return value <= filterInt;
            }
            else if (filterExpression.StartsWith(">") && lenght > 1 && int.TryParse(filterExpression.Remove(0, 1), out filterInt))
            {
                column.InvalidFilter = false;
                return value > filterInt;
            }
            else if (filterExpression.StartsWith(">=") && lenght > 2 && int.TryParse(filterExpression.Remove(0, 2), out filterInt))
            {
                column.InvalidFilter = false;
                return value >= filterInt;
            }

            column.InvalidFilter = true;
            return false;
        }

        public class ColumnFilter : BaseObject
        {
            private object _Filter;
            private bool _FilterActive;
            private bool _InvalidFilter = false;

            public ColumnType Type { get; set; }
            public string Path { get; set; } = string.Empty;
            public object Filter
            {
                get { return _Filter; }
                set
                {
                    if (SetProperty(ref _Filter, value))
                        CollectionViewSource.GetDefaultView(Grid.ItemsSource).Refresh();
                }
            }
            public DataGridColumn Column { get; set; }
            public DataGrid Grid { get; set; }
            public bool FilterActive { get { return _FilterActive; } set { SetProperty(ref _FilterActive, value); } }
            public bool InvalidFilter { get { return _InvalidFilter; } set { SetProperty(ref _InvalidFilter, value); } }
        }

        public enum ColumnType
        {
            Text = 0,
            Bool = 1,
            Integer = 2,
            Double = 3,
        }
    }
}
