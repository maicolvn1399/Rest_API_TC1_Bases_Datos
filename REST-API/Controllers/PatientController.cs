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

        [HttpGet("get_phone")]
        public async Task<ActionResult<JSON_Object>> GetPhone([FromQuery] Identification identification)
        {
            //Se busca los números de telefono para obtener los telefonos de una persona
            PatientPhone patientPhone = new PatientPhone();
            patientPhone.telefono = "85859656";
            JSON_Object json = new JSON_Object("ok", patientPhone);
            return Ok(json);

        }

        [HttpPost("add_phone")]
        public async Task<ActionResult<JSON_Object>> AddPhone(NewPatientPhone newPatientPhone)
        {
            //añadir el telefono con la cedula del paciente en la base 
            JSON_Object json = new JSON_Object("ok",newPatientPhone);
            return Ok(json);

        }

        [HttpDelete("delete_phone")]
        public async Task<ActionResult<JSON_Object>> DeletePhone(NewPatientPhone deletePatientPhone)
        {
            //añadir el telefono con la cedula del paciente en la base 
            JSON_Object json = new JSON_Object("ok", deletePatientPhone);
            return Ok(json);

        }

    }
}
