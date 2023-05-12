namespace MiGuachincheWeb.Models
{
    public class ManagerDTO
    {
        public int? restauranteId {get; set;}
        public int? platoId { get; set;}
        public string? managerId { get; set;}

        public List<Restaurante>? restaurantes { get; set;}


        public ManagerDTO() 
        {
        }

        public ManagerDTO(int restauranteId, int platoId, string managerId,List<Restaurante> rest)
        {
            this.restauranteId = restauranteId;
            this.platoId = platoId;
            this.managerId = managerId;
            this. restaurantes = rest.ToList();
        }
    }
}
