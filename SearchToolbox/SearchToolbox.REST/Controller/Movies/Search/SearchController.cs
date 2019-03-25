using Microsoft.AspNetCore.Mvc;
using SearchToolbox.Interfaces;
using SearchToolbox.REST.Classes;
using System;

namespace SearchToolbox.REST.Controller.Movies.CRUD
{
    /// <summary>
    /// Controller for querying gauges
    /// </summary>
    [Produces("application/json")]
    [Route("api/Movies/Search")]
    public class SearchController : ControllerBase
    {
        private IBusinessLogicLayer _businessLogicLayer;

        /// <summary>
        /// Constructor for the ClientsController
        /// </summary>
        /// <param name="businessLogicLayer">Class implementing the IBusinessLogicLayer interface</param>
        public SearchController(IBusinessLogicLayer businessLogicLayer)
        {
            _businessLogicLayer = businessLogicLayer;
        }

        /// <summary>
        /// Read the movie information
        /// </summary>
        /// <param name="code">Movie code to query</param>
        /// <returns>Movie information for the specified movie code</returns>
        [HttpGet(Name = "GetSearchMatches")]
        public IActionResult GetSearchMatches([FromBody] string searchFor)
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
        /// Read the movie information
        /// </summary>
        /// <param name="code">Movie code to query</param>
        /// <returns>Movie information for the specified movie code</returns>
        [HttpGet(Name = "GetMatchingMovies")]
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
