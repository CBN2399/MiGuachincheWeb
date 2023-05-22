using System.ComponentModel.DataAnnotations;
namespace MiGuachincheWeb.Models
{
    public class ValoracionPlato
    {
        [Key]
        public int Id { get; set; }

        public int valoracion { get; set; }

        public int platoRestauranteId { get; set; }

        public PlatoRestaurante platoRestaurante { get; set; }
    }
}
