using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace server.Controllers
{
    [ApiController]
    [Route("api/data")]
    public class DataController : ControllerBase
    {
        private readonly string SQLdotaz;
        private readonly string? connectionString;

        public DataController(IConfiguration configuration)
        {
            SQLdotaz = System.IO.File.ReadAllText(AppContext.BaseDirectory + "/filter.sql");
            connectionString = configuration.GetConnectionString("MyConnectionString");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetData(string? query = "")
        {
            try
            {
                DataTable dataTable = new DataTable();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(SQLdotaz, connection);
                    if (!string.IsNullOrEmpty(query))
                    {
                        command.Parameters.Add(new SqlParameter("hledanyVyraz", query));
                    }
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    dataAdapter.Fill(dataTable);
                }

                List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
                foreach (DataRow row in dataTable.Rows)
                {
                    Dictionary<string, object> rowData = new Dictionary<string, object>();
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        rowData[column.ColumnName] = row[column];
                    }
                    data.Add(rowData);
                }

                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}