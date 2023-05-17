using System.ComponentModel.DataAnnotations.Schema;

namespace MiGuachincheWeb.Models
{
    [Table("Reservas")]
    public partial class Reserva
    {
        public int Id { get; set; }
        public DateTime FechaReserva { get; set; }

        public int numeroComensales { get; set; }
        public string? customerUserId { get; set; }
        public int restauranteId { get; set; }
        public int estadoReservaId { get; set; }

        public virtual CustomUser CustomUser { get; set; }
        public virtual Restaurante restaurante { get; set; }
        public virtual EstadoReserva estado { get; set; }
    }
}
