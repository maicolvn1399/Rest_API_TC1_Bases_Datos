using Microsoft.AspNetCore.Mvc;
using REST_API.Models;

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
            JSON_Object json = new JSON_Object("ok", newClinicalHistory);
            return Ok(json);

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
