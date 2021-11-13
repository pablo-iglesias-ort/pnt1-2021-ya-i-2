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
					Email = "paciente@gmail.com",
					Direccion = "CABA",
					Password = seguridad.EncriptarPass("paciente"),
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
					Duracion = new DateTime(1, 1, 1, 01, 00, 0),
					Precio = 1000,

				};

				var usuarioPrestacion2 = new Prestacion
				{
					PrestacionId = Guid.NewGuid(),
					Nombre = "Traumatologia",
					Descripcion = "Traumatologia",
					Duracion = new DateTime(1, 1, 1, 01, 00, 0),
					Precio = 1000,

				};

				var usuarioPrestacion3 = new Prestacion
				{
					PrestacionId = Guid.NewGuid(),
					Nombre = "Ginecologia",
					Descripcion = "Ginecologia",
					Duracion = new DateTime(1, 1, 1, 01, 00, 0),
					Precio = 1000,

				};
				context.Prestacion.Add(usuarioPrestacion);
				context.Prestacion.Add(usuarioPrestacion2);
				context.Prestacion.Add(usuarioPrestacion3);

				var usuarioProfesional = new Profesional
				{
					Id = Guid.NewGuid(),
					Nombre = "Luciano",
					Apellido = "Sanchez",
					Dni = "40123456",
					Password = seguridad.EncriptarPass("profesional"),
					FechaAlta = DateTime.Now,
					Direccion = "caba",
					Email = "profesional@gmail.com",
					HoraInicio = new DateTime(1, 1, 1, 10, 00, 0),
					HoraFin = new DateTime(1, 1, 1, 18, 00, 0),
					Matricula = "123721",
					Prestacion = usuarioPrestacion,
					PrestacionId = usuarioPrestacion.PrestacionId,
					Telefono = "1123123123",

				};

				var usuarioProfesional2 = new Profesional
				{
					Id = Guid.NewGuid(),
					Nombre = "Santiago",
					Apellido = "Martinez",
					Dni = "39123456",
					Password = seguridad.EncriptarPass("profesional"),
					FechaAlta = DateTime.Now,
					Direccion = "caba",
					Email = "profesional2@gmail.com",
					HoraInicio = new DateTime(1, 1, 1, 10, 00, 0),
					HoraFin = new DateTime(1, 1, 1, 18, 00, 0),
					Matricula = "164221",
					Prestacion = usuarioPrestacion2,
					PrestacionId = usuarioPrestacion2.PrestacionId,
					Telefono = "1123123123",

				};

				var usuarioProfesional3 = new Profesional
				{
					Id = Guid.NewGuid(),
					Nombre = "Roberto",
					Apellido = "Rodriguez",
					Dni = "38123456",
					Password = seguridad.EncriptarPass("profesional"),
					FechaAlta = DateTime.Now,
					Direccion = "caba",
					Email = "profesional3@gmail.com",
					HoraInicio = new DateTime(1, 1, 1, 10, 00, 0),
					HoraFin = new DateTime(1, 1, 1, 18, 00, 0),
					Matricula = "128531",
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
					Activo = false,
					Confirmado = true,
					Atendido = false,
					Fecha = new DateTime(2021, 11, 15, 12, 00, 0),
					FechaAlta = DateTime.Now,
					Paciente = usuarioPaciente,
					PacienteId = usuarioPaciente.Id,
					Profesional = usuarioProfesional,
					ProfesionalId = usuarioProfesional.Id,

				};
				context.Turno.AddRange(turno);

				var turno2 = new Turno
				{
					Id = Guid.NewGuid(),
					Activo = false,
					Confirmado = true,
					Atendido = false,
					Fecha = new DateTime(2021, 11, 15, 14, 00, 0),
					FechaAlta = DateTime.Now,
					Paciente = null,
					PacienteId = null,
					Profesional = usuarioProfesional,
					ProfesionalId = usuarioProfesional.Id,

				};
				context.Turno.AddRange(turno);

				var turno3 = new Turno
				{
					Id = Guid.NewGuid(),
					Activo = false,
					Confirmado = true,
					Atendido = false,
					Fecha = new DateTime(2021, 11, 15, 15, 00, 0),
					FechaAlta = DateTime.Now,
					Paciente = null,
					PacienteId = null,
					Profesional = usuarioProfesional,
					ProfesionalId = usuarioProfesional.Id,

				};
				context.Turno.AddRange(turno);

				var turno4 = new Turno
				{
					Id = Guid.NewGuid(),
					Activo = false,
					Confirmado = false,
					Atendido = false,
					Fecha = new DateTime(2021, 11, 15, 16, 00, 0),
					FechaAlta = DateTime.Now,
					Paciente = null,
					PacienteId = null,
					Profesional = usuarioProfesional,
					ProfesionalId = usuarioProfesional.Id,

				};

				var turno5 = new Turno
				{
					Id = Guid.NewGuid(),
					Activo = false,
					Confirmado = true,
					Atendido = false,
					Fecha = new DateTime(2021, 11, 15, 12, 00, 0),
					FechaAlta = DateTime.Now,
					Paciente = null,
					PacienteId = null,
					Profesional = usuarioProfesional2,
					ProfesionalId = usuarioProfesional2.Id,

				};
				context.Turno.AddRange(turno);

				var turno6 = new Turno
				{
					Id = Guid.NewGuid(),
					Activo = false,
					Confirmado = true,
					Atendido = false,
					Fecha = new DateTime(2021, 11, 15, 13, 00, 0),
					FechaAlta = DateTime.Now,
					Paciente = null,
					PacienteId = null,
					Profesional = usuarioProfesional2,
					ProfesionalId = usuarioProfesional2.Id,

				};
				context.Turno.AddRange(turno);

				var turno7 = new Turno
				{
					Id = Guid.NewGuid(),
					Activo = false,
					Confirmado = false,
					Atendido = false,
					Fecha = new DateTime(2021, 11, 15, 14, 00, 0),
					FechaAlta = DateTime.Now,
					Paciente = null,
					PacienteId = null,
					Profesional = usuarioProfesional2,
					ProfesionalId = usuarioProfesional2.Id,

				};

				var turno8 = new Turno
				{
					Id = Guid.NewGuid(),
					Activo = false,
					Confirmado = true,
					Atendido = false,
					Fecha = new DateTime(2021, 11, 15, 12, 00, 0),
					FechaAlta = DateTime.Now,
					Paciente = null,
					PacienteId = null,
					Profesional = usuarioProfesional3,
					ProfesionalId = usuarioProfesional3.Id,

				};
				context.Turno.AddRange(turno);

				var turno9 = new Turno
				{
					Id = Guid.NewGuid(),
					Activo = false,
					Confirmado = true,
					Atendido = false,
					Fecha = new DateTime(2021, 11, 15, 13, 00, 0),
					FechaAlta = DateTime.Now,
					Paciente = null,
					PacienteId = null,
					Profesional = usuarioProfesional3,
					ProfesionalId = usuarioProfesional3.Id,

				};
				context.Turno.AddRange(turno);

				var turno10 = new Turno
				{
					Id = Guid.NewGuid(),
					Activo = false,
					Confirmado = false,
					Atendido = false,
					Fecha = new DateTime(2021, 11, 15, 14, 00, 0),
					FechaAlta = DateTime.Now,
					Paciente = null,
					PacienteId = null,
					Profesional = usuarioProfesional3,
					ProfesionalId = usuarioProfesional3.Id,

				};
				context.Turno.AddRange(turno);
				context.Turno.AddRange(turno2);
				context.Turno.AddRange(turno3);
				context.Turno.AddRange(turno4);
				context.Turno.AddRange(turno5);
				context.Turno.AddRange(turno6);
				context.Turno.AddRange(turno7);
				context.Turno.AddRange(turno8);
				context.Turno.AddRange(turno9);
				context.Turno.AddRange(turno10);

				context.SaveChanges();
			}

			

			if (!context.Administrador.Any())
			{
				var usuarioAdministrador = new Administrador
				{
					Id = Guid.NewGuid(),
					Nombre = "Jorge el admin",
					Apellido = "Patricio",
					Dni = "35423754",
					Password = seguridad.EncriptarPass("administrador"),
					Direccion = "caba",
					Email = "administrador@gmail.com",
					FechaAlta = DateTime.Now,
					Telefono = "1231231231"
				};
				context.Administrador.Add(usuarioAdministrador);
				context.SaveChanges();
			}
		}
	}
}
