using Microsoft.AspNetCore.Mvc;
using REST_API.Models;

namespace REST_API.Controllers
{

    [ApiController]
    [Route("api")]
    public class PatientController : ControllerBase
    {
        [HttpPost("create_patient")]
        public async Task<ActionResult<JSON_Object>> CreatePatient(Patient new_patient)
        {
            //Se guarda el paciente nuevo en la base
            Console.WriteLine(new_patient);
            JSON_Object json = new JSON_Object("ok", null);
            return Ok(json);
        }

        [HttpPut("update_patient")]
        public async Task<ActionResult<JSON_Object>> UpdatePatient(Patient updated_patient)
        {
            //Se guarda el paciente nuevo en la base 
            Console.WriteLine($"{updated_patient}");
            JSON_Object json = new JSON_Object("ok", null);
            return Ok(json);
        }

        [HttpGet("get_patient")]
        public async Task<ActionResult<JSON_Object>> GetPatient([FromQuery] Identification identification)
        {
            //Se busca a la persona con el número de cédula
            Console.WriteLine(identification);
            JSON_Object json = new JSON_Object("ok", null);
            return Ok(json);

        }

        [HttpDelete("delete_patient")]
        public async Task<ActionResult<JSON_Object>> DeletePatient(Identification identification)
        {
            //Se busca a la persona con el número de cédula para eliminar
            Console.WriteLine(identification);
            JSON_Object json = new JSON_Object("ok", null);
            return Ok(json);

        }



    }
}
