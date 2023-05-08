using System.ComponentModel.DataAnnotations.Schema;

namespace MiGuachincheWeb.Models
{
    [Table("Reservas")]
    public partial class Reserva
    {
        public int Id { get; set; }
        public DateTime FechaReserva { get; set; }
        public string customerUserId { get; set; }
        public int restauranteId { get; set; }
        public int estadoReservaId { get; set; }

        public CustomUser? CustomUser { get; set; }
        public Restaurante? restaurante { get; set; }

        

        public EstadoReserva? estado { get; set; }
    }
}
