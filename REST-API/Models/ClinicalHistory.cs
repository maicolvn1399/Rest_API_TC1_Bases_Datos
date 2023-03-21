namespace REST_API.Models
{
    public class ClinicalHistory
    {
        public string cedula_paciente { get; set; } = string.Empty;
        public DateTime fecha_procedimiento { get; set; }
        public string tratamiento { get; set; } = string.Empty;
        public string nombre_procedimiento { get;set; } = string.Empty;
        public string cedula_personal { get; set; } = string.Empty;

    }
}
