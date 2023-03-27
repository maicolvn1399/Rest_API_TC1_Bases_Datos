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

        //Metodo para añadir una nueva entrada al historial clinico de un paciente
        //Se recibe un JSON con los campos del historial clinico 

        [HttpPost("add_clinical_history")]
        public async Task<ActionResult<JSON_Object>> AddClinicalHistory(ClinicalHistory newClinicalHistory)
        {
            //Guardar el historial medico 
            JSON_Object json = new JSON_Object("ok", null);
            bool var = DatabaseConnection.ExecuteAddClinicalHistory(newClinicalHistory); //Se llama al metodo que ejecuta el stored procedure para guardar el historial nuevo 
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

        //Metodo para actualizar una entrada al historial clinico de un paciente
        //Se recibe un JSON con los campos del historial clinico que se quieren modificar

        [HttpPut("update_clinical_history")]
        public async Task<ActionResult<JSON_Object>> UpdateClinicalHistory(UpdatedClinicalHistory updatedClinicalHistory)
        {
            //Actualizar el historial medico 
            JSON_Object json = new JSON_Object("ok", null);
            bool var = DatabaseConnection.ExecuteUpdateClinicalHistory(updatedClinicalHistory); //Se llama al metodo que ejecuta al stored procedure para actualizar datos del historial clinico 
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


        //Método para obtener un objeto JSON con una lista de JSONs que representan cada entrada del historial clinico de un paciente 
        //Se recibe como parametro un JSON que contiene la idetificacion del paciente que se desea consultar
        [HttpGet("get_history")]
        public async Task<ActionResult<JSON_Object>> GetHistory([FromQuery] Identification identification)
        {
            //Buscar en la base el historial medico con la identificacion y retornar el historial 
            
            DataTable patient_history = DatabaseConnection.GetClinicalHistory(identification); //Se ejecuta el método que llama al stored procedure o funcion que devuelve una DataTable con la informacion 
            
            //Se crea una lista para almacenar la informacion de cada tupla obtenida en el objeto DataTable
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
