using Newtonsoft.Json;
using SearchToolbox.Classes;
using SearchToolbox.REST.Classes;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SearchToolbox.REST.Client
{
    /// <summary>
    /// RESTClient class
    /// </summary>
    public class RESTClient
    {
        HttpClient _httpClient;

        #region Constructor
        /// <summary>
        /// Overloaded constructor
        /// </summary>
        public RESTClient(Uri endpoint) : this(new HttpClient(), endpoint)
        {
        }
        /// <summary>
        /// Overloaded  constructor
        /// </summary>
        /// <param name="client">http client to use for access the REST service</param>
        public RESTClient(HttpClient client, Uri endpoint)
        {
            Endpoint = endpoint;

            _httpClient = client;
            _httpClient.BaseAddress = Endpoint;
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(@"application/json"));
            _httpClient.Timeout = new TimeSpan(0, 5, 0);
        }

        /// <summary>
        /// Enpoint URL to which to connect
        /// </summary>
        public Uri Endpoint { get; set; }
        #endregion

        #region Search
        /// <summary>
        /// Gets the number of movies that meet the search criteria
        /// </summary>
        /// <param name="searchFor">Code part to search for</param>
        /// <returns>Count of the number of movies that meet the search criteria</returns>
        public async Task<int> GetSearchMatches(string searchFor)
        {
            string contentString = string.Empty;
            int searchMatches = 0;

            using (HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync($"/api/Movies/Search/{searchFor}"))
            {
                contentString = await httpResponseMessage.Content.ReadAsStringAsync();

                if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    searchMatches = JsonConvert.DeserializeObject<int>(contentString);
                }
                else
                {
                    throw new HttpRequestException(httpResponseMessage.StatusCode.ToString(), string.IsNullOrWhiteSpace(contentString) ? null : new Exception(contentString));
                }
            }

            return searchMatches;
        }


        /// <summary>
        /// Gets the number of movies that meet the search criteria
        /// </summary>
        /// <param name="searchFor">Code part to serach for</param>
        /// <param name="codeGreaterThan">Last movie code of the previous search block</param>
        /// <param name="blockSize">Number of results to return in the result block</param>
        /// <returns>A block of movies that meet the search criteria</returns>
        public async Task<List<Movie>> ReadMatchingMovies(string searchFor, string codeGreaterThan = "", int blockSize = 1000)
        {
            HttpContent httpContent = null;
            SearchCriteria searchCriteria = new SearchCriteria(searchFor, codeGreaterThan, blockSize);
            string contentString = string.Empty;
            List<Movie> result = new List<Movie>();

            httpContent = new StringContent(searchCriteria.Serialize(), Encoding.UTF8, "application/json");

            using (HttpResponseMessage httpResponseMessage = await _httpClient.PostAsync($"/api/Movies/Search", httpContent))
            {
                contentString = await httpResponseMessage.Content.ReadAsStringAsync();

                if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    result = JsonConvert.DeserializeObject<List<Movie>>(contentString);
                }
                else
                {
                    throw new HttpRequestException(httpResponseMessage.StatusCode.ToString(), string.IsNullOrWhiteSpace(contentString) ? null : new Exception(contentString));
                }
            }

            return result;
        }

        #endregion

    }
}
