namespace REST_API.Models
{
    public class Patient
    {
        public string cedula { get; set; }
        public string password { get; set; }
        public string nombre { get; set; }
        public string apellido_1 { get; set; }
        public string apellido_2 { get; set; }
        public DateTime fecha_nac { get; set; } //Puede dar problemas **
        public char sexo { get; set; }
        public int edad { get; set; }
        public List<string> telefono { get; set; }
        public List<Direccion> direccion { get; set; }

    }
}
