using Microsoft.AspNetCore.Mvc;
using SearchToolbox.Classes;
using SearchToolbox.Interfaces;
using System;
using System.Threading.Tasks;

namespace SearchToolbox.REST.Controller.Movies.CRUD
{
    /// <summary>
    /// Controller for querying gauges
    /// </summary>
    [Produces("application/json")]
    [Route("api/Movies/CRUD")]
    public class MovieController : ControllerBase
    {
        private IBusinessLogicLayer _businessLogicLayer;

        /// <summary>
        /// Constructor for the ClientsController
        /// </summary>
        /// <param name="businessLogicLayer">Class implementing the IBusinessLogicLayer interface</param>
        public MovieController(IBusinessLogicLayer businessLogicLayer)
        {
            _businessLogicLayer = businessLogicLayer;
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

            return Ok(_businessLogicLayer.ReadMovie(code));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Writes a new movie to the database
        /// </summary>
        /// <param name="movie">Object of type "Movie"</param>
        /// <returns></returns>
        [HttpPost(Name = "AddMovie")]
        public IActionResult AddMovie([FromBody] Movie movie)
        {
            try
            {
                return Ok(_businessLogicLayer.AddMovie(movie));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates a movie to the database
        /// </summary>
        /// <param name="code">Movie code to update</param>
        /// <param name="movie">Object of type "Movie"</param>
        /// <returns></returns>
        [HttpGet("{code}", Name = "UpdateMovie")]
        public IActionResult UpdateMovie(string code, [FromBody] Movie movie)
        {
            try
            {
                return Ok(_businessLogicLayer.UpdateMovie(code, movie));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a movie to the database
        /// </summary>
        /// <param name="code">Movie code to delete</param>
        /// <returns></returns>
        [HttpDelete("{code}", Name = "DeleteMovie")]
        public IActionResult DeleteMovie(string code)
        {
            try
            {
                return Ok(_businessLogicLayer.DeleteMovie(code));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
