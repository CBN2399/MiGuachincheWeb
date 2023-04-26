using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiGuachincheWeb.Models
{
    [Table("CustomUser")]
    public partial class CustomUser : IdentityUser
    {
        public String Nombre { get; set; }

        public String Apelllidos { get; set; }

        public String Telefono { get; set; }

        public bool? isActive { get; set; }

        public ICollection<restaurante>? restaurantesFavoritos { get; set; }

        public ICollection<plato_restaurante>? platosFavoritos { get; set; }

        public ICollection<Reserva>? reservas { get; set; }



    }
}
