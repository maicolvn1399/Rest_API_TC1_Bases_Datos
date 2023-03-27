namespace REST_API.Models
{

    //Modelo que permite crear un JSON para representar los datos actuallizados del historial clinico asociado a un paciente
    public class UpdatedClinicalHistory
    {
        public string cedula_paciente { get; set; } = string.Empty;
        public string fecha_procedimiento { get; set; }
        public string tratamiento { get; set; } = string.Empty;
    }
}
