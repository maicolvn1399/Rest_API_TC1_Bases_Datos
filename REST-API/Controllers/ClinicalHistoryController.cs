using Microsoft.AspNetCore.Mvc;
using REST_API.Models;
using REST_API.Resources;

namespace REST_API.Controllers
{
    [ApiController]
    [Route("api")]
    public class ClinicalHistoryController : ControllerBase
    {
        [HttpPost("add_clinical_history")]
        public async Task<ActionResult<JSON_Object>> AddClinicalHistory(ClinicalHistory newClinicalHistory)
        {
            //Guardar el historial medico 
            JSON_Object json = new JSON_Object("ok", null);
            bool var = DatabaseConnection.ExecuteAddClinicalHistory(newClinicalHistory);
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

        [HttpPut("update_clinical_history")]
        public async Task<ActionResult<JSON_Object>> UpdateClinicalHistory(UpdatedClinicalHistory updatedClinicalHistory)
        {
            //Actualizar el historial medico 
            JSON_Object json = new JSON_Object("ok", null);
            bool var = DatabaseConnection.ExecuteUpdateClinicalHistory(updatedClinicalHistory);
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



            [HttpGet("get_history")]
        public async Task<ActionResult<JSON_Object>> GetHistory([FromQuery] Identification identification)
        {
            //Buscar en la base el historial medico con la identificacion y retornar el historial 
            ClinicalHistory _clinicalHistory = new ClinicalHistory();
            _clinicalHistory.cedula_paciente = identification.cedula;

            JSON_Object json = new JSON_Object("ok", _clinicalHistory);
            return Ok(json);
        }
    }
}
