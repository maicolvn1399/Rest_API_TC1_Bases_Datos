using REST_API.Models;
using System.Data;
using System.Data.SqlClient;

namespace REST_API.Resources
{
    public class DatabaseConnection
    {
        public static string cadenaConexion = "Data Source=LAPTOP_MICHAEL;Initial Catalog=Tarea_Corta_1;Persist Security Info=True;User ID=michael;Password=abc13";

        public static bool ExecuteCreateReservationProcedure(Reservation json)
        {

            SqlConnection conn = new SqlConnection(cadenaConexion);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("[dbo].[sp_InsertarProcedimientoReservacion]", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Reservacion_ID", SqlDbType.Int).Value = json.reservacion_ID;
                cmd.Parameters.AddWithValue("@Procedimiento_nombre", SqlDbType.NVarChar).Value = json.procedimiento_nombre;
                int i = cmd.ExecuteNonQuery();
                return (i > 0) ? true : false;

            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        public static bool ExecuteAddPatient(Patient json)
        {
            SqlConnection conn = new SqlConnection(cadenaConexion);

            DateTime dateTime = Convert.ToDateTime(json.fecha_nac);
            DateOnly dateOnly = DateOnly.FromDateTime(dateTime); 
            string dbDate = dateOnly.ToString("yyyy-MM-dd");
            Console.WriteLine(dbDate);
            DateOnly dateOnly1 = DateOnly.ParseExact(dbDate,"yyyy-MM-dd");
            Console.WriteLine(dateOnly1);
            DateTime testDateTime = dateOnly1.ToDateTime(TimeOnly.Parse("12:00 AM"));
            Console.WriteLine(testDateTime);


            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("[dbo].[sp_Insertar_Paciente]", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Cedula", SqlDbType.NVarChar).Value = json.cedula;
                cmd.Parameters.AddWithValue("@Nombre", SqlDbType.NVarChar).Value = json.nombre;
                cmd.Parameters.AddWithValue("@Apellido1", SqlDbType.NVarChar).Value = json.apellido_1;
                cmd.Parameters.AddWithValue("@Apellido2", SqlDbType.NVarChar).Value = json.apellido_2;
                cmd.Parameters.AddWithValue("@Sexo", SqlDbType.NVarChar).Value = json.sexo;
                cmd.Parameters.AddWithValue("@Password", SqlDbType.NVarChar).Value = json.password;
                cmd.Parameters.AddWithValue("@Fecha_nac", SqlDbType.Date).Value = testDateTime;
                string cedula = json.cedula;
                int i = cmd.ExecuteNonQuery();
                //ExecuteAddPatientPhone(json);

                foreach (string phone in json.telefono)
                {
                    ExecuteAddPatientPhone(cedula, phone);
                }

                return (i > 0) ? false : true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);  
                return false;
            }
            finally
            {
                conn.Close();
            }
        }


        public static bool ExecuteAddPatientPhone(string cedula, string telefono)
        {
            SqlConnection conn = new SqlConnection(cadenaConexion);
            try
            {

                conn.Open();
                SqlCommand cmd = new SqlCommand("[dbo].[sp_InsertarTelefonoPaciente]", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                

                Console.WriteLine(cedula);
                cmd.Parameters.AddWithValue("@Paciente_cedula", SqlDbType.NVarChar).Value = cedula;
                Console.WriteLine(telefono);
                cmd.Parameters.AddWithValue("@Telefono", SqlDbType.NVarChar).Value = telefono;
                
                

                int i = cmd.ExecuteNonQuery();
                return (i > 0) ? false : true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }

        }

    }
}
