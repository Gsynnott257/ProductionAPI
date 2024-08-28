using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductionAPI.Data;
using ProductionAPI.Data.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace ProductionAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ShippingReleaseController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly IConfiguration _configuration;

        public ShippingReleaseController(AppDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/Users/5
        [HttpGet("{SkidSN}")]
        public async Task<ActionResult<IEnumerable<ShippingRelease>>> GetSkid(string SkidSN)
        {
            if (!string.IsNullOrWhiteSpace(SkidSN))
            {
                var results = new List<ShippingRelease>();

                try
                {
                    using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                    {
                        await con.OpenAsync();

                        using (SqlCommand cmd = new SqlCommand("READ_FR_FINAL_RELEASE_AND_SUSPECT_LIST", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Skid_SN", SkidSN);

                            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                            {
                                if (!reader.HasRows)
                                {
                                    return NotFound(); // No data found
                                }

                                while (await reader.ReadAsync())
                                {
                                    var data = new ShippingRelease
                                    {
                                        Part_Status = reader["Part_Status"] != DBNull.Value ? reader["Part_Status"].ToString() : null,
                                        Skid_Status = reader["Skid_Status"] != DBNull.Value ? reader["Skid_Status"].ToString() : null,
                                        Internal_PUN = reader["Internal_PUN"] != DBNull.Value ? reader["Internal_PUN"].ToString() : null,
                                        Customer_PUN = reader["Customer_PUN"] != DBNull.Value ? reader["Customer_PUN"].ToString() : null,
                                        Skid_Serial_Number = reader["Skid_Serial_Number"] != DBNull.Value ? reader["Skid_Serial_Number"].ToString() : null,
                                        Part_Number = reader["Part_Number"] != DBNull.Value ? reader["Part_Number"].ToString() : null,
                                        Customer = reader["Customer"] != DBNull.Value ? reader["Customer"].ToString() : null,
                                        Full_Shipping_Label = reader["Full_Shipping_Label"] != DBNull.Value ? reader["Full_Shipping_Label"].ToString() : null,
                                        Suspect_Status = reader["Suspect_Status"] != DBNull.Value ? reader["Suspect_Status"].ToString() : null,
                                        Suspect_Reason = reader["Suspect_Reason"] != DBNull.Value ? reader["Suspect_Reason"].ToString() : null,
                                        Skid_QTY = reader["Skid_QTY"] != DBNull.Value ? reader["Skid_QTY"].ToString() : null,
                                    };
                                    results.Add(data);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception (consider using a logging framework)
                    return StatusCode(500, $"Internal server error: {ex.Message}, StackTrace: {ex.StackTrace}");
                }

                return results.Count > 0 ? Ok(results) : NotFound(); // Explicitly return NotFound if no results
            }
            return BadRequest("Invalid SkidSN");
        }

        [HttpPut("Test")]
        public IActionResult TestPut()
        {
            return Ok("PUT method is working");
        }

        [HttpPut("Ship/{partStatus}/{skidStatus}/{skidSN}/{shipLabel}/{fullShipLabel}")]
        public async Task<IActionResult> PutSkid(string partStatus, string skidStatus, string skidSN, string shipLabel, string fullShipLabel)
        {
            if (!string.IsNullOrWhiteSpace(partStatus) || !string.IsNullOrWhiteSpace(skidStatus) || !string.IsNullOrWhiteSpace(skidSN) || !string.IsNullOrWhiteSpace(shipLabel) || !string.IsNullOrWhiteSpace(fullShipLabel))
            {
                try
                {
                    //var results = new List<ShipSkid>();

                    using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                    {
                        await con.OpenAsync();

                        using (SqlCommand cmd = new SqlCommand("UPDATE_FR_FINAL_RELEASE_SHIP_STATUS_V2", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Part_Status", partStatus);
                            cmd.Parameters.AddWithValue("@Skid_Status", skidStatus);
                            cmd.Parameters.AddWithValue("@Skid_Serial_Number", skidSN);
                            cmd.Parameters.AddWithValue("@Shipping_Label", shipLabel);
                            cmd.Parameters.AddWithValue("@Full_Shipping_Label", fullShipLabel);

                            int rowsAffected = await cmd.ExecuteNonQueryAsync();
                            if (rowsAffected > 0)
                            {
                                return Ok("Updated Succesfully");
                            }
                            else
                            {
                                return NotFound("Skid Not Found");
                            }
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}, StackTrace: {ex.StackTrace}");
                }
            }
            return BadRequest();
        }
    }
}
