namespace REST_API.Models
{
    public class UpdatedClinicalHistory
    {
        public string cedula_paciente { get; set; } = string.Empty;
        public string fecha_procedimiento { get; set; }
        public string tratamiento { get; set; } = string.Empty;
    }
}
