using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgendaDeTurnos.Data;
using AgendaDeTurnos.Models;
using Microsoft.AspNetCore.Authorization;

namespace AgendaDeTurnos.Controllers
{
    [Authorize(Roles = nameof(Rol.Administrador))]
    public class ProfesionalController : Controller
    {
        private readonly AgendaDeTurnosContext _context;
        private readonly ISeguridad seguridad = new SeguridadBasica();

        public ProfesionalController(AgendaDeTurnosContext context)
        {
            _context = context;
        }

        // GET: Profesionales
        public async Task<IActionResult> Index()
        {
            var agendaDeTurnosContext = _context.Profesional.Include(p => p.Prestacion);
            return View(await agendaDeTurnosContext.ToListAsync());
        }

        // GET: Profesionales/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesional = await _context.Profesional
                .Include(p => p.Prestacion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesional == null)
            {
                return NotFound();
            }

            return View(profesional);
        }

        [Authorize(Roles = nameof(Rol.Administrador))]
        // GET: Profesionales/Create
        public IActionResult Create()
        {
            ViewData["PrestacionId"] = new SelectList(_context.Set<Prestacion>(), "PrestacionId", "Descripcion");
            return View();
        }

        // POST: Profesionales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(Rol.Administrador))]
        public async Task<IActionResult> Create(Profesional profesional, string pass)
        {
            if (ModelState.IsValid)
            {
                if (_context.Profesional.Any(p => p.Email == profesional.Email))
                {
                    ModelState.AddModelError(nameof(Paciente.Email), "El mail ya esta utilizado");
                }
                else
                {
                    if (seguridad.ValidarPass(pass))
                    {
                        profesional.Id = Guid.NewGuid();
                        profesional.FechaAlta = DateTime.Now;
                        profesional.Password = seguridad.EncriptarPass(pass);
                        _context.Add(profesional);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(nameof(Paciente.Password), "No cumple con los requisitos");
                    }
                }
            }
            ViewData["PrestacionId"] = new SelectList(_context.Set<Prestacion>(), "PrestacionId", "Descripcion", profesional.PrestacionId);
            return View(profesional);
        }

        [Authorize(Roles = nameof(Rol.Administrador))]
        // GET: Profesionales/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesional = await _context.Profesional.FindAsync(id);
            if (profesional == null)
            {
                return NotFound();
            }
            ViewData["PrestacionId"] = new SelectList(_context.Set<Prestacion>(), "PrestacionId", "Descripcion", profesional.PrestacionId);
            return View(profesional);
        }

        // POST: Profesionales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(Rol.Administrador))]
        public async Task<IActionResult> Edit(Guid id, [Bind("Matricula,HoraInicio,HoraFin,PrestacionId,Id,Nombre,Apellido,Dni,Email,Telefono,Direccion")] Profesional profesional)
        {
            if (id != profesional.Id)
            {
                return NotFound();
            }
                
                    var profesionalUpdate = await _context.Profesional.FindAsync(id);

                    profesionalUpdate.Matricula = profesional.Matricula;
                    profesionalUpdate.HoraInicio = profesional.HoraInicio;
                    profesionalUpdate.HoraFin = profesional.HoraFin;
                    profesionalUpdate.PrestacionId = profesional.PrestacionId;
                    profesionalUpdate.Nombre = profesional.Nombre;
                    profesionalUpdate.Apellido = profesional.Apellido;
                    profesionalUpdate.Dni = profesional.Dni;
                    profesionalUpdate.Email = profesional.Email;
                    profesionalUpdate.Telefono = profesional.Telefono;
                    profesionalUpdate.Direccion = profesional.Direccion;

                    _context.Update(profesionalUpdate);
                    await _context.SaveChangesAsync();

                
            ViewData["PrestacionId"] = new SelectList(_context.Set<Prestacion>(), "PrestacionId", "Descripcion", profesional.PrestacionId);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = nameof(Rol.Administrador))]
        // GET: Profesionales/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesional = await _context.Profesional
                .Include(p => p.Prestacion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesional == null)
            {
                return NotFound();
            }

            return View(profesional);
        }

        // POST: Profesionales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(Rol.Administrador))]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var profesional = await _context.Profesional.FindAsync(id);
            _context.Profesional.Remove(profesional);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfesionalExists(Guid id)
        {
            return _context.Profesional.Any(e => e.Id == id);
        }
    }
}
