﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace MiGuachincheWeb.Models
{
    public partial class plato
    {
        public plato()
        {
            plato_restaurantes = new HashSet<plato_restaurante>();
        }

        public int PlatoId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int tipoId { get; set; }

        public virtual tipo tipo { get; set; }
        public virtual ICollection<plato_restaurante> plato_restaurantes { get; set; }
    }
}