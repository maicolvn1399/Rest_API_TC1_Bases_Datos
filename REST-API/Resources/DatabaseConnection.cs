using Microsoft.AspNetCore.Components.Web;
using REST_API.Models;
using System;
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
            DateOnly dateOnly1 = DateOnly.ParseExact(dbDate, "yyyy-MM-dd");
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

                foreach(Direccion direccion in json.direccion)
                {
                    ExecuteAddPatientAddress(cedula,direccion.provincia, direccion.canton, direccion.distrito);
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
                return (i > 0) ? true : false;

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


        public static bool ExecuteAddPatientAddress(string cedula,string provincia, string canton, string distrito)
        {
            SqlConnection conn = new SqlConnection(cadenaConexion);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("[dbo].[InsertarDireccionPaciente]", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Paciente_cedula", SqlDbType.NVarChar).Value = cedula;
                cmd.Parameters.AddWithValue("@Provincia", SqlDbType.NVarChar).Value = provincia;
                cmd.Parameters.AddWithValue("@Canton", SqlDbType.NVarChar).Value = canton;
                cmd.Parameters.AddWithValue("@Distrito", SqlDbType.NVarChar).Value = distrito;

                int i = cmd.ExecuteNonQuery();
                return (i > 0) ? true : false;

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        public static bool ExecuteAddClinicalHistory(ClinicalHistory newClinicalHistory)
        {

            DateTime dateTime = Convert.ToDateTime(newClinicalHistory.fecha_procedimiento);
            DateOnly dateOnly = DateOnly.FromDateTime(dateTime);
            string dbDate = dateOnly.ToString("dd/MM/yyyy");
            Console.WriteLine(dbDate);
            DateOnly dateOnly1 = DateOnly.ParseExact(dbDate, "dd/MM/yyyy");
            Console.WriteLine(dateOnly1);
            DateTime testDateTime = dateOnly1.ToDateTime(TimeOnly.Parse("12:00 AM"));
            Console.WriteLine(testDateTime.GetType());
            Console.WriteLine(testDateTime);

            SqlConnection conn = new SqlConnection(cadenaConexion);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("[dbo].[sp_InsertarHistorialClinico]", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Paciente_cedula", SqlDbType.NVarChar).Value = newClinicalHistory.cedula_paciente;
                cmd.Parameters.AddWithValue("@Fecha_procedimiento", SqlDbType.Date).Value = dateTime;
                cmd.Parameters.AddWithValue("@Tratamiento", SqlDbType.NVarChar).Value = newClinicalHistory.tratamiento;
                cmd.Parameters.AddWithValue("@Procedimiento_nombre", SqlDbType.NVarChar).Value = newClinicalHistory.nombre_procedimiento;
                cmd.Parameters.AddWithValue("@Personal_cedula", SqlDbType.NVarChar).Value = newClinicalHistory.cedula_personal;

                int i = cmd.ExecuteNonQuery();
                return (i > 0) ? false : true;//Funciona

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



        public static bool ExecuteUpdateClinicalHistory(UpdatedClinicalHistory updatedClinicalHistory)
        {

            SqlConnection conn = new SqlConnection(cadenaConexion);

            DateTime dateTime = Convert.ToDateTime(updatedClinicalHistory.fecha_procedimiento);
            DateOnly dateOnly = DateOnly.FromDateTime(dateTime);
            string dbDate = dateOnly.ToString("dd/MM/yyyy");
            Console.WriteLine(dbDate);
            DateOnly dateOnly1 = DateOnly.ParseExact(dbDate, "dd/MM/yyyy");
            Console.WriteLine(dateOnly1);
            DateTime testDateTime = dateOnly1.ToDateTime(TimeOnly.Parse("12:00 AM"));
            Console.WriteLine(testDateTime.GetType());
            Console.WriteLine(testDateTime);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("[dbo].[sp_ActualizarHistorialClinico]", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@Paciente_cedula", SqlDbType.NVarChar).Value = updatedClinicalHistory.cedula_paciente;
                cmd.Parameters.AddWithValue("@Fecha_procedimiento", SqlDbType.Date).Value = dateTime;
                cmd.Parameters.AddWithValue("@Tratamiento", SqlDbType.NVarChar).Value = updatedClinicalHistory.tratamiento;


                int i = cmd.ExecuteNonQuery();
                return (i > 0) ? false : true;//Funciona

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



        public static DataTable Login(Credentials login_credentials)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            try
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Paciente_Login] (@Cedula, @Password)", conexion);
               
                cmd.Parameters.AddWithValue("@Cedula", SqlDbType.NVarChar).Value = login_credentials.cedula;
                cmd.Parameters.AddWithValue("@Password", SqlDbType.NVarChar).Value = login_credentials.password;


                DataTable tabla = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);

                return tabla;
            }
            catch (Exception ex)
            {
                Console.WriteLine (ex.Message); 
                return null;
            }
            finally
            {
                conexion.Close();
            }

        }

        public static DataTable GetPhones(Credentials login_credentials, string auth_side)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            if (auth_side == "patient_auth")
            {
                try
                {

                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("[dbo].[sp_ObtenerTelefonosPaciente]", conexion);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Paciente_cedula", SqlDbType.NVarChar).Value = login_credentials.cedula;

                    DataTable tabla = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tabla);


                    return tabla;
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    conexion.Close();
                }
            }
            else
            {
                try
                {

                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("[dbo].[Obtener_Telefonos_Personal]", conexion);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@cedula_personal", SqlDbType.NVarChar).Value = login_credentials.cedula;

                    DataTable tabla = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tabla);


                    return tabla;
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    conexion.Close();
                }


            }

        }



        public static DataTable GetAddress(Credentials login_credentials,string auth_side)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            if(auth_side == "patient_auth")
            {
                try
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("[dbo].[Obtener_Direccion_Paciente]", conexion);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cedula_paciente", SqlDbType.NVarChar).Value = login_credentials.cedula;
                    DataTable tabla = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tabla);


                    return tabla;
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally { conexion.Close(); }
            }
            else
            {
                try
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("[dbo].[Obtener_Direccion_Personal]", conexion);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cedula_personal", SqlDbType.NVarChar).Value = login_credentials.cedula;
                    DataTable tabla = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tabla);


                    return tabla;
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally { conexion.Close(); }
            }
           
        }

        public static DataTable LoginWorker(Credentials login_credentials)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            try
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Personal_Login] (@Cedula, @Password)", conexion);

                cmd.Parameters.AddWithValue("@Cedula", SqlDbType.NVarChar).Value = login_credentials.cedula;
                cmd.Parameters.AddWithValue("@Password", SqlDbType.NVarChar).Value = login_credentials.password;


                DataTable tabla = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);

                return tabla;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                conexion.Close();
            }

        }


        public static bool DeletePatientPhone(NewPatientPhone patientPhoneToDelete)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            try
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("[dbo].[sp_BorrarTelefonoPaciente]", conexion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@Paciente_cedula", SqlDbType.NVarChar).Value = patientPhoneToDelete.cedula;

                cmd.Parameters.AddWithValue("@Telefono", SqlDbType.NVarChar).Value = patientPhoneToDelete.telefono;


                int i = cmd.ExecuteNonQuery();
                return (i > 0) ? false : true;

            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                conexion.Close();
            }
        }


        public static DataTable GetClinicalHistory(Identification identification)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            try
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[buscarHistorialClinico] (@cedula)", conexion);

                cmd.Parameters.AddWithValue("@cedula", SqlDbType.NVarChar).Value = identification.cedula;
                
                DataTable tabla = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);

                return tabla;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                conexion.Close();
            }
        }


        public static bool CreateReservation(ReservationFields reservation)
        {


            SqlConnection conn = new SqlConnection(cadenaConexion);

            DateTime dateTime = Convert.ToDateTime(reservation.fecha_ingreso);
            DateOnly dateOnly = DateOnly.FromDateTime(dateTime);
            string dbDate = dateOnly.ToString("dd/MM/yyyy");
            Console.WriteLine(dbDate);
            DateOnly dateOnly1 = DateOnly.ParseExact(dbDate, "dd/MM/yyyy");
            Console.WriteLine(dateOnly1);
            DateTime testDateTime = dateOnly1.ToDateTime(TimeOnly.Parse("12:00 AM"));
            Console.WriteLine(testDateTime.GetType());
            Console.WriteLine(testDateTime);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("[dbo].[BuscarCamaDisponible]", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Cedula", SqlDbType.NVarChar).Value = reservation.cedula;
                cmd.Parameters.AddWithValue("@FechaIngreso", SqlDbType.Date).Value = dateTime;
                

                int i = cmd.ExecuteNonQuery();
                return (i > 0) ? false : true;//Funciona

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

    

