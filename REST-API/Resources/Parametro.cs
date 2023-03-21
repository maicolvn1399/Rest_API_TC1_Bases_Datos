using Microsoft.Extensions.Options;

namespace REST_API.Resources
{
    public class Parametro
    {

        public Parametro(string nombre, string valor)
        {
            this.Nombre = nombre;
            this.Valor = valor;
        }
        public string Nombre { get; set; }
        public string Valor { get; set; }   
    }
}
