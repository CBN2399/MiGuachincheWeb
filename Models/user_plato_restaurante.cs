﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace MiGuachincheWeb.Models
{
    public partial class user_plato_restaurante
    {
        public int id { get; set; }
        public string usuario_Id { get; set; }
        public int plato_restaurante_Id { get; set; }

        public virtual plato_restaurante plato_restaurante { get; set; }
    }
}