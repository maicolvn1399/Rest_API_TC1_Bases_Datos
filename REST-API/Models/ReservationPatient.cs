namespace REST_API.Models
{
    public class ReservationPatient
    {
        public int reservationId { get; set; }
        public string fecha_ingreso { get; set; } = string.Empty;
        public string fecha_salida { get; set; } = string.Empty;
        public int cama_ID { get; set; } 
        public string cedula_paciente { get; set; } = string.Empty;
        public List<string> procedimientos { get; set; }

    }
}
