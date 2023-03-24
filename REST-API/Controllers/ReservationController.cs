using Microsoft.AspNetCore.Mvc;
using REST_API.Models;
using REST_API.Resources;
using System.Data;
using System.Data.SqlClient;



namespace REST_API.Controllers
{

    [ApiController]
    [Route("api")]
    public class ReservationController : ControllerBase
    {
        [HttpPost("create_reservation")]
        public async Task<ActionResult<JSON_Object>> CreateReservation(Reservation new_reservation)
        {
            JSON_Object json = new JSON_Object("ok", null);

            bool var = DatabaseConnection.ExecuteCreateReservationProcedure(new_reservation);
            if (var)
            {
                return Ok(json);
            }
            json.status = "error";
            return Ok(json);
        }
        
    }
}
