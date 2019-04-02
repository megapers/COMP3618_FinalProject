using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;
using SearchToolbox.WPF.Model;
using SearchToolbox.WPF.DataVirtualization;

namespace SearchToolbox.WPF.ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {
        const int PAGESIZE = 40;
        const int TIMEOUT = 200;

        private RESTClient client;
        private string _message;

        public string Message
        {
            set
            {
                _message = value;
                OnPropertyChanged("Message");
            }
            get
            {
                return _message;
            }
        }
        public AsyncVirtualizingCollection<Movie> DataContext { set; get; }
        public string SearchFor { set; get; }



        public MainVM()
        {
            client = new RESTClient();
        }

        public async Task GetMovie(string code)
        {
            Movie result = null;
            try
            {
                result = await client.GetAsync(code);
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }

        }

        public async Task Delete(System.Collections.IList movies)
        {
            Message = "Wait...";
            var counter = 0;
            //await Task.Run(() =>
            //         {
            foreach (Movie item in movies)
            {
                await client.DeleteAsync(item.Code);
                counter++;
            }
            Message = $"{counter} record(s) were deleted.";
            //}
            //);
        }

        public async Task<int> GetCountSearchMovie()
        {
            Message = "Wait...";
            int count = 0;
            try
            {
                count = await client.GetCountSearch(SearchFor);
                Message = $"{count} records found";

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                Message = "";
            }
            return count;

        }

        public async Task SearchMatch()
        {
            
            SearchCriteria search = new SearchCriteria();
            search.SearchFor = SearchFor;
            search.BlockSize = PAGESIZE;
            search.CodeGreaterThan = "";
            try
            {
                Task<int> getCountTask = new Task<int>(() =>
                {
                    return GetCountSearchMovie().Result;
                });
                getCountTask.Start();
                await getCountTask;

                var count = getCountTask.Result;
                

                MovieProvider movieProvider = new MovieProvider(count, search);
                DataContext = new AsyncVirtualizingCollection<Movie>(movieProvider, PAGESIZE, TIMEOUT);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                Message = "";
            }

        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}