using demoApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demoApp.Services
{
    public interface ILocationService
    {
        /// <summary>
        /// Add bulk location data from csv
        /// </summary>
        /// <returns>Boolean result based on status of the operation</returns>
        bool AddBulkLocationData();

        /// <summary>
        /// Gets all the location based on the given start and end time
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns>Result in the form of list of location records</returns>
        List<Location> GetAllLocationsBasedOnTime(TimeSpan startTime, TimeSpan endTime);

        /// <summary>
        /// Adds location data
        /// </summary>
        /// <param name="location"></param>
        /// <returns>Boolean result based on status of the operation</returns>
        bool AddLocation(Location location);

    }
}
