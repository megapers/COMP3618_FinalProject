using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SearchToolbox.Interfaces;
using SearchToolbox.REST.Classes;
using System;

namespace SearchToolbox.REST.Controller.Movies.Search
{
    /// <summary>
    /// Controller for searching movies
    /// </summary>
    [Produces("application/json")]
    [Route("api/Movies/Search")]
    public class SearchController : ControllerBase
    {
        private readonly IBusinessLogicLayer _businessLogicLayer;
        private readonly ILogger<SearchController> _logger;

        /// <summary>
        /// Constructor for the SearchController
        /// </summary>
        /// <param name="businessLogicLayer">Class implementing the IBusinessLogicLayer interface</param>
        /// <param name="logger">Class implementing ILoggeer to log queries against</param>
        public SearchController(
            IBusinessLogicLayer businessLogicLayer,
            ILogger<SearchController> logger)
        {
            _businessLogicLayer = businessLogicLayer;
            _logger = logger;
        }

        /// <summary>
        /// Gets the number of movies that meet the search criteria
        /// </summary>
        /// <param name="searchFor">Code part to search for</param>
        /// <returns>Count of the number of movies that meet the search criteria</returns>
        [HttpGet("{searchFor}", Name = "GetSearchMatches")]
        public IActionResult GetSearchMatches(string searchFor)
        {
            try
            {
                _logger.LogInformation($"api/Movies/Search:GetSearchMatches: " +
                    $"Search For: {searchFor}");

                return Ok(_businessLogicLayer.GetSearchMatches(searchFor));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets the next block of movies meeting the search criteria
        /// </summary>
        /// <returns>A block of movies that meet the search criteria</returns>
        [HttpPost(Name = "GetMatchingMovies")]
        public IActionResult GetMatchingMovies([FromBody] SearchCriteria searchCriteria)
        {
            try
            {
                _logger.LogInformation($"api/Movies/Search:GetSearchMatches " +
                    $"Search Criteria: {searchCriteria.ToString()}");

                return Ok(_businessLogicLayer.ReadMatchingMovies(searchCriteria.SearchFor, searchCriteria.CodeGreaterThan, searchCriteria.BlockSize));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
