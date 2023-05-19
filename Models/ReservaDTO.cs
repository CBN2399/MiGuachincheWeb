namespace MiGuachincheWeb.Models
{
    public class ReservaDTO
    {
        public int numeroComensales { get; set; }
        public DateTime fechaReserva { get; set; }

        public string? userId { get; set; }

        public int restId { get; set; }
        public string nombreUsuario { get; set; }



        public string nombreRestaurante { get; set; }

        public ReservaDTO() { }

        public ReservaDTO(int numeroComensales, DateTime fechaReserva, string nombreUsuario, string? nombreRestaurante, string userId, int restId)
        {
            this.numeroComensales = numeroComensales;
            this.fechaReserva = fechaReserva;
            this.nombreUsuario = nombreUsuario;
            this.nombreRestaurante = nombreRestaurante;
            this.userId = userId;
            this.restId = restId;
        }

        public ReservaDTO(string userId, int restId, string nombreUsuario, string nombreRestaurante)
        {
            this.nombreUsuario = nombreUsuario;
            this.nombreRestaurante = nombreRestaurante;
            this.userId = userId;
            this.restId = restId;
        }
    }
}
