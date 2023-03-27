namespace REST_API.Models
{


    //Modelo que permite crear un JSON para representar los datos de un historial medico
    public class ClinicalHistory
    {

        public string cedula_paciente { get; set; } = string.Empty;
        public string fecha_procedimiento { get; set; }
        public string tratamiento { get; set; } = string.Empty;
        public string nombre_procedimiento { get; set; } = string.Empty;
        public string cedula_personal { get; set; } = string.Empty;
    }
}
