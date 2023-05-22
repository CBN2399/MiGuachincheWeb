using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MiGuachincheWeb.Models
{
    public class CustomUserDTO
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "El Campo 'Nombre' es requerido")]
        public String? Nombre { get; set; }

        [Required(ErrorMessage = "El Campo 'Apellidos' es requerido")]
        public String? Apellidos { get; set; }

        [DisplayName("Teléfono")]
        [Required(ErrorMessage = "El Campo 'Teléfono' es requerido")]
        public String? Telefono { get; set; }

        [DisplayName("Correo Electrónico")]
        [Required(ErrorMessage = "El Campo 'Correo Electrónico' es requerido")]
        public String? Email { get; set; }

        public String? Role { get; set; }

        public CustomUserDTO() { }


        public CustomUserDTO(string? id, string? nombre, string? apelllidos, string? telefono, string? email, string role)
        {
            Id = id;
            Nombre = nombre;
            Apellidos = apelllidos;
            Telefono = telefono;
            Email = email;
            Role = role;
        }

        public CustomUserDTO(string? id, string? nombre, string? apelllidos, string? telefono, string? email)
        {
            Id = id;
            Nombre = nombre;
            Apellidos = apelllidos;
            Telefono = telefono;
            Email = email;
        }
    }
}
