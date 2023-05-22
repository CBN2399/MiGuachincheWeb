namespace MiGuachincheWeb.Models
{
    public class ValoracionRestaurante
    {
        public int id {get;set;}

        public int valoracion { get;set;}

        public int restauranteId { get;set;}

        public Restaurante restaurante { get;set;}
    }
}
