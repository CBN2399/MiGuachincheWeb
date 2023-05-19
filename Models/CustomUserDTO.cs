namespace MiGuachincheWeb.Models
{
    public class CustomUserDTO
    {
        public string? Id { get; set; }
        public String? Nombre { get; set; }

        public String? Apellidos { get; set; }

        public String? Telefono { get; set; }

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
