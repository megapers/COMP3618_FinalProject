using SearchToolbox.WPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SearchToolbox.WPF.ViewModel
{
    public class AddVM
    {
        public Movie Movie { get; set; }
        

        public AddVM()
        {
        }

        public AddVM(Movie movie)
        {
            Movie = movie;
        }


        public async Task<bool> AddAsync()
        {
            RESTClient client = new RESTClient();
            try
            {
                await client.AddAsync(Movie);
                MessageBox.Show("Done");
                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return false;
            }

            
        }
    }

}
