namespace REST_API.Models
{

    //Modelo que permite crear un JSON para representar las credenciales de un paciente o doctor que hace login en el sistema 
    public class Credentials
    {
        public string cedula { get; set; }
        public string password { get; set; }
    }
}
