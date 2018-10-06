using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Controls
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new Viewmodels.MainVM();
        }

        private void DataGridSorting(object sender, DataGridSortingEventArgs e)
        {
            DataGridColumn column = e.Column;
            if (e.Column.SortDirection.HasValue && e.Column.SortDirection.Value == ListSortDirection.Descending)
            {
                e.Column.SortDirection = null;
                ((DataGrid)sender).Items.SortDescriptions.Clear();
                e.Handled = true;
            }
        }
    }
}
