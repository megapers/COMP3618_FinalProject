using Newtonsoft.Json;
using System;

namespace SearchToolbox.REST.Classes
{
    /// <summary>
    /// Class used to pass serach criteria to the Web API
    /// </summary>
    [Serializable]
    public class SearchCriteria
    {
        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public SearchCriteria()
        {
        }

        /// <summary>
        /// Override constructor
        /// </summary>
        /// <param name="searchFor">Code part to serach for</param>
        /// <param name="codeGreaterThan">Last movie code of the previous search block</param>
        /// <param name="blockSize">Number of results to return in the result block</param>
        public SearchCriteria(string searchFor, string codeGreaterThan = "", int blockSize = 1000)
        {
            SearchFor = SearchFor;
            CodeGreaterThan = codeGreaterThan;
            BlockSize = blockSize;
        }
        #endregion

        /// <summary>
        /// Code part to serach for
        /// </summary>
        public string SearchFor { get; set; } = string.Empty;

        ///
        ///Last movie code of the previous search block
        ///
        public string CodeGreaterThan { get; set; } = string.Empty;

        /// <summary>
        /// Number of results to return in the result block
        /// </summary>
        public int BlockSize { get; set; } = 1000;

        /// <summary>
        /// Serializes the class to json
        /// </summary>
        /// <returns>Json string representation of class</returns>
        public string Serialize()
        {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Include,
                StringEscapeHandling = StringEscapeHandling.EscapeHtml
            };

            return JsonConvert.SerializeObject(this, Formatting.None, jsonSerializerSettings);
        }

        /// <summary>
        /// Override of the 'ToString' method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Search For: {SearchFor}\nCode Grater Than: {CodeGreaterThan}\n" +
                $"Block Size: {BlockSize}";
        }
    }
}
