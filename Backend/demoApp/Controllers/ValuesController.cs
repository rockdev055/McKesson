using demoApp.Models;
using demoApp.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace demoApp.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly ILocationService _locationService;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public ValuesController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        /// <summary>
        /// PopulateTableFromCSV - This API will read the CSV file and then populate the tbldemoapp table
        /// </summary>
        /// <returns>JSON API Response</returns>
        [Route("api/values/PopulateTableFromCSV")]
        [HttpGet]
        public IHttpActionResult PopulateTableFromCSV()
        {
            try
            {
                var result = _locationService.AddBulkLocationData();
                var resp = new ApiResponse();
                resp.IsSuccess = result;
                resp.Message = result ? "Success" : "Something went wrong!";
                resp.Data = "";
                _logger.Info("PopulateTableFromCSV executed successfully");
                return Ok(resp);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return BadRequest(JsonConvert.SerializeObject(new ApiResponse(false, ex.Message, null)));
            }
        }

        /// <summary>
        /// GetAllLocations - Retrieve all the locations between startTime and endTime
        /// </summary>
        /// <param name="startTime">Taking startTime input for filter</param>
        /// <param name="endTime">Taking startTime input for filter</param>
        /// <returns>JSON API Response</returns>
        [Route("api/values/GetAllLocations")]
        [HttpGet]
        public IHttpActionResult GetAllLocations(TimeSpan startTime, TimeSpan endTime)
        {
            try
            {
                var result = _locationService.GetAllLocationsBasedOnTime(startTime, endTime);
                var resp = new ApiResponse();
                resp.IsSuccess = true;
                resp.Message = "Success";
                resp.Data = result;
                _logger.Info("PopulateTableFromCSV executed successfully");
                return Ok(resp);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return BadRequest(JsonConvert.SerializeObject(new ApiResponse(false, ex.Message, null)));
            }
        }

        /// <summary>
        /// AddLocation - It will insert new Location to the database
        /// </summary>
        /// <param name="model">taking City & Time input parameters</param>
        /// <returns>JSON API Response</returns>
        [Route("api/values/AddLocation")]
        [HttpPost]
        public IHttpActionResult AddLocation(Location model)
        {
            try
            {
                var result = _locationService.AddLocation(model);
                var resp = new ApiResponse();
                resp.IsSuccess = result;
                resp.Message = result ? "Success" : "Something went wrong!";
                resp.Data = "";
                return Ok(resp);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return BadRequest(JsonConvert.SerializeObject(new ApiResponse(false, ex.Message, null)));
            }
        }
    }
}
