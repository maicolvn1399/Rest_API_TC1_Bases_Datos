using Microsoft.AspNetCore.Mvc;
using REST_API.Models;

namespace REST_API.Controllers
{

    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {

        [HttpGet("auth_patient")]
        public async Task<ActionResult<JSON_Object>> AuthPatient([FromQuery] Credentials patient_credentials)
        {
            //revisa en la base de datos si existe el paciente y retorna la informacion del paciente
            Patient patient = new Patient();
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

