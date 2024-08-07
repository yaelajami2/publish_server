using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.Core.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
   
    public class LocationController : ControllerBase
    {
        private readonly ManagQuery managQuery; // Replace with your actual manager or service

        public LocationController(ManagQuery managQuery)
        {
            this.managQuery = managQuery;
        }

        [HttpGet]
        public IActionResult Get()
        {

      
            DataTable dataTable = managQuery.ExecuteQuery("select * from [dbo].[Table_1]", CommandType.Text);

            // Check if dataTable is null or empty, handle errors if needed
            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                return NotFound(); // Or return appropriate response
            }

            // Convert DataTable to a list of anonymous objects or a custom DTO
            var data = ConvertDataTableToList(dataTable);

            // Serialize the converted data into JSON
            //var json = JsonSerializer.Serialize(data);

            // Return the serialized JSON as OkResult
            return Ok(data);
        }

        public class Location
        {
            public int Id { get; set; }
     
            public string name_Location { get; set; }


        }

        [HttpPost("PostLocation")]
        public IActionResult Add_Location([FromBody] string Name)
        {
            if (Name == null )
            {
                return BadRequest("Invalid JSON data");
            }

            try
            {

               
                SqlParameter sqlParameter = new SqlParameter("name", Name);
                int data_row = managQuery.ExecuteNonQuery("insert into [dbo].[Table_1] values(@name)", CommandType.Text, sqlParameter);

                // Process the 'name' value as needed

            }
            catch (Exception ex)
            {
                return BadRequest($"Error processing request: {ex.Message}");
            }

            // Check if dataTable is null or empty, handle errors if needed


            // Convert DataTable to a list of anonymous objects or a custom DTO


            // Serialize the converted data into JSON
            //var json = JsonSerializer.Serialize(data);

            // Return the serialized JSON as OkResult
            return Ok(0);
        }
      

        [HttpDelete("DeleteLocation")]
        public IActionResult DeleteLocation([FromBody] Location object_p)
        {
            if (object_p == null)
            {
                return BadRequest("Invalid JSON data");
            }

            try
            {


                SqlParameter sqlParameter = new SqlParameter("id", object_p.Id);
                int data_row = managQuery.ExecuteNonQuery(" delete [dbo].[Table_1] where id=@id", CommandType.Text, sqlParameter);

                // Process the 'name' value
            }
            catch (Exception ex)
            {
                return BadRequest($"Error processing request: {ex.Message}");
            }

            // Check if dataTable is null or empty, handle errors if needed


            // Convert DataTable to a list of anonymous objects or a custom DTO


            // Serialize the converted data into JSON
            //var json = JsonSerializer.Serialize(data);

            // Return the serialized JSON as OkResult
            return Ok(0);
        }
        [HttpPut("PutLocation")]
        public IActionResult PutLocation([FromBody] Location object_p)
        {
            if (object_p == null)
            {
                return BadRequest("Invalid JSON data");
            }

            try
            {


                SqlParameter sqlParameter = new SqlParameter("id", object_p.Id);
                SqlParameter name_Location = new SqlParameter("name_Location", object_p.name_Location);
                int data_row = managQuery.ExecuteNonQuery("update  Table_1 set [name_Location]=@name_Location where id=@id", CommandType.Text, sqlParameter, name_Location);

                // Process the 'name' value as needed
        
            }
            catch (Exception ex)
            {
                return BadRequest($"Error processing request: {ex.Message}");
            }

            // Check if dataTable is null or empty, handle errors if needed


            // Convert DataTable to a list of anonymous objects or a custom DTO


            // Serialize the converted data into JSON
            //var json = JsonSerializer.Serialize(data);

            // Return the serialized JSON as OkResult
            return Ok(0);
        }
        private List<object> ConvertDataTableToList(DataTable table)
        {
            var result = new List<object>();

            foreach (DataRow row in table.Rows)
            {
                var obj = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    obj[col.ColumnName] = row[col];
                }
                result.Add(obj);
            }

            return result;
        }
        //[HttpGet]
        //public IActionResult Get_Location()
        //{
        //    return Ok();
        //}
    }
}
