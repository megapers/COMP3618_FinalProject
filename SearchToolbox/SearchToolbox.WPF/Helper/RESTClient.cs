using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SearchToolbox.WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SearchToolbox.WPF
{
    public class RESTClient
    {
        const string _uriBase = "http://localhost:54754";
        const string _uriCRUD = "/api/Movies/CRUD/";
        const string _uriSearch = "/api/Movies/Search/";

        private JsonMediaTypeFormatter formatter;

        public RESTClient()
        {
            if (ApiHelper.ApiClient == null)
            {
                ApiHelper.InitializeClient();
            }

            if (formatter == null)
            {
                formatter = new JsonMediaTypeFormatter();
                formatter.SerializerSettings = new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()

                };
            }
        }

        #region CRUD OPERATIONS
        public async Task UpdateAsync(string code, Movie movie)
        {
            using (var response = await ApiHelper.ApiClient.PostAsync($"{_uriBase}{_uriCRUD}{code}", movie, formatter))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Updating failed.\n{response.ReasonPhrase}");
                }

            }
        }

        public async Task AddAsync(Movie movie)
        {
            using (var response = await ApiHelper.ApiClient.PostAsync($"{_uriBase}{_uriCRUD}", movie, formatter))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Adding failed.\n{response.ReasonPhrase}");
                }
            }
        }

        public async Task DeleteAsync(string code)
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.DeleteAsync($"{_uriBase}{_uriCRUD}{code}"))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Deleting failed.\n{response.ReasonPhrase}");
                }
            }
        }


        public async Task<Movie> GetAsync(string code)
        {

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync($"{_uriBase}{_uriCRUD}{code}"))
            {

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var movie = JsonConvert.DeserializeObject<Movie>(result);
                    return movie;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        #endregion


        #region Search Operation
        public async Task<List<Movie>> SearchAsync(SearchCriteria searchCriteria)
        {

            HttpContent httpContent = new StringContent(searchCriteria.Serialize(), Encoding.UTF8, "application/json");
            string contentString = string.Empty;
            List<Movie> result = new List<Movie>();

            using (HttpResponseMessage httpResponseMessage = await ApiHelper.ApiClient.PostAsync($"{_uriBase}{_uriSearch}", httpContent))
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

        public async Task<int> GetCountSearch(string searchFor)
        {
            string contentString = string.Empty;
            int searchMatches = 0;

            using (HttpResponseMessage httpResponseMessage = await ApiHelper.ApiClient.GetAsync($"{_uriBase}{_uriSearch}{searchFor}"))
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
        #endregion


    }
}
