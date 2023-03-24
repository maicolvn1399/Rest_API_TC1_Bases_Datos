using Microsoft.AspNetCore.Mvc;
using REST_API.Models;
using System.Linq.Expressions;

namespace REST_API.Controllers
{

    [ApiController]
    [Route("api")]
    public class LoginController : ControllerBase
    {

        [HttpGet("auth_patient")]
        public async Task<ActionResult<JSON_Object>> AuthPatient([FromQuery] Credentials patient_credentials)
        {
            //revisa en la base de datos si existe el paciente y retorna la informacion del paciente
            Patient patient = new Patient();
            patient.cedula = "3052220111";
            patient.password = "password";
            patient.nombre = "marco";
            patient.apellido_1 = "mora";
            patient.apellido_2 = "lopez";
            patient.fecha_nac = "13/10/1990";
            patient.sexo = "masculino";
            patient.edad = 30;
            patient.telefono = new List<string>
            {
                "25481010"
            };
            Direccion direc = new Direccion();
            direc.provincia = "cartago";
            direc.canton = "centraal";
            direc.distrito = "whatever";

            patient.cedula = patient_credentials.cedula;
            patient.password = patient_credentials.password;
            patient.direccion = new List<Direccion>();
            patient.direccion.Add(direc);

            JSON_Object json = new JSON_Object("ok", patient);
            return Ok(json);
        }

        [HttpGet("auth_worker")]
        public async Task<ActionResult<JSON_Object>> AuthWorker([FromQuery] Credentials worker_credentials)
        {
            //revisa en la base de datos si existe el personal y retorna la informacion del personal
            Worker worker = new Worker();
            worker.cedula = worker_credentials.cedula;
            worker.password = worker_credentials.password;
            JSON_Object json = new JSON_Object("ok", worker);
            return Ok(json);
        }
    }
}

