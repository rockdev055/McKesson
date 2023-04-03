using demoApp.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace demoApp.Services
{
    public class LocationService : ILocationService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        SqlConnection con;
        private void connection()
        {
            con = new SqlConnection(DBService.connectionString);
        }

        /// <summary>
        /// Add bulk location data from csv
        /// </summary>
        /// <returns>Boolean result based on status of the operation</returns>
        public bool AddBulkLocationData()
        {
            var result = false;
            try
            {
                DataTable tblcsv = new DataTable();
                tblcsv.Columns.Add("City");
                tblcsv.Columns.Add("Time");
                // specify CSV Path
                string CSVFilePath = Path.GetFullPath(HttpContext.Current.Server.MapPath("~/DataResources/inputFile.csv"));
                // Read CSV file
                string ReadCSV = File.ReadAllText(CSVFilePath);
                foreach (string csvRow in ReadCSV.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(csvRow))
                    {
                        tblcsv.Rows.Add();
                        int count = 0;
                        foreach (string FileRec in csvRow.Split(','))
                        {
                            tblcsv.Rows[tblcsv.Rows.Count - 1][count] = FileRec;
                            count++;
                        }
                    }
                }
                //Call InsertCSVRecords method to populate table
                result = InsertCSVRecords(tblcsv);
            }
            catch(Exception ex)
            {
                _logger.Error(ex, ex.Message);
                throw;
            }
            return result;
        }

        /// <summary>
        /// Gets all the location based on the given start and end time
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns>Result in the form of list of location records</returns>
        public List<Location> GetAllLocationsBasedOnTime(TimeSpan startTime, TimeSpan endTime)
        {
            List<Location> result = new List<Location>();
            try
            {
                string selectquery = "select ID,City,Time from tblDemoApp WHERE Time BETWEEN @startTime AND @endTime";
                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                sqlParameters.Add(new SqlParameter("@startTime", startTime));
                sqlParameters.Add(new SqlParameter("@endTime", endTime));
                var resDT = DBService.ExecuteDataTable(selectquery, CommandType.Text, sqlParameters);
                if (resDT != null && resDT.Rows != null && resDT.Rows.Count > 0)
                {
                    foreach (DataRow row in resDT.Rows)
                    {
                        result.Add(new Location()
                        {
                            ID = Convert.ToInt32(row["ID"]),
                            City = row["City"].ToString(),
                            Time = TimeSpan.Parse(row["Time"].ToString())
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                throw;
            }
            return result;
        }
        /// <summary>
        /// Adds location data
        /// </summary>
        /// <param name="location"></param>
        /// <returns>Boolean result based on status of the operation</returns>
        public bool AddLocation(Location location)
        {
            var result = false;
            try
            {
                string insertQuery = "INSERT INTO tblDemoApp(City,Time) VALUES (@City, @Time);";
                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                sqlParameters.Add(new SqlParameter("@City", location.City));
                sqlParameters.Add(new SqlParameter("@Time", location.Time));
                var res = DBService.ExecuteNonQuery(insertQuery, CommandType.Text, sqlParameters);
                if (res > 0)
                    result = true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                throw;
            }
            return result;
        }

        /// <summary>
        /// InsertCSVRecords - It will do bulk insert to tblDemoApp
        /// </summary>
        /// <param name="csvDataTable">It will contain the CSV file data as a DataTable form</param>
        /// <returns>bool - true/false</returns>
        private bool InsertCSVRecords(DataTable csvDataTable)
        {
            try
            {
                connection();
                // SqlBulkCopy command Initialization and it's configuration, specify tableName, do the column mappings then finally write to server
                SqlBulkCopy objbulk = new SqlBulkCopy(con);
                objbulk.DestinationTableName = "tblDemoApp";
                objbulk.ColumnMappings.Add("City", "City");
                objbulk.ColumnMappings.Add("Time", "Time");
                con.Open();
                objbulk.WriteToServer(csvDataTable);
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return false;
            }
        }
    }
}
