﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MiGuachincheWeb.Models;

namespace MiGuachincheWeb.Data
{
    public partial class guachincheContext : IdentityDbContext<CustomUser>
    {
        public guachincheContext()
        {
        }

        public guachincheContext(DbContextOptions<guachincheContext> options)
            : base(options)
        {
        }

        public virtual DbSet<plato> platos { get; set; }
        public virtual DbSet<plato_restaurante> plato_restaurantes { get; set; }
        public virtual DbSet<restaurante> restaurantes { get; set; }
        public virtual DbSet<tipo> tipos { get; set; }
        public virtual DbSet<tipoRestaurante> tipoRestaurantes { get; set; }
        public virtual DbSet<zona> zonas { get; set; }
        public virtual DbSet<CustomUser> custom_users { get; set; }
        public virtual DbSet<Reserva> reservas { get; set; }
        public virtual DbSet<EstadoReserva> estadoReservas { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<plato>(entity =>
            {
                entity.ToTable("plato");

                entity.HasIndex(e => e.tipoId, "ifk_PlatoTipo");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.tipo)
                    .WithMany(p => p.platos)
                    .HasForeignKey(d => d.tipoId)
                    .HasConstraintName("fk_tipo");
            });

            modelBuilder.Entity<plato_restaurante>(entity =>
            {
                entity.ToTable("plato_restaurante");

                entity.HasOne(d => d.plato)
                    .WithMany(p => p.plato_restaurantes)
                    .HasForeignKey(d => d.plato_Id)
                    .HasConstraintName("fk_plato");

                entity.HasOne(d => d.restaurante)
                    .WithMany(p => p.plato_restaurantes)
                    .HasForeignKey(d => d.restaurante_Id)
                    .HasConstraintName("fk_restaurante");
            });

            modelBuilder.Entity<restaurante>(entity =>
            {
                entity.ToTable("restaurante");

                entity.HasIndex(e => e.Id_tipo, "ifk_restTipoRest");

                entity.HasIndex(e => e.zonaId, "ifk_zonaRest");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Rest_Url)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.telefono)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.Id_tipoNavigation)
                    .WithMany(p => p.restaurantes)
                    .HasForeignKey(d => d.Id_tipo)
                    .HasConstraintName("fk_tipoRest");

                entity.HasOne(d => d.zona)
                    .WithMany(p => p.restaurantes)
                    .HasForeignKey(d => d.zonaId)
                    .HasConstraintName("fk_zona");
            });

            modelBuilder.Entity<tipo>(entity =>
            {
                entity.ToTable("tipo");

                entity.Property(e => e.nombre)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<tipoRestaurante>(entity =>
            {
                entity.ToTable("tipoRestaurante");

                entity.Property(e => e.nombre)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<zona>(entity =>
            {
                entity.HasKey(e => e.Zona_id)
                    .HasName("PK__zona__CD7EA2967C6981B9");

                entity.ToTable("zona");

                entity.Property(e => e.descripcion)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            //Perzonalizar la tabla user
            modelBuilder.Entity<CustomUser>(e =>
            {
                e.ToTable(nameof(CustomUser));
                e.Property(p => p.Nombre).HasMaxLength(25);
                e.Property(p => p.Apelllidos).HasMaxLength(50);
                e.Property(p => p.isActive).HasDefaultValue(true);
                e.Property(p => p.Telefono).HasMaxLength(9);

            });

            modelBuilder.Entity<CustomUser>().Ignore(i => i.platosFavoritos);
            modelBuilder.Entity<CustomUser>().Ignore(i => i.restaurantesFavoritos);


            //Seeders

            List<IdentityRole> roles = new List<IdentityRole>();
            roles.Add(new IdentityRole { Name = "Default", NormalizedName = "DEFAULT" });
            roles.Add(new IdentityRole { Name = "Manager", NormalizedName = "MANAGER" });
            roles.Add(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" });
            modelBuilder.Entity<IdentityRole>().HasData(roles);


            List<CustomUser> users = new List<CustomUser>();

            users.Add(new CustomUser //ADMIN
            {
                Nombre = "Cesar",
                Apelllidos = "Bartolome Navarro",
                Telefono = "922111333",
                isActive = true,
                UserName = "Admin@guachinche.com",
                NormalizedUserName = "ADMIN@GUACHINCHE.COM",
                Email = "Admin@guachinche.com",
                NormalizedEmail = "ADMIN@GUACHINCHE.COM",
                EmailConfirmed = true,
            });

            users.Add(new CustomUser //MANAGER
            {
                Nombre = "Manager",
                Apelllidos = "Guachinche",
                Telefono = "922000111",
                isActive = true,
                UserName = "Manager@guachinche.com",
                NormalizedUserName = "MANAGER@GUACHINCHE.COM",
                Email = "Manager@guachinche.com",
                NormalizedEmail = "MANAGER@GUACHINCHE.COM",
                EmailConfirmed = true,
            });

            users.Add(new CustomUser //DEFAULT
            {
                Nombre = "User",
                Apelllidos = "Guachinche",
                Telefono = "922456789",
                isActive = true,
                UserName = "User@guachinche.com",
                NormalizedUserName = "USER@GUACHINCHE.COM",
                Email = "User@guachinche.com",
                NormalizedEmail = "USER@GUACHINCHE.COM",
                EmailConfirmed = true,
            });

            modelBuilder.Entity<CustomUser>().HasData(users);
            var passwordHasher = new PasswordHasher<CustomUser>();
            users[0].PasswordHash = passwordHasher.HashPassword(users[0], "guachinpass");
            users[1].PasswordHash = passwordHasher.HashPassword(users[1], "1234");
            users[2].PasswordHash = passwordHasher.HashPassword(users[2], "1234");

            List<IdentityUserRole<string>> userRoles = new List<IdentityUserRole<string>>();

            userRoles.Add(new IdentityUserRole<string>
            {
                RoleId = roles.Find(r => r.Name == "Admin").Id,
                UserId = users[0].Id
            });

            userRoles.Add(new IdentityUserRole<string>
            {
                RoleId = roles.Find(r => r.Name == "Manager").Id,
                UserId = users[1].Id
            });

            userRoles.Add(new IdentityUserRole<string>
            {
                RoleId = roles.Find(r => r.Name == "Default").Id,
                UserId = users[2].Id
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRoles);

            base.OnModelCreating(modelBuilder);

            List<EstadoReserva> estados = new List<EstadoReserva>();
            estados.Add(new EstadoReserva { Id = -1, Name= "Activa"});
            estados.Add(new EstadoReserva { Id = -2, Name = "Cancelada" });
            estados.Add(new EstadoReserva { Id = -3, Name = "Finalizada" });

            modelBuilder.Entity<EstadoReserva>().HasData(estados);
        }

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}