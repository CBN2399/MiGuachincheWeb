namespace MiGuachincheWeb.Models
{
    public class PlatoDTO
    {
        public int? restauranteId {get; set;}
        public int? platoId { get; set;}
        public string? managerId { get; set;}

        public List<Restaurante> restaurantes { get; set;}


        public PlatoDTO() 
        {
            restaurantes = new List<Restaurante>();
        }

        public PlatoDTO(int restauranteId, int platoId, string managerId)
        {
            this.restauranteId = restauranteId;
            this.platoId = platoId;
            this.managerId = managerId;
        }
    }
}
