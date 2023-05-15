namespace MiGuachincheWeb.Models
{
    public class ReservaDTO
    {
        public int? numeroComensales { get; set; }
        public DateTime? fechaReserva { get; set; }

        public string nombreUsuario { get; set; }

        public string nombreRestaurante { get; set; }

        public ReservaDTO() { }

        public ReservaDTO(int numeroComensales, DateTime fechaReserva, string nombreUsuario, string? nombreRestaurante)
        {
            this.numeroComensales = numeroComensales;
            this.fechaReserva = fechaReserva;
            this.nombreUsuario = nombreUsuario;
            this.nombreRestaurante = nombreRestaurante;
        }

        public ReservaDTO( string nombreUsuario, string? nombreRestaurante)
        {
            this.nombreUsuario = nombreUsuario;
            this.nombreRestaurante = nombreRestaurante;
        }
    }
}
