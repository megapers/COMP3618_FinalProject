using SearchToolbox.WPF.Model;
using SearchToolbox.WPF.ViewModel;
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
using System.Windows.Shapes;

namespace SearchToolbox.WPF
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {

        //private RESTClient apiCalls;

        private AddVM ViewModel;

        public AddWindow(Movie movie)
        {
            InitializeComponent();
            ViewModel = new AddVM(movie);
            DataContext = ViewModel;
            //apiCalls = new RESTClient();
        }

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            btnAdd.IsEnabled = false;
            var result = await ViewModel.AddAsync();
            if (result == true)
            {
                this.Close();
            }
            btnAdd.IsEnabled = true;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
