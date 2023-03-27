using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using REST_API.Models;
using REST_API.Resources;
using System.Data;
using System.Linq.Expressions;

namespace REST_API.Controllers
{
    //Controlador del API que permite gestionar Logins de la vista paciente y doctor
    [ApiController]
    [Route("api")]
    public class LoginController : ControllerBase
    {


        //Metodo para autenticar al paciente 
        //Se recibe como parametro un JSON con las credenciales del paciente (cedula y password) 
        //Se retorna un JSON con la informacion del paciente si este es encontrado en la base y la contraseña hace match con la que se proporciona 
        [HttpGet("auth_patient")]
        public async Task<ActionResult<JSON_Object>> AuthPatient([FromQuery] Credentials patient_credentials)
        {
            //revisa en la base de datos si existe el paciente y retorna la informacion del paciente
            JSON_Object json = new JSON_Object("error", null);
            try
            {
                Patient information_patient = new Patient();

                DataTable login_data = DatabaseConnection.Login(patient_credentials); //Llamada al metodo que ejecuta la funcion en SQL que retorna una tabla con la informacion del paciente

                DataRow dataRow = login_data.Rows[0];

                int patient_age = Convert.ToInt32(dataRow["Edad"]);
                //Conversion de tipo de datos de DataTable a objeto tipo Patient para crear el JSON que retorna el método
                information_patient.cedula = dataRow["Cedula"].ToString();
                information_patient.nombre = dataRow["Nombre"].ToString();
                information_patient.apellido_1 = dataRow["Apellido1"].ToString();
                information_patient.apellido_2 = dataRow["Apellido2"].ToString();
                information_patient.sexo = dataRow["Sexo"].ToString();
                information_patient.password = dataRow["Password"].ToString();
                information_patient.fecha_nac = dataRow["Fecha_nac"].ToString();
                information_patient.edad = patient_age;

                string info_patient = JsonConvert.SerializeObject(login_data);
                Console.WriteLine(info_patient);


                json.status = "ok";
                json.result = information_patient;

                return Ok(json);

            } catch (Exception ex)
            {
                Console.WriteLine("El paciente no se encuentra en base de datos, debe crearse una cuenta");
                json.result = "El paciente no se encuentra en base de datos, debe crearse una cuenta";
                return BadRequest(json);
            }
            
        }


        //Metodo para autenticar al doctor o personal 
        //Se recibe como parametro un JSON con las credenciales del doctir  (cedula y password) 
        //Se retorna un JSON con la informacion del doctor si este es encontrado en la base y la contraseña hace match con la que se proporciona 
        [HttpGet("auth_worker")]
        public async Task<ActionResult<JSON_Object>> AuthWorker([FromQuery] Credentials worker_credentials)
        {

            JSON_Object json = new JSON_Object("error", null);
            try
            {
                Worker information_worker = new Worker();

                DataTable login_data_worker = DatabaseConnection.LoginWorker(worker_credentials);//Llamada al metodo que ejecuta la funcion en SQL que retorna una tabla con la informacion del doctor

                DataRow dataRow = login_data_worker.Rows[0];
                //Conversion de tipo de datos de DataTable a objeto tipo Worker para crear el JSON que retorna el método
                information_worker.cedula = dataRow["Cedula"].ToString();
                information_worker.password = dataRow["Password"].ToString();
                information_worker.nombre = dataRow["Nombre"].ToString();
                information_worker.apellido_1 = dataRow["Apellido1"].ToString();
                information_worker.apellido_2 = dataRow["Apellido2"].ToString();
                information_worker.tipo = dataRow["Tipo"].ToString();
                information_worker.fecha_ingreso = dataRow["Fecha_ingreso"].ToString();
                information_worker.fecha_nacimiento = dataRow["Fecha_nac"].ToString();


                List<string> phone_list_worker = new List<string>();
                //Obtencion de telefonos pertenecientes a un doctor o personal 
                DataTable phones_worker = DatabaseConnection.GetPhones(worker_credentials, "worker_auth");
                foreach (DataRow row in phones_worker.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        Console.WriteLine(item.ToString());
                        phone_list_worker.Add(item.ToString());
                    }
                }

                List<Direccion> addresses = new List<Direccion>();
                //Obtencion de direcciones asociadas a un doctor o personal
                DataTable address_worker = DatabaseConnection.GetAddress(worker_credentials, "worker_auth");
                foreach (DataRow row in address_worker.Rows)
                {
                    Direccion address = new Direccion();
                    address.provincia = row["Provincia"].ToString();
                    address.canton = row["Canton"].ToString();
                    address.distrito = row["Distrito"].ToString();
                    addresses.Add(address);
                }

                information_worker.direccion = addresses;
                information_worker.telefono = phone_list_worker;
                json.status = "ok";
                json.result = information_worker;
                return Ok(json);


            }
            catch(Exception ex)
            {
                Console.WriteLine("El personal no se encuentra en base de datos, revise los datos ingresados");
                json.result = "El personal no se encuentra en base de datos, revise los datos ingresados";
                return BadRequest(json);
            }



        }
    }
}


