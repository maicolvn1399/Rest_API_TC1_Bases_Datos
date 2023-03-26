namespace REST_API.Models
{
    public class LastInsertedReservation
    {
        public int ID { get; set; } 
        public string fecha_ingreso { get; set; }
        public string fecha_salida { get; set; }
        public int cama_ID { get; set; }    

        public int paciente_ID { get; set; }
         
    }
}
