using SearchToolbox.WPF.Model;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SearchToolbox.WPF.DataVirtualization
{
    /// <summary>
    /// Demo implementation of IItemsProvider returning dummy customer items after
    /// a pause to simulate network/disk latency.
    /// </summary>
    public class MovieProvider : IItemsProvider<Movie>
    {
        private readonly int _count;
        private SearchCriteria _searchCriteria;

        private RESTClient Client = new RESTClient();
        /// <summary>
        /// Initializes a new instance of the <see cref="MovieProvider"/> class.
        /// </summary>
        /// <param name="count">The count.</param>
        public MovieProvider(int count, SearchCriteria searchCriteria)
        {
            _count = count;
            _searchCriteria = searchCriteria;
        }

        /// <summary>
        /// Fetches the total number of items available.
        /// </summary>
        /// <returns></returns>
        public int FetchCount()
        {
            return _count;
        }

        /// <summary>
        /// Fetches a range of items.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The number of items to fetch.</param>
        /// <returns></returns>
        public IList<Movie> FetchRange(int startIndex, int count)
        {
            IList<Movie> result = null;
            try
            {
                _searchCriteria.BlockSize = count;
                var task = Client.SearchAsync(_searchCriteria);

                result = task.Result;
                _searchCriteria.CodeGreaterThan = result[result.Count - 1].Code;
            }
            catch (System.Exception ex)
            {

                //var a = ex.Message;
            }

            return result;
        }
    }
}
