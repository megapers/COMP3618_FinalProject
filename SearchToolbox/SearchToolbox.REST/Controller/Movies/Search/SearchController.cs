using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SearchToolbox.Interfaces;
using SearchToolbox.REST.Classes;
using System;

namespace SearchToolbox.REST.Controller.Movies.CRUD
{
    /// <summary>
    /// Controller for searching movies
    /// </summary>
    [Produces("application/json")]
    [Route("api/Movies/Search")]
    public class SearchController : ControllerBase
    {
        private readonly IBusinessLogicLayer _businessLogicLayer;

        /// <summary>
        /// Constructor for the SearchController
        /// </summary>
        /// <param name="businessLogicLayer">Class implementing the IBusinessLogicLayer interface</param>
        public SearchController(IBusinessLogicLayer businessLogicLayer)
        {
            _businessLogicLayer = businessLogicLayer;
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
                return Ok(_businessLogicLayer.ReadMatchingMovies(searchCriteria.SearchFor, searchCriteria.CodeGreaterThan, searchCriteria.BlockSize));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
