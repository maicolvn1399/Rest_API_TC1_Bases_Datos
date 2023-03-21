using Microsoft.AspNetCore.Mvc;
using REST_API.Models;

namespace REST_API.Controllers
{
    [ApiController]
    [Route("api")]
    public class ProcedureController : ControllerBase
    {
        [HttpGet("get_procedure_days")]
        public async Task<ActionResult<JSON_Object>> GetProcedureDays(ProcedureIdentifier procedure_name)
        {
            //Obtener el procedimiento y retornar el nombre del procedimiento y dias 
            Procedure procedure = new Procedure();
            procedure.nombre_procedimiento = "cirugia de cataratas";
            procedure.cantidad_dias = 1;

            JSON_Object json = new JSON_Object("ok", procedure);
            return Ok(json);    

        }
    }
}
