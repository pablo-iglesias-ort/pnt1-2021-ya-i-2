﻿// <auto-generated />
using System;
using AgendaDeTurnos.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AgendaDeTurnos.Migrations
{
    [DbContext(typeof(AgendaDeTurnosContext))]
    [Migration("20211105001434_Version_Inicial")]
    partial class Version_Inicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("AgendaDeTurnos.Models.Prestacion", b =>
                {
                    b.Property<Guid>("PrestacionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Duracion")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Precio")
                        .HasColumnType("REAL");

                    b.HasKey("PrestacionId");

                    b.ToTable("Prestacion");
                });

            modelBuilder.Entity("AgendaDeTurnos.Models.Turno", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Activo")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Confirmado")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DescripcionCancelacion")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaAlta")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PacienteId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ProfesionalId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PacienteId");

                    b.HasIndex("ProfesionalId");

                    b.ToTable("Turno");
                });

            modelBuilder.Entity("AgendaDeTurnos.Models.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Direccion")
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Dni")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaAlta")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Usuario");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Usuario");
                });

            modelBuilder.Entity("AgendaDeTurnos.Models.Administrador", b =>
                {
                    b.HasBaseType("AgendaDeTurnos.Models.Usuario");

                    b.HasDiscriminator().HasValue("Administrador");
                });

            modelBuilder.Entity("AgendaDeTurnos.Models.Paciente", b =>
                {
                    b.HasBaseType("AgendaDeTurnos.Models.Usuario");

                    b.Property<string>("ObraSocial")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasDiscriminator().HasValue("Paciente");
                });

            modelBuilder.Entity("AgendaDeTurnos.Models.Profesional", b =>
                {
                    b.HasBaseType("AgendaDeTurnos.Models.Usuario");

                    b.Property<DateTime>("HoraFin")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("HoraInicio")
                        .HasColumnType("TEXT");

                    b.Property<string>("Matricula")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PrestacionId")
                        .HasColumnType("TEXT");

                    b.HasIndex("PrestacionId");

                    b.HasDiscriminator().HasValue("Profesional");
                });

            modelBuilder.Entity("AgendaDeTurnos.Models.Turno", b =>
                {
                    b.HasOne("AgendaDeTurnos.Models.Paciente", "Paciente")
                        .WithMany()
                        .HasForeignKey("PacienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AgendaDeTurnos.Models.Profesional", "Profesional")
                        .WithMany("Turnos")
                        .HasForeignKey("ProfesionalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Paciente");

                    b.Navigation("Profesional");
                });

            modelBuilder.Entity("AgendaDeTurnos.Models.Profesional", b =>
                {
                    b.HasOne("AgendaDeTurnos.Models.Prestacion", "Prestacion")
                        .WithMany("Profesionales")
                        .HasForeignKey("PrestacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Prestacion");
                });

            modelBuilder.Entity("AgendaDeTurnos.Models.Prestacion", b =>
                {
                    b.Navigation("Profesionales");
                });

            modelBuilder.Entity("AgendaDeTurnos.Models.Profesional", b =>
                {
                    b.Navigation("Turnos");
                });
#pragma warning restore 612, 618
        }
    }
}