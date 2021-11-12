using AgendaDeTurnos.Controllers;
using AgendaDeTurnos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDeTurnos.Data
{
	public static class InicializacionDeDatos
	{
		public static readonly ISeguridad seguridad = new SeguridadBasica();
		public static void Inicializar(AgendaDeTurnosContext context)
		{
			context.Database.EnsureCreated();
			var usuarioPaciente = new Paciente();

			if (!context.Paciente.Any())
			{

				 usuarioPaciente = new Paciente
				{
					Id = Guid.NewGuid(),
					Nombre = "Paciente",
					Apellido = "Paciente",
					Dni = "50123456",
					Email = "Paciente@gmail.com",
					Direccion = "CABA",
					Password = seguridad.EncriptarPass("Paciente"),
					FechaAlta = DateTime.Now,
					ObraSocial = "La Mejor",
					Telefono = "1234567890",

				};
				context.Paciente.AddRange(usuarioPaciente);
				context.SaveChanges();
			}

			if (!context.Profesional.Any())
			{
				var usuarioPrestacion = new Prestacion
				{
					PrestacionId = Guid.NewGuid(),
					Nombre = "Odontologia",
					Descripcion = "Odontologia",
					Duracion = DateTime.Now,
					Precio = 1000,

				};

				var usuarioPrestacion2 = new Prestacion
				{
					PrestacionId = Guid.NewGuid(),
					Nombre = "Traumatologia",
					Descripcion = "Traumatologia",
					Duracion = DateTime.Now,
					Precio = 1000,

				};

				var usuarioPrestacion3 = new Prestacion
				{
					PrestacionId = Guid.NewGuid(),
					Nombre = "Ginecologia",
					Descripcion = "Ginecologia",
					Duracion = DateTime.Now,
					Precio = 1000,

				};
				context.Prestacion.Add(usuarioPrestacion);
				context.Prestacion.Add(usuarioPrestacion2);
				context.Prestacion.Add(usuarioPrestacion3);

				var usuarioProfesional = new Profesional
				{
					Id = Guid.NewGuid(),
					Nombre = "Profesional",
					Apellido = "Profesional",
					Dni = "40123456",
					Password = seguridad.EncriptarPass("Profesional"),
					FechaAlta = DateTime.Now,
					Direccion = "caba",
					Email = "Profesional@gmail.com",
					HoraInicio = DateTime.Now,
					HoraFin = DateTime.Now,
					Matricula = "123721",
					Prestacion = usuarioPrestacion,
					PrestacionId = usuarioPrestacion.PrestacionId,
					Telefono = "1123123123",

				};

				var usuarioProfesional2 = new Profesional
				{
					Id = Guid.NewGuid(),
					Nombre = "Profesional2",
					Apellido = "Profesional2",
					Dni = "40123456",
					Password = seguridad.EncriptarPass("Profesional"),
					FechaAlta = DateTime.Now,
					Direccion = "caba",
					Email = "Profesional2@gmail.com",
					HoraInicio = DateTime.Now,
					HoraFin = DateTime.Now,
					Matricula = "123721",
					Prestacion = usuarioPrestacion2,
					PrestacionId = usuarioPrestacion2.PrestacionId,
					Telefono = "1123123123",

				};

				var usuarioProfesional3 = new Profesional
				{
					Id = Guid.NewGuid(),
					Nombre = "Profesional3",
					Apellido = "Profesional3",
					Dni = "40123456",
					Password = seguridad.EncriptarPass("Profesional"),
					FechaAlta = DateTime.Now,
					Direccion = "caba",
					Email = "Profesional3@gmail.com",
					HoraInicio = DateTime.Now,
					HoraFin = DateTime.Now,
					Matricula = "123721",
					Prestacion = usuarioPrestacion3,
					PrestacionId = usuarioPrestacion3.PrestacionId,
					Telefono = "1123123123",

				};
				context.Profesional.Add(usuarioProfesional);
				context.Profesional.Add(usuarioProfesional2);
				context.Profesional.Add(usuarioProfesional3);

				var turno = new Turno
				{
					Id = Guid.NewGuid(),
					Activo = true,
					Confirmado = false,
					Fecha = DateTime.Now,
					FechaAlta = DateTime.Now,
					Paciente = usuarioPaciente,
					PacienteId = usuarioPaciente.Id,
					Profesional = usuarioProfesional,
					ProfesionalId = usuarioProfesional.Id,

				};
				context.Turno.AddRange(turno);

				context.SaveChanges();
			}

			

			if (!context.Administrador.Any())
			{
				var usuarioAdministrador = new Administrador
				{
					Id = Guid.NewGuid(),
					Nombre = "Administrador",
					Apellido = "Administrador",
					Dni = "20123456",
					Password = seguridad.EncriptarPass("Administrador"),
					Direccion = "caba",
					Email = "Administrador@gmail.com",
					FechaAlta = DateTime.Now,
					Telefono = "1231231231"
				};
				context.Administrador.Add(usuarioAdministrador);
				context.SaveChanges();
			}
		}
	}
}
