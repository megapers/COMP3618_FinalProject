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

namespace SearchToolbox.WPF.View
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {

        private EditVM ViewModel;

        public EditWindow(Movie movie)
        {
            InitializeComponent();
            ViewModel = new EditVM(movie);
            DataContext = ViewModel;
        }

        private async void BtnApply_Click(object sender, RoutedEventArgs e)
        {
           await ViewModel.Apply();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
