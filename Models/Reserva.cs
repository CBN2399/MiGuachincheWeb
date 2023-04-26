using System.ComponentModel.DataAnnotations.Schema;

namespace MiGuachincheWeb.Models
{
    [Table("Reservas")]
    public partial class Reserva
    {
        public int Id { get; set; }

        public DateTime FechaReserva { get; set; }

        public CustomUser CustomUser { get; set; }

        public restaurante restaurante { get; set; }

        public EstadoReserva estado { get; set; }
    }
}
