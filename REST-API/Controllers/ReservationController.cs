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
        public async Task<ActionResult<JSON_Object>> CreateReservation(ReservationFields new_reservation)
        {
            JSON_Object json = new JSON_Object("ok", null);

            bool var = DatabaseConnection.CreateReservation(new_reservation);
            Console.WriteLine(var);
            if (var)
            {
                return Ok(json);
            }
            else
            {
                json.status = "error";
                return BadRequest(json);
            }

        }

        [HttpPost("insert_procedure_reservation")]
        public async Task<ActionResult<JSON_Object>> InsertProcedureReservation(Reservation reservation)
        {
            JSON_Object json = new JSON_Object("ok", null);

            bool var = DatabaseConnection.ExecuteCreateReservationProcedure(reservation);
            Console.WriteLine(var);
            if (var)
            {
                return Ok(json);
            }
            else
            {
                json.status = "error";
                return BadRequest(json);
            }
        }





    }
}
