﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MiGuachincheWeb.Models
{
    public partial class Plato
    {
        public int PlatoId { get; set; }
        public string Nombre { get; set; }

        [DisplayName("Descripción")]
        public string Descripcion { get; set; }
        public int tipoId { get; set; }

        [AllowNull]
        public string ImagenURL { get; set; }
        public virtual Tipo tipo { get; set; }
        public virtual ICollection<Restaurante> restaurantes { get; set; }

    }
}