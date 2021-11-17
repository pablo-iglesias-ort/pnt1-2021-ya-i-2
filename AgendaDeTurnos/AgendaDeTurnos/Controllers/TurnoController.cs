using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgendaDeTurnos.Data;
using AgendaDeTurnos.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace AgendaDeTurnos.Controllers
{
    [Authorize]
    public class TurnoController : Controller
    {
        private readonly AgendaDeTurnosContext _context;

        public TurnoController(AgendaDeTurnosContext context)
        {
            _context = context;
        }

        // GET: Turno
        public async Task<IActionResult> Index()
        {
            IQueryable<Turno> agendaDeTurnosContext;
            if (User.FindFirst(ClaimTypes.Role).Value == Rol.Administrador.ToString())
            {
                agendaDeTurnosContext = _context.Turno.Include(t => t.Paciente).Include(t => t.Profesional);
                return View(await agendaDeTurnosContext.ToListAsync());
            }
            else
            {
                var userId = Guid.Parse(User.FindFirst("UserId").Value);
                agendaDeTurnosContext = _context.Turno.Include(t => t.Paciente).Include(t => t.Profesional).Where<Turno>(t => t.PacienteId == userId || t.ProfesionalId == userId);
                return View(await agendaDeTurnosContext.ToListAsync());
            }
        }

        // GET: Turno/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turno = await _context.Turno
                .Include(t => t.Paciente)
                .Include(t => t.Profesional)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (turno == null)
            {
                return NotFound();
            }

            return View(turno);
        }

        // GET: Turno/Create
        [Authorize(Roles = nameof(Rol.Administrador))]
        public IActionResult Create()
        {

            ViewData["ListaProfesional"] = new SelectList(_context.Profesional, "Id", "NombreYApellido");
            return View();
        }
        public IActionResult SinTurno()
        {
            return View();
        }


        private bool ValidarHorarioTurnos(Turno turno) 
        {
            var ListaTurnos = _context.Turno
                    .Include(t => t.Profesional)
                    .Include(t => t.Profesional.Prestacion)
                    .Where<Turno>(t => t.ProfesionalId == turno.ProfesionalId);
            foreach (var t in ListaTurnos)
            {
                var Iniciohora = new DateTime(t.Fecha.Year, t.Fecha.Month, t.Fecha.Day, t.Fecha.Hour, t.Fecha.Minute, 0);

                var Finhora = new DateTime(t.Fecha.Year, t.Fecha.Month, t.Fecha.Day, t.Fecha.Hour + t.Profesional.Prestacion.Duracion.Hour, t.Fecha.Minute, 0);

            
                if ((t.Fecha.Year == turno.Fecha.Year && t.Fecha.Month == turno.Fecha.Month && t.Fecha.Day == turno.Fecha.Day))
                {
                    if (((turno.Fecha.Hour >= Iniciohora.Hour && turno.Fecha.Hour < Finhora.Hour) || ((t.Fecha.Hour + t.Profesional.Prestacion.Duracion.Hour) > Iniciohora.Hour && (turno.Fecha.Hour + t.Fecha.Hour + t.Profesional.Prestacion.Duracion.Hour) <= Finhora.Hour))) return false;

                }
            }

            return true;
        }


        // POST: Turno/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(Rol.Administrador))]
        public async Task<IActionResult> Create([Bind("Id,Fecha,Confirmado,Activo,FechaAlta,DescripcionCancelacion,PacienteId,ProfesionalId")] Turno turno)
        {
            //hacer la validacion del horario
            var profesional = _context.Profesional.FirstOrDefault(p => p.Id == turno.ProfesionalId);

            var horaSolicitada = new DateTime(1, 1, 1, turno.Fecha.Hour, turno.Fecha.Minute, 0);
            var horaInicio = new DateTime(1, 1, 1, profesional.HoraInicio.Hour, profesional.HoraInicio.Minute, 0);
            var horaFin = new DateTime(1, 1, 1, profesional.HoraFin.Hour, profesional.HoraFin.Minute, 0);

            if (horaSolicitada >= horaInicio && horaSolicitada < horaFin)
            {
                if (ValidarHorarioTurnos(turno))
                {
                    turno.FechaAlta = DateTime.Now;
                    if (ModelState.IsValid && !TurnoExists(new Guid(), turno))
                    {
                        turno.Id = Guid.NewGuid();
                        _context.Add(turno);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    ViewData["PacienteId"] = new SelectList(_context.Paciente, "Id", "Apellido", turno.PacienteId);
                    ViewData["ProfesionalId"] = new SelectList(_context.Profesional, "Id", "Apellido", turno.ProfesionalId);

                }
                else 
                {
                    ModelState.AddModelError("Fecha", "El horario ya esta tomado.");

                }

            }
            else
            {
                ModelState.AddModelError("Fecha", "La hora debe ser entre " + profesional.HoraInicio.ToString("HH:mm") + " y " + 
                    profesional.HoraFin.ToString("HH:mm") + ".");
            }

            return View(turno);
        }

        public IActionResult SelecionPrestacion()
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);

            var TurnoPaciente = _context.Turno.FirstOrDefault(t => t.PacienteId == userId && t.Activo == true);

            if (TurnoPaciente != null) {
                //ModelState.AddModelError("","Ya tiene un tuno activo para " + TurnoPaciente.Profesional.Prestacion +" a las "+ TurnoPaciente.Fecha);
                return RedirectToAction(nameof(SinTurno));
                
            } 

            ViewData["ListPrestaciones"] = new SelectList(_context.Prestacion, "PrestacionId", "Descripcion");
            return View();
        }

        public IActionResult SelecionProfesional(Prestacion Prestacion)
        {
            var profes = _context.Profesional;
            IQueryable<Profesional> profe;

            if (User.FindFirst(ClaimTypes.Role).Value == Rol.Administrador.ToString())
            {
                ViewData["ListProfesional"] = new SelectList(profes, "Id", "Nombre");
            }
            else
            {
                profe = profes.Where(profe => profe.PrestacionId == Prestacion.PrestacionId);
                ViewData["ListProfesional"] = new SelectList(profe, "Id", "Nombre");

            }

            return View();
        }



        public IActionResult ListaTurnosDisponibles(Profesional profesional)
        {
            //1 crear los turnos de 7 dias en adelante de la fecha actual en memoria  y tomar en cuenta el horario del profecional
            //2 Eliminar los turnos ya tomado 
            var turnos = _context.Turno
                        .Include(t => t.Profesional)
                        .Where(t => t.ProfesionalId == profesional.Id && t.Confirmado && t.Fecha <= DateTime.Today.AddDays(7) && t.PacienteId == null)
                        .ToList();


            return View(turnos);
        }

       


        private bool TurnoExists(Guid id, Turno turno = null)
        {
            if (turno != null) return _context.Turno.Any(e => e.Fecha == turno.Fecha && e.ProfesionalId == turno.ProfesionalId && e.PacienteId == turno.PacienteId && turno.Activo == true);
            return _context.Turno.Any(e => e.Id == id);
        }

        [Authorize(Roles = "Administrador,Profesional")]
        public async Task<IActionResult> Confirmar(Guid id)
        {


            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var turno = _context.Turno.Include(t => t.Paciente).Include(t => t.Profesional).FirstOrDefault(t => t.Id == id);

                if (turno.Confirmado) return RedirectToAction(nameof(Index)); // DEVOLVER UN ERROR 

                turno.Confirmado = true;
                turno.DescripcionCancelacion = null;

                _context.Update(turno);
                await _context.SaveChangesAsync();


            }
            catch (Exception EX)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = nameof(Rol.Profesional))]
        public async Task<IActionResult> Atendido(Guid id)
        {

            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var turno = _context.Turno.Include(t => t.Paciente).Include(t => t.Profesional).FirstOrDefault(t => t.Id == id);

                turno.Atendido = true;
                turno.Activo = false;
                turno.Confirmado = false;
                turno.DescripcionCancelacion = "Turno atendido";

                _context.Update(turno);
                await _context.SaveChangesAsync();


            }
            catch (Exception EX)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Cancelar(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turno = await _context.Turno.FindAsync(id);
            if (turno == null)
            {
                return NotFound();
            }
            return View(turno);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancelar(Guid id, [Bind("Id,DescripcionCancelacion")] Turno turno)
        {

            if (id == null )
            {
                return NotFound();
            }

            try
            {
                var Nuevoturno = _context.Turno.Include(t => t.Paciente).Include(t => t.Profesional).FirstOrDefault(t => t.Id == id);

                if (string.IsNullOrEmpty(turno.DescripcionCancelacion))
                {
                    ModelState.AddModelError(nameof(Turno.DescripcionCancelacion),"campo requerido");
                    return View();
                }

                Nuevoturno.Activo = false;
                Nuevoturno.DescripcionCancelacion = turno.DescripcionCancelacion;
                Nuevoturno.Confirmado = false;
                _context.Update(Nuevoturno);
                await _context.SaveChangesAsync();
            }
            catch (Exception EX)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Seleccionar(string id)
        {

            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var turno = _context.Turno.Include(t => t.Paciente).Include(t => t.Profesional).FirstOrDefault(t => t.Id == Guid.Parse(id));
                var userId = Guid.Parse(User.FindFirst("UserId").Value);

                turno.PacienteId = userId;
                turno.Activo = true;
                _context.Update(turno);
                await _context.SaveChangesAsync();
            }
            catch (Exception EX)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
