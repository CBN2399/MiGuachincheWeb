using System.ComponentModel.DataAnnotations;

namespace MiGuachincheWeb.Models
{
    public partial class EstadoReserva
    {
        [Key]
        public int Id { get; set; }


        public string? Name { get; set; }

        public virtual ICollection<Reserva> listaReservas { get; set; }
    }
}
