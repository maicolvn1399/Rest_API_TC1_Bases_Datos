using Microsoft.AspNetCore.Mvc;
using REST_API.Models;
using REST_API.Resources;

namespace REST_API.Controllers
{

    //Controlador para gestionar la informacion del paciente

    [ApiController]
    [Route("api")]
    public class PatientController : ControllerBase
    { 
        //Metodo para crear un paciente
        //Se recibe como parametro un JSON con la informacion del paciente 
        [HttpPost("create_patient")]
        public async Task<ActionResult<JSON_Object>> CreatePatient(Patient new_patient)
        {
            JSON_Object json = new JSON_Object("ok", null);

            //Se ejecuta el metodo que llama a un stored procedure en SQL para almacenar la informacion en la tabla correspondiente
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

        //Metodo que permite agregar un nuevo de telefono y asociarlo a un paciente
        [HttpPost("add_phone")]
        public async Task<ActionResult<JSON_Object>> AddPhone(NewPatientPhone newPatientPhone)
        {

            JSON_Object json = new JSON_Object("ok", null);
            //se ejecuta el metodo que llama a un stored procedure en SQL para almacenar la informacion en la tabla correspondiente
            bool var = DatabaseConnection.ExecuteAddPatientPhone(newPatientPhone);
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

        //Metodo que permite agregar una nueva direccion y asociarla a un paciente
        [HttpPost("add_address")]
        public async Task<ActionResult<JSON_Object>> AddAdress(Direccion address)
        {

            JSON_Object json = new JSON_Object("ok", null);
            //se ejecuta el metodo que llama a un stored procedure en SQL para almacenar la informacion en la tabla correspondiente
            bool var = DatabaseConnection.ExecuteAddPatientAddress(address);
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



        //Metodo que permite eliminar el numero de telefono de un paciente 
        [HttpDelete("delete_phone")]
        public async Task<ActionResult<JSON_Object>> DeletePhone(NewPatientPhone deletePatientPhone)
        {
            JSON_Object json = new JSON_Object("ok", null);
            //Metodo que llama a un stored procedure para eliminar el telefono correspondiente 
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
