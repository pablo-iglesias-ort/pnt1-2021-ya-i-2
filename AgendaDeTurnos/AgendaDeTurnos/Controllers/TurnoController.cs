﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgendaDeTurnos.Data;
using AgendaDeTurnos.Models;
using System.Security.Claims;

namespace AgendaDeTurnos.Controllers
{
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
            var agendaDeTurnosContext = _context.Turno.Include(t => t.Paciente).Include(t => t.Profesional);
            return View(await agendaDeTurnosContext.ToListAsync());
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
        public IActionResult Create()
        {
            ViewData["ListaProfesional"] = new SelectList(_context.Profesional,)
            return View();
        }

        public IActionResult SelecionPrestacion()
        {

            //1 verificar si el paciente actual tiene un turno activo 
            //2 Agragar el turno al paciente en estado 

            var userId = Guid.Parse(User.FindFirst("UserId").Value);
            ViewData["ListPrestaciones"] = new SelectList(_context.Prestacion, "PrestacionId", "Descripcion");
            return View();
        }

        public IActionResult SelecionProfesional(Prestacion Prestacion)
        {
            var profes = _context.Profesional;
            IQueryable<Profesional> profe;

            if (User.FindFirst(ClaimTypes.Role).Value == Rol.Administrador.ToString()){
                ViewData["ListProfesional"] = new SelectList(profes, "Id", "Nombre");
            }
            else {
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

        // POST: Turno/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,Confirmado,Activo,FechaAlta,DescripcionCancelacion,PacienteId,ProfesionalId")] Turno turno)
        {

            turno.FechaAlta = DateTime.Now;
            if (ModelState.IsValid && !TurnoExists(new Guid(), turno ))
            {
                turno.Id = Guid.NewGuid();
                _context.Add(turno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PacienteId"] = new SelectList(_context.Paciente, "Id", "Apellido", turno.PacienteId);
            ViewData["ProfesionalId"] = new SelectList(_context.Paciente, "Id", "Apellido", turno.ProfesionalId);
            return View(turno);
        }

        // GET: Turno/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
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
            ViewData["PacienteId"] = new SelectList(_context.Paciente, "Id", "Apellido", turno.PacienteId);
            ViewData["ProfesionalId"] = new SelectList(_context.Paciente, "Id", "Apellido", turno.ProfesionalId);
            return View(turno);
        }

        // POST: Turno/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Fecha,Confirmado,Activo,FechaAlta,DescripcionCancelacion,PacienteId,ProfesionalId")] Turno turno)
        {
            if (id != turno.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(turno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TurnoExists(turno.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PacienteId"] = new SelectList(_context.Paciente, "Id", "Apellido", turno.PacienteId);
            ViewData["ProfesionalId"] = new SelectList(_context.Paciente, "Id", "Apellido", turno.ProfesionalId);
            return View(turno);
        }

        // GET: Turno/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
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

        // POST: Turno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var turno = await _context.Turno.FindAsync(id);
            _context.Turno.Remove(turno);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TurnoExists(Guid id,Turno turno = null)
        {
           if(turno!=null) return _context.Turno.Any(e => e.Fecha == turno.Fecha && e.ProfesionalId == turno.ProfesionalId && e.PacienteId == turno.PacienteId && turno.Activo == true);
           return _context.Turno.Any(e => e.Id == id);
        }
    }
}
