using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchToolbox.WPF.Model
{
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

    }
}
