using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using SearchToolbox.WPF.DataVirtualization;
using SearchToolbox.WPF.Model;
using SearchToolbox.WPF.View;
using SearchToolbox.WPF.ViewModel;

namespace SearchToolbox.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainVM viewModel;
        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MainVM();
            DataContext = viewModel;
        }


        private async void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            btnSearch.IsEnabled = false;
            await viewModel.SearchMatch();
            moviesList.DataContext = viewModel.DataContext;   
            btnSearch.IsEnabled = true;

        }

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

            AddWindow addWindow = new AddWindow(new Movie());
            addWindow.ShowDialog();
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                await viewModel.Delete(moviesList.SelectedItems);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (moviesList.SelectedItem != null)
            {
                EditWindow editWindow = new EditWindow((Movie)moviesList.SelectedItem);
                editWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select an item to edit.");
            }

        }
    }
}
