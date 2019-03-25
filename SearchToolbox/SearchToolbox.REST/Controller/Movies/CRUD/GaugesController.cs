using Microsoft.AspNetCore.Mvc;
using SearchToolbox.Interfaces;
using System.Threading.Tasks;

namespace SearchToolbox.REST.Controller.Movies.CRUD
{
    /// <summary>
    /// Controller for querying gauges
    /// </summary>
    [Produces("application/json")]
    [Route("api/Movies/CRUD")]
    public class GaugesController : ControllerBase
    {
        private IBusinessLogicLayer _businessLogicLayer;
        //private IValidation _validation;

        /// <summary>
        /// Constructor for the ClientsController
        /// </summary>
        /// <param name="businessLogicLayer">Class implementing the IBusinessLogicLayer interface</param>
        public GaugesController(IBusinessLogicLayer businessLogicLayer)
        {
            _businessLogicLayer = businessLogicLayer;
        }

        /// <summary>
        /// Read the gauge information
        /// </summary>
        /// <param name="code">Movie code to query</param>
        /// <returns>Gauge information for the specified gauge code</returns>
        [HttpGet("{code}", Name = "GetMovieAsync")]
        public async Task<IActionResult> GetMovieAsync(string code)
        {
            //short usersId;
            //int gaugesId;

            //try
            //{
            //    usersId = await _validation.ValidateUserAsync(Utilities.GetUserNameFromClaims(User));
            //    gaugesId = await _validation.ValidateGaugeCodeAsync(gaugeCode);

            return Ok(); // await _dataAccessLayer.ReadGaugeAsync(gaugesId));
            //}
            //catch (HydrometricException ex)
            //{
            //    return BadRequest(ex.Message);
            //}
        }
    }
}
