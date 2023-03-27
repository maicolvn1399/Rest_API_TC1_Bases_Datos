namespace REST_API.Models
{

    //Modelo que permite crear un JSON para representar los datos de un paciente 
    public class Patient
    {
        public string cedula { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string nombre { get; set; } = string.Empty;
        public string apellido_1 { get; set; } = string.Empty;
        public string apellido_2 { get; set; } = string.Empty;
        public string fecha_nac { get; set; }
        public string sexo { get; set; } = string.Empty;
        public int edad { get; set; } = 0;

    }
}
