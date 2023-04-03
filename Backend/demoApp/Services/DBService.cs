using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace demoApp
{
    public static class DBService
    {
        //Getting connection string from the web config
        public static readonly string connectionString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        /// <summary>
        /// Executes the sql query(insert/update) using the parameters passed
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns>Integer result based on status of the operation</returns>
        public static int ExecuteNonQuery(string commandText, CommandType commandType, List<SqlParameter> parameters = null)
        {
            int result = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(commandText, connection))
            {
                command.CommandType = commandType;

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters.ToArray());
                }

                connection.Open();
                result = command.ExecuteNonQuery();
            }

            return result;
        }

        /// <summary>
        /// Executes the sql query(select) using the parameters passed
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns>Object result based on status of the operation</returns>
        public static object ExecuteScalar(string commandText, CommandType commandType, List<SqlParameter> parameters = null)
        {
            object result = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(commandText, connection))
            {
                command.CommandType = commandType;

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters.ToArray());
                }

                connection.Open();
                result = command.ExecuteScalar();
            }

            return result;
        }

        /// <summary>
        /// Executes the sql query(select) using the parameters passed
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns>SqlDataReader result based on status of the operation</returns>
        public static SqlDataReader ExecuteReader(string commandText, CommandType commandType, List<SqlParameter> parameters = null)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(commandText, connection);
            command.CommandType = commandType;

            if (parameters != null)
            {
                command.Parameters.AddRange(parameters.ToArray());
            }

            connection.Open();
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        /// <summary>
        /// Executes the sql query(select) using the parameters passed
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns>DataTable result based on status of the operation</returns>
        public static DataTable ExecuteDataTable(string commandText, CommandType commandType, List<SqlParameter> parameters = null)
        {
            DataTable result = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(commandText, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.CommandType = commandType;

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters.ToArray());
                }

                adapter.Fill(result);
            }

            return result;
        }
    }
}
