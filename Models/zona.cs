﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace MiGuachincheWeb.Models
{
    public partial class Zona
    {
        public Zona()
        {
            restaurantes = new HashSet<Restaurante>();
        }

        public int Zona_id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }

        public string ImagenURL { get; set; }

        public virtual ICollection<Restaurante> restaurantes { get; set; }
    }
}