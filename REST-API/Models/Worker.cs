namespace REST_API.Models
{
    public class Worker
    {
        public string cedula { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string nombre { get; set; } = string.Empty;
        public string apellido_1 { get; set; } = string.Empty;
        public string apellido_2 { get; set; } = string.Empty;
        public string tipo { get; set; } = string.Empty;
        public string  fecha_ingreso { get; set; }
        public string  fecha_nacimiento { get; set; }

        public List<string> telefono { get; set; }
        public List<Direccion> direccion { get; set; }
    }
}
