using Microsoft.AspNetCore.Mvc;
using REST_API.Models;
using REST_API.Resources;
using System.Data;

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
            
            DataTable patient_history = DatabaseConnection.GetClinicalHistory(identification);
            List<ClinicalHistory> all_history = new List<ClinicalHistory>();
            foreach (DataRow row in patient_history.Rows) 
            {
                ClinicalHistory clinicalHistory = new ClinicalHistory();
                clinicalHistory.cedula_paciente = row["Paciente_cedula"].ToString();
                clinicalHistory.fecha_procedimiento = row["Fecha_procedimiento"].ToString();
                clinicalHistory.tratamiento = row["Tratamiento"].ToString();
                clinicalHistory.nombre_procedimiento = row["Procedimiento_nombre"].ToString();
                clinicalHistory.cedula_personal = row["Personal_cedula"].ToString();
                all_history.Add(clinicalHistory);

            }

            JSON_Object json = new JSON_Object("ok", all_history);
            return Ok(json);
        }
    }
}
