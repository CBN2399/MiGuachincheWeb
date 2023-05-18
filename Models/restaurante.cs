﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MiGuachincheWeb.Models
{
    public partial class Restaurante
    {
        public int RestauranteId { get; set; }

        
        public string Nombre { get; set; }
        public string Rest_Url { get; set; }

        [Required(ErrorMessage = "El campo 'Telefono' es requerido")]
        [StringLength(9)]
        [DisplayName("Telefono")]
        public string telefono { get; set; }

        [Required(ErrorMessage = "El campo 'valoracion' es requerido")]
        public int? valoracion { get; set; }
        public int Id_tipo { get; set; }
        public int zonaId { get; set; }

        [Required(ErrorMessage = "El campo 'Descripcion' es requerido")]
        public string Descripcion { get; set; }

        [DisplayName("Especialidad")]
        public virtual TipoRestaurante Id_tipoNavigation { get; set; }
        public virtual Zona zona { get; set; }
        public virtual ICollection<Plato> platos { get; set; }
        public virtual ICollection<CustomUser> usuarios { get; set; }

        public virtual ICollection<Reserva> reservas { get; set; }

    }
}