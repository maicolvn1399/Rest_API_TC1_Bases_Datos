using Microsoft.Extensions.Options;

namespace REST_API.Resources
{
    public class Parametro
    {

        public Parametro(string nombre, int valorNum)
        {
            this.Nombre = nombre;
            this.Valor_int = valorNum;

        }
        public Parametro(string nombre, DateOnly fecha)
        {
            this.Nombre = nombre;
            this.Fecha = fecha;

        }
        public Parametro(string nombre, string valor)
        {
            this.Nombre = nombre;
            this.Valor = valor;
        }
        public string Nombre { get; set; }
        public Object Valor { get; set; } 

        public int Valor_int { get; set; }  
        
        public DateOnly Fecha { get; set;}
    }
}
