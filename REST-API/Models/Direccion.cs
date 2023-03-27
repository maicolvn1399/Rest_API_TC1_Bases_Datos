namespace REST_API.Models
{

    //Modelo que permite crear un JSON para representar los datos de una direccion de un paciente o doctor
    public class Direccion
    {

        public string cedula { get; set; } 
        public string provincia { get; set; }
        public string canton { get; set; }
        public string distrito { get; set; }
    }
}
