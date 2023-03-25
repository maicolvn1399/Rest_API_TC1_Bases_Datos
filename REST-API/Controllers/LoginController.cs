using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using REST_API.Models;
using REST_API.Resources;
using System.Data;
using System.Linq.Expressions;

namespace REST_API.Controllers
{

    [ApiController]
    [Route("api")]
    public class LoginController : ControllerBase
    {

        [HttpGet("auth_patient")]
        public async Task<ActionResult<JSON_Object>> AuthPatient([FromQuery] Credentials patient_credentials)
        {
            //revisa en la base de datos si existe el paciente y retorna la informacion del paciente
            JSON_Object json = new JSON_Object("error", null);
            try
            {
                Patient information_patient = new Patient();

                DataTable login_data = DatabaseConnection.Login(patient_credentials);

                DataRow dataRow = login_data.Rows[0];

                int patient_age = Convert.ToInt32(dataRow["Edad"]);

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

                List<string> phone_list = new List<string>();

                DataTable phones_patient = DatabaseConnection.GetPhones(patient_credentials, "patient_auth");
                foreach (DataRow row in phones_patient.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        Console.WriteLine(item.ToString());
                        phone_list.Add(item.ToString());
                    }
                }


                List<Direccion> addresses = new List<Direccion>();

                DataTable address_patient = DatabaseConnection.GetAddress(patient_credentials,"patient_auth");
                foreach (DataRow row in address_patient.Rows)
                {
                    Direccion address = new Direccion();
                    address.provincia = row["Provincia"].ToString();
                    address.canton = row["Canton"].ToString();
                    address.distrito = row["Distrito"].ToString();
                    addresses.Add(address);
                }


                information_patient.telefono = phone_list;
                information_patient.direccion = addresses;

                string info_phones_patient = JsonConvert.SerializeObject(phones_patient);
                Console.WriteLine(info_phones_patient);
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

        [HttpGet("auth_worker")]
        public async Task<ActionResult<JSON_Object>> AuthWorker([FromQuery] Credentials worker_credentials)
        {

            JSON_Object json = new JSON_Object("error", null);
            try
            {
                Worker information_worker = new Worker();

                DataTable login_data_worker = DatabaseConnection.LoginWorker(worker_credentials);

                DataRow dataRow = login_data_worker.Rows[0];

                information_worker.cedula = dataRow["Cedula"].ToString();
                information_worker.password = dataRow["Password"].ToString();
                information_worker.nombre = dataRow["Nombre"].ToString();
                information_worker.apellido_1 = dataRow["Apellido1"].ToString();
                information_worker.apellido_2 = dataRow["Apellido2"].ToString();
                information_worker.tipo = dataRow["Tipo"].ToString();
                information_worker.fecha_ingreso = dataRow["Fecha_ingreso"].ToString();
                information_worker.fecha_nacimiento = dataRow["Fecha_nac"].ToString();


                List<string> phone_list_worker = new List<string>();

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


