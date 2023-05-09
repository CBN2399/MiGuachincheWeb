using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiGuachincheWeb.Models
{
    [Table("CustomUser")]
    public partial class CustomUser : IdentityUser
    {
        public String? Nombre { get; set; }

        public String? Apelllidos { get; set; }

        public String? Telefono { get; set; }

        public bool? isActive { get; set; }

        public virtual ICollection<Restaurante>? restaurantes { get; set; } 

        public virtual ICollection<PlatoRestaurante>? platos { get; set; }

        public virtual ICollection<Reserva>? reservas { get; set; } 

        

    }
}
