﻿// <auto-generated />
using System;
using MiGuachincheWeb.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MiGuachincheWeb.Migrations
{
    [DbContext(typeof(guachincheContext))]
    [Migration("20230510231754_AddCamposToEntities")]
    partial class AddCamposToEntities
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "555753b2-84a3-4a93-a41d-9864516b97cd",
                            ConcurrencyStamp = "fb9625e5-94e5-4ee5-ac33-b1fa48a8a310",
                            Name = "Default",
                            NormalizedName = "DEFAULT"
                        },
                        new
                        {
                            Id = "9f58e5ee-1397-460c-975a-b8dda54503e1",
                            ConcurrencyStamp = "db59f895-b6e2-4a2d-86cb-87c940a4dba0",
                            Name = "Manager",
                            NormalizedName = "MANAGER"
                        },
                        new
                        {
                            Id = "f162e4de-4550-4d27-87bb-356d4f65d58d",
                            ConcurrencyStamp = "8a8f1db8-09bd-44e1-8402-4fc81d621fec",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "69d0b763-34a3-4bd5-ae5a-06e125932b2f",
                            RoleId = "f162e4de-4550-4d27-87bb-356d4f65d58d"
                        },
                        new
                        {
                            UserId = "60a8863f-af7b-40be-bd46-c0847a2ae8b4",
                            RoleId = "9f58e5ee-1397-460c-975a-b8dda54503e1"
                        },
                        new
                        {
                            UserId = "8a4e9a90-228d-41d0-9953-cc39fa494375",
                            RoleId = "555753b2-84a3-4a93-a41d-9864516b97cd"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("MiGuachincheWeb.Models.CustomUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Apelllidos")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Nombre")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool?>("isActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "69d0b763-34a3-4bd5-ae5a-06e125932b2f",
                            AccessFailedCount = 0,
                            Apelllidos = "Bartolome Navarro",
                            ConcurrencyStamp = "d77d9e5d-5578-41f0-bff6-e58cd6be4e78",
                            Email = "Admin@guachinche.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            Nombre = "Cesar",
                            NormalizedEmail = "ADMIN@GUACHINCHE.COM",
                            NormalizedUserName = "ADMIN@GUACHINCHE.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEOmMYsjpa90/RSnxaW0DIujEX1MKNMIbAr9xerJS8t8HqmZv3kF1d7ahF/Rysq4ycA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "f10573d8-222d-41e6-9764-08227020f991",
                            Telefono = "922111333",
                            TwoFactorEnabled = false,
                            UserName = "Admin@guachinche.com",
                            isActive = true
                        },
                        new
                        {
                            Id = "60a8863f-af7b-40be-bd46-c0847a2ae8b4",
                            AccessFailedCount = 0,
                            Apelllidos = "Guachinche",
                            ConcurrencyStamp = "dfd2b1ab-74a9-4a2a-9318-58c4501b4b83",
                            Email = "Manager@guachinche.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            Nombre = "Manager",
                            NormalizedEmail = "MANAGER@GUACHINCHE.COM",
                            NormalizedUserName = "MANAGER@GUACHINCHE.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEMgQR8e8SbNXvhKMbjizX2Jl4wkLswryFrG0gCVzt28hLT1v3vrCQCYtc1OTXq2N7g==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "0e0b49fb-cad7-4333-82c4-8c63497d1a0b",
                            Telefono = "922000111",
                            TwoFactorEnabled = false,
                            UserName = "Manager@guachinche.com",
                            isActive = true
                        },
                        new
                        {
                            Id = "8a4e9a90-228d-41d0-9953-cc39fa494375",
                            AccessFailedCount = 0,
                            Apelllidos = "Guachinche",
                            ConcurrencyStamp = "f357f015-de86-4ab9-b168-7294039bb821",
                            Email = "User@guachinche.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            Nombre = "User",
                            NormalizedEmail = "USER@GUACHINCHE.COM",
                            NormalizedUserName = "USER@GUACHINCHE.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEN1/a+fF1tmKeoeGt+CRgHPJS+Rgt3kKPRtZTI42sR+ZFtqaSI/yydU/Tf2b+xyj/g==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "82eeec92-eb5c-46e5-b1b0-aa45ba4e8c4d",
                            Telefono = "922456789",
                            TwoFactorEnabled = false,
                            UserName = "User@guachinche.com",
                            isActive = true
                        });
                });

            modelBuilder.Entity("MiGuachincheWeb.Models.EstadoReserva", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("estadoReservas");

                    b.HasData(
                        new
                        {
                            Id = -1,
                            Name = "Activa"
                        },
                        new
                        {
                            Id = -2,
                            Name = "Pendiente"
                        },
                        new
                        {
                            Id = -3,
                            Name = "Cancelada"
                        },
                        new
                        {
                            Id = -4,
                            Name = "Rechazada"
                        },
                        new
                        {
                            Id = -5,
                            Name = "Finalizada"
                        });
                });

            modelBuilder.Entity("MiGuachincheWeb.Models.Plato", b =>
                {
                    b.Property<int>("PlatoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlatoId"), 1L, 1);

                    b.Property<string>("Descripcion")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("ImagenURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("tipoId")
                        .HasColumnType("int");

                    b.HasKey("PlatoId");

                    b.HasIndex(new[] { "tipoId" }, "ifk_PlatoTipo");

                    b.ToTable("plato", (string)null);
                });

            modelBuilder.Entity("MiGuachincheWeb.Models.PlatoRestaurante", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int?>("PlatoId")
                        .HasColumnType("int");

                    b.Property<int?>("RestauranteId")
                        .HasColumnType("int");

                    b.Property<bool>("activo")
                        .HasColumnType("bit");

                    b.Property<int>("plato_Id")
                        .HasColumnType("int");

                    b.Property<int>("restaurante_Id")
                        .HasColumnType("int");

                    b.Property<int?>("valoracion")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("PlatoId");

                    b.HasIndex("RestauranteId");

                    b.ToTable("plato_restaurantes");
                });

            modelBuilder.Entity("MiGuachincheWeb.Models.Reserva", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("FechaReserva")
                        .HasColumnType("datetime2");

                    b.Property<string>("customerUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("estadoReservaId")
                        .HasColumnType("int");

                    b.Property<int>("numeroComensales")
                        .HasColumnType("int");

                    b.Property<int>("restauranteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("customerUserId");

                    b.HasIndex("estadoReservaId");

                    b.HasIndex("restauranteId");

                    b.ToTable("Reservas");
                });

            modelBuilder.Entity("MiGuachincheWeb.Models.Restaurante", b =>
                {
                    b.Property<int>("RestauranteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RestauranteId"), 1L, 1);

                    b.Property<int>("Id_tipo")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Rest_Url")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("telefono")
                        .HasMaxLength(9)
                        .IsUnicode(false)
                        .HasColumnType("char(9)")
                        .IsFixedLength();

                    b.Property<int?>("valoracion")
                        .HasColumnType("int");

                    b.Property<int>("zonaId")
                        .HasColumnType("int");

                    b.HasKey("RestauranteId");

                    b.HasIndex(new[] { "Id_tipo" }, "ifk_restTipoRest");

                    b.HasIndex(new[] { "zonaId" }, "ifk_zonaRest");

                    b.ToTable("restaurante", (string)null);
                });

            modelBuilder.Entity("MiGuachincheWeb.Models.Tipo", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)");

                    b.HasKey("id");

                    b.ToTable("tipo", (string)null);
                });

            modelBuilder.Entity("MiGuachincheWeb.Models.TipoRestaurante", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)");

                    b.HasKey("id");

                    b.ToTable("tipoRestaurante", (string)null);
                });

            modelBuilder.Entity("MiGuachincheWeb.Models.UserPlatoRestaurante", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("custom_userId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("plato_restaurante_Id")
                        .HasColumnType("int");

                    b.Property<int?>("plato_restauranteid")
                        .HasColumnType("int");

                    b.Property<string>("usuario_Id")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("custom_userId");

                    b.HasIndex("plato_restauranteid");

                    b.ToTable("userPlatoRestaurante");
                });

            modelBuilder.Entity("MiGuachincheWeb.Models.UserRestaurante", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int?>("RestauranteId")
                        .HasColumnType("int");

                    b.Property<string>("customUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("restaurante_Id")
                        .HasColumnType("int");

                    b.Property<string>("usuario_Id")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("RestauranteId");

                    b.HasIndex("customUserId");

                    b.ToTable("userRestaurantes");
                });

            modelBuilder.Entity("MiGuachincheWeb.Models.Zona", b =>
                {
                    b.Property<int>("Zona_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Zona_id"), 1L, 1);

                    b.Property<string>("ImagenURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("descripcion")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Zona_id")
                        .HasName("PK__zona__CD7EA2967C6981B9");

                    b.ToTable("zona", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("MiGuachincheWeb.Models.CustomUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("MiGuachincheWeb.Models.CustomUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MiGuachincheWeb.Models.CustomUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("MiGuachincheWeb.Models.CustomUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MiGuachincheWeb.Models.Plato", b =>
                {
                    b.HasOne("MiGuachincheWeb.Models.Tipo", "tipo")
                        .WithMany("platos")
                        .HasForeignKey("tipoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_tipo");

                    b.Navigation("tipo");
                });

            modelBuilder.Entity("MiGuachincheWeb.Models.PlatoRestaurante", b =>
                {
                    b.HasOne("MiGuachincheWeb.Models.Plato", "plato")
                        .WithMany()
                        .HasForeignKey("PlatoId");

                    b.HasOne("MiGuachincheWeb.Models.Restaurante", "restaurante")
                        .WithMany()
                        .HasForeignKey("RestauranteId");

                    b.Navigation("plato");

                    b.Navigation("restaurante");
                });

            modelBuilder.Entity("MiGuachincheWeb.Models.Reserva", b =>
                {
                    b.HasOne("MiGuachincheWeb.Models.CustomUser", "CustomUser")
                        .WithMany("reservas")
                        .HasForeignKey("customerUserId")
                        .HasConstraintName("fk_user_reservas");

                    b.HasOne("MiGuachincheWeb.Models.EstadoReserva", "estado")
                        .WithMany("listaReservas")
                        .HasForeignKey("estadoReservaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_estado_reserva");

                    b.HasOne("MiGuachincheWeb.Models.Restaurante", "restaurante")
                        .WithMany("reservas")
                        .HasForeignKey("restauranteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_reserva_restaurante");

                    b.Navigation("CustomUser");

                    b.Navigation("estado");

                    b.Navigation("restaurante");
                });

            modelBuilder.Entity("MiGuachincheWeb.Models.Restaurante", b =>
                {
                    b.HasOne("MiGuachincheWeb.Models.TipoRestaurante", "Id_tipoNavigation")
                        .WithMany("restaurantes")
                        .HasForeignKey("Id_tipo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_tipoRest");

                    b.HasOne("MiGuachincheWeb.Models.Zona", "zona")
                        .WithMany("restaurantes")
                        .HasForeignKey("zonaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_zona");

                    b.Navigation("Id_tipoNavigation");

                    b.Navigation("zona");
                });

            modelBuilder.Entity("MiGuachincheWeb.Models.UserPlatoRestaurante", b =>
                {
                    b.HasOne("MiGuachincheWeb.Models.CustomUser", "custom_user")
                        .WithMany()
                        .HasForeignKey("custom_userId");

                    b.HasOne("MiGuachincheWeb.Models.PlatoRestaurante", "plato_restaurante")
                        .WithMany()
                        .HasForeignKey("plato_restauranteid");

                    b.Navigation("custom_user");

                    b.Navigation("plato_restaurante");
                });

            modelBuilder.Entity("MiGuachincheWeb.Models.UserRestaurante", b =>
                {
                    b.HasOne("MiGuachincheWeb.Models.Restaurante", "restaurante")
                        .WithMany()
                        .HasForeignKey("RestauranteId");

                    b.HasOne("MiGuachincheWeb.Models.CustomUser", "customUser")
                        .WithMany()
                        .HasForeignKey("customUserId");

                    b.Navigation("customUser");

                    b.Navigation("restaurante");
                });

            modelBuilder.Entity("MiGuachincheWeb.Models.CustomUser", b =>
                {
                    b.Navigation("reservas");
                });

            modelBuilder.Entity("MiGuachincheWeb.Models.EstadoReserva", b =>
                {
                    b.Navigation("listaReservas");
                });

            modelBuilder.Entity("MiGuachincheWeb.Models.Restaurante", b =>
                {
                    b.Navigation("reservas");
                });

            modelBuilder.Entity("MiGuachincheWeb.Models.Tipo", b =>
                {
                    b.Navigation("platos");
                });

            modelBuilder.Entity("MiGuachincheWeb.Models.TipoRestaurante", b =>
                {
                    b.Navigation("restaurantes");
                });

            modelBuilder.Entity("MiGuachincheWeb.Models.Zona", b =>
                {
                    b.Navigation("restaurantes");
                });
#pragma warning restore 612, 618
        }
    }
}
