using DataEditor.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace DataEditor.Views
{
    public partial class MainView : Window
    {
        private MainViewModel ViewModel => (MainViewModel)DataContext;

        public MainView()
        {
            InitializeComponent();
        }

        private void DataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {
            ViewModel.SortByHeaderText = (string)e.Column.Header;
            e.Handled = true;
        }

        private void DataGridRow_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ViewModel.EditSelectedCustomer();
        }
    }
}
