namespace REST_API.Models
{

    //Modelo que permite crear un JSON para representar los datos de una reservacion 
    public class Reservation
    {
        public int reservacion_ID { get; set; } = 0;
        public string procedimiento_nombre { get; set;  } = string.Empty;
    }
}
