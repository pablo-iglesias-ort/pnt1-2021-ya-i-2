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
    public class PrestacionController : Controller
    {
        private readonly AgendaDeTurnosContext _context;

        public PrestacionController(AgendaDeTurnosContext context)
        {
            _context = context;
        }

        // GET: Prestacion
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Prestacion.ToListAsync());
        }

        // GET: Prestacion/Details/5
        [Authorize]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestacion = await _context.Prestacion
                .FirstOrDefaultAsync(m => m.PrestacionId == id);
            if (prestacion == null)
            {
                return NotFound();
            }

            return View(prestacion);
        }

        [Authorize(Roles = nameof(Rol.Administrador))]
        // GET: Prestacion/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Prestacion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(Rol.Administrador))]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,Duracion,Precio")] Prestacion prestacion)
        {
            if (ModelState.IsValid)
            {
                prestacion.PrestacionId = Guid.NewGuid();
                _context.Add(prestacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prestacion);
        }

        // GET: Prestacion/Edit/5
        [Authorize(Roles = nameof(Rol.Administrador))]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestacion = await _context.Prestacion.FindAsync(id);
            if (prestacion == null)
            {
                return NotFound();
            }
            return View(prestacion);
        }

        // POST: Prestacion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = nameof(Rol.Administrador))]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PrestacionId,Nombre,Descripcion,Duracion,Precio")] Prestacion prestacion)
        {
            if (id != prestacion.PrestacionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prestacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrestacionExists(prestacion.PrestacionId))
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
            return View(prestacion);
        }

        // GET: Prestacion/Delete/5
        [Authorize(Roles = nameof(Rol.Administrador))]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestacion = await _context.Prestacion
                .FirstOrDefaultAsync(m => m.PrestacionId == id);
            if (prestacion == null)
            {
                return NotFound();
            }

            return View(prestacion);
        }

        // POST: Prestacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(Rol.Administrador))]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var prestacion = await _context.Prestacion.FindAsync(id);
            _context.Prestacion.Remove(prestacion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> PrestacionesAsync()
        {

            return View(await _context.Prestacion.ToListAsync());
        }

        private bool PrestacionExists(Guid id)
        {
            return _context.Prestacion.Any(e => e.PrestacionId == id);
        }
    }
}
