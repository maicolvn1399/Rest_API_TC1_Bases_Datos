namespace REST_API.Models
{

    //Modelo que permite crear un JSON para enviar al web app y que sea interpretado por los developers encargados del web app
    public class JSON_Object
    {
        public string status { get; set; } = string.Empty;
        public Object result { get; set; }


        public JSON_Object(string status, Object result)
        {
            this.status = status;
            this.result = result;
        }
    }
}
