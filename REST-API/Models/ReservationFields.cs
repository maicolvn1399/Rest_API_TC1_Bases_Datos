namespace REST_API.Models
{

    //Modelo que permite crear un JSON para representar los datos con los que se puede agendar una reservacion 
    public class ReservationFields
    {
        public string cedula { get; set; } = string.Empty;
        public string fecha_ingreso { get; set; }
    }
}
