using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SearchToolbox.Classes;
using SearchToolbox.Interfaces;
using System;
using System.Threading.Tasks;

namespace SearchToolbox.REST.Controller.Movies.CRUD
{
    /// <summary>
    /// Controller for performing CRID operations on movies
    /// </summary>
    [Produces("application/json")]
    [Route("api/Movies/CRUD")]
    public class MovieController : ControllerBase
    {
        private readonly IBusinessLogicLayer _businessLogicLayer;
        private readonly ILogger<MovieController> _logger;

        /// <summary>
        /// Constructor for the MovieController
        /// </summary>
        /// <param name="businessLogicLayer">Class implementing the IBusinessLogicLayer interface</param>
        /// <param name="logger">Class implementing ILoggeer to log queries against</param>
        public MovieController(
            IBusinessLogicLayer businessLogicLayer,
            ILogger<MovieController> logger)
        {
            _businessLogicLayer = businessLogicLayer;
            _logger = logger;
        }

        /// <summary>
        /// Read the movie information
        /// </summary>
        /// <param name="code">Movie code to query</param>
        /// <returns>Movie information for the specified movie code</returns>
        [HttpGet("{code}", Name = "GetMovie")]
        public IActionResult GetMovie(string code)
        {
            try
            {
                _logger.LogInformation($"api/Movies/CRUD:GetMovie: " +
                    $"Code: {code}");

                return Ok(_businessLogicLayer.ReadMovie(code));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Adds a new movie
        /// </summary>
        /// <param name="movie">Object of type "Movie"</param>
        /// <returns>Flag indicating success / failure</returns>
        [HttpPost(Name = "AddMovie")]
        public IActionResult AddMovie([FromBody] Movie movie)
        {
            try
            {
                _logger.LogInformation($"api/Movies/CRUD:AddMovie: " +
                    $"Movie: {movie.ToString()}");

                return Ok(_businessLogicLayer.AddMovie(movie));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates a movie
        /// </summary>
        /// <param name="code">Movie code to update</param>
        /// <param name="movie">Object of type "Movie"</param>
        /// <returns>Flag indicating success / failure</returns>
        [HttpPost("{code}", Name = "UpdateMovie")]
        public IActionResult UpdateMovie(string code, [FromBody] Movie movie)
        {
            try
            {
                _logger.LogInformation($"api/Movies/CRUD:UpdateMovie: " +
                    $"Code: {code} " +
                    $"Movie: {movie.ToString()}");

                return Ok(_businessLogicLayer.UpdateMovie(code, movie));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a movie
        /// </summary>
        /// <param name="code">Movie code to delete</param>
        /// <returns>Flag indicating success / failure</returns>
        [HttpDelete("{code}", Name = "DeleteMovie")]
        public IActionResult DeleteMovie(string code)
        {
            try
            {
                _logger.LogInformation($"api/Movies/CRUD:DeleteMovie: " +
                    $"Code: {code}");

                    return Ok(_businessLogicLayer.DeleteMovie(code));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
