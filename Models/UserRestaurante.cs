﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace MiGuachincheWeb.Models
{
    public partial class UserRestaurante
    {
        public int id { get; set; }
        public string usuario_Id { get; set; }
        public int restaurante_Id { get; set; }

        public virtual Restaurante restaurante { get; set; }

        public virtual CustomUser customUser { get; set; }

    }
}