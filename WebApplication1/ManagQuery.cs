using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class ManagQuery
    {
        string connectionstring;
        private readonly IConfiguration _configuration;

        public ManagQuery(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionstring = _configuration.GetConnectionString("test");
        }

     
        
      
        public ManagQuery()
        {
               
        }
        public int ExecuteNonQuery(string sql, CommandType commandType, params SqlParameter[] parameters)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand(sql, sqlConnection))
                {
                    command.CommandType = commandType; // סוג הפקודה (SELECT, INSERT, UPDATE, DELETE)
                    command.Parameters.AddRange(parameters); // הוספת פרמטרים ל־SqlCommand

                    sqlConnection.Open();

                    int rowsAffected = command.ExecuteNonQuery(); // ביצוע השאילתה וקבלת מספר השורות המושפעות

                    return rowsAffected;
                }
            }
        }

        public DataTable ExecuteQuery(string sql, CommandType commandType, params SqlParameter[] parameters)
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
                {
                    using (SqlCommand command = new SqlCommand(sql, sqlConnection))
                    {
                        command.CommandType = commandType;
                        command.Parameters.AddRange(parameters);

                        sqlConnection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                dataTable.Load(reader); // Load data into dataTable
                            }
                        } // reader is automatically closed and disposed here
                    }
                }
            }
            catch (Exception ex)
            {
                // Implement error handling (logging or rethrowing) based on your application's needs
                Console.WriteLine($"Error executing query: {ex.Message}");
                throw; // Rethrow the exception to propagate it up the call stack
            }

            return dataTable;
        }


    }
}
