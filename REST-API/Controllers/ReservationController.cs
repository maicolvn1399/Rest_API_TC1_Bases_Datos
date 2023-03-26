using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using REST_API.Models;
using REST_API.Resources;
using System.Data;
using System.Data.SqlClient;



namespace REST_API.Controllers
{

    [ApiController]
    [Route("api")]
    public class ReservationController : ControllerBase
    {
        [HttpPost("create_reservation")]
        public async Task<ActionResult<JSON_Object>> CreateReservation(ReservationFields new_reservation)
        {
            JSON_Object json = new JSON_Object("ok", null);

            bool var = DatabaseConnection.CreateReservation(new_reservation);
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

        [HttpPost("insert_procedure_reservation")]
        public async Task<ActionResult<JSON_Object>> InsertProcedureReservation(Reservation reservation)
        {
            JSON_Object json = new JSON_Object("ok", null);

            bool var = DatabaseConnection.ExecuteCreateReservationProcedure(reservation);
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

        [HttpGet("get_patient_reservations")]
        public async Task<ActionResult<JSON_Object>> GetPatientReservations([FromQuery] Identification identification)
        {
            JSON_Object json = new JSON_Object("error", null);
            try
            {
                DataTable patient_reservations = DatabaseConnection.GetPatientReservations(identification);
                List<ReservationPatient> all_patient_reservations = new List<ReservationPatient>();



                foreach (DataRow row in patient_reservations.Rows)
                {
                    ReservationPatient reservations = new ReservationPatient();
                    int reservation_id_int = Convert.ToInt32(row["ID"].ToString());
                    int cama_id_int = Convert.ToInt32(row["Cama_ID"].ToString());


                    reservations.reservationId = reservation_id_int;
                    reservations.fecha_ingreso = row["Fecha_ingreso"].ToString();
                    reservations.fecha_salida = row["Fecha_salida"].ToString();
                    reservations.cama_ID = cama_id_int;
                    reservations.cedula_paciente = row["Paciente_ID"].ToString();

                    all_patient_reservations.Add(reservations);


                    List<string> procedures_list = new List<string>();

                    DataTable procedures = DatabaseConnection.GetProcedures(identification);
                    foreach (DataRow fila in procedures.Rows)
                    {
                        procedures_list.Add(fila["Procedimiento_nombre"].ToString());

                    }
                    reservations.procedimientos = procedures_list;

                }

                json.status = "ok";
                json.result = all_patient_reservations;
                return Ok(json);
            

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(json);
            }
            

        }

        [HttpDelete("delete_reservation")]
        public async Task<ActionResult<JSON_Object>> DeleteReservation(ReservationID reservationID)
        {

            JSON_Object json = new JSON_Object("error", null);
            bool var = DatabaseConnection.DeleteReservation(reservationID);
            Console.WriteLine(var);
            if (var)
            {
                json.status = "ok";
                return Ok(json);
            }
            else
            {
                
                return BadRequest(json);
            }

        }

        [HttpGet("get_last_inserted_reservation")]
        public async Task<ActionResult<JSON_Object>> GetLastInsertedReservation()
        {
            JSON_Object json = new JSON_Object("error", null);

            try
            {
                DataTable last_reservation = DatabaseConnection.GetLastInsertedReservation();




                LastInsertedReservation lastInserted = new LastInsertedReservation();
                foreach (DataRow row in last_reservation.Rows)
                {
                    

                    int id_int = Convert.ToInt32(row["ID"].ToString());
                    int cama_id_int = Convert.ToInt32(row["Cama_ID"].ToString());
                    int paciente_id_int = Convert.ToInt32(row["Paciente_ID"].ToString());


                    lastInserted.ID = id_int; 
                    lastInserted.fecha_ingreso = row["fecha_ingreso"].ToString();
                    lastInserted.fecha_salida = row["fecha_salida"].ToString();
                    lastInserted.cama_ID = cama_id_int;
                    lastInserted.paciente_ID = paciente_id_int;

                }

                json.status = "ok";
                json.result = lastInserted;
                return Ok(json);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(json);
            }


        }





        }
}
