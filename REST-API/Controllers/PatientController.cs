using Microsoft.AspNetCore.Mvc;
using REST_API.Models;
using REST_API.Resources;

namespace REST_API.Controllers
{

    [ApiController]
    [Route("api")]
    public class PatientController : ControllerBase
    {
        [HttpPost("create_patient")]
        public async Task<ActionResult<JSON_Object>> CreatePatient(Patient new_patient)
        {
            JSON_Object json = new JSON_Object("ok", null);

            bool var = DatabaseConnection.ExecuteAddPatient(new_patient);
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
        
        [HttpDelete("delete_phone")]
        public async Task<ActionResult<JSON_Object>> DeletePhone(NewPatientPhone deletePatientPhone)
        {
            JSON_Object json = new JSON_Object("ok", null);

            bool var = DatabaseConnection.DeletePatientPhone(deletePatientPhone);
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
