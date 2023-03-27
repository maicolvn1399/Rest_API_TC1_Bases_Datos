namespace REST_API.Models
{

    //Modelo que permite crear un JSON para representar los datos de un nuevo telefono que se asocia a un paciente
    public class NewPatientPhone
    {
        public string cedula { get; set; }  = string.Empty;
        public string telefono { get; set; } = string.Empty;
    }
}
