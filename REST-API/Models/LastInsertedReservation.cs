namespace REST_API.Models
{

    //Modelo que permite crear un JSON para representar los datos de la ultima reservacion guardada en la base de datos
    public class LastInsertedReservation
    {
        public int ID { get; set; } 
        public string fecha_ingreso { get; set; }
        public string fecha_salida { get; set; }
        public int cama_ID { get; set; }    

        public int paciente_ID { get; set; }
         
    }
}
