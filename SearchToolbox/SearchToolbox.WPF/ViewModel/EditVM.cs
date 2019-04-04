using SearchToolbox.WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SearchToolbox.WPF.ViewModel
{
    public class EditVM
    {
        public Movie Movie { get; set; }

        public EditVM()
        {
        }

        public EditVM(Movie movie)
        {
            Movie = movie;
        }

        public async Task Apply()
        {
            RESTClient client = new RESTClient();

            try
            {
                await client.UpdateAsync((string)Movie.Code, (Movie)Movie);
                MessageBox.Show("Done");

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
