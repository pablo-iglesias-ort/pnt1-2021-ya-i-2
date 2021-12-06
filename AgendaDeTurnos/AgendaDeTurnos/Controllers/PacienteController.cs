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
    [Authorize]
    public class PacienteController : Controller
    {
        private readonly AgendaDeTurnosContext _context;

        public PacienteController(AgendaDeTurnosContext context)
        {
            _context = context;
        }

        // GET: Pacientes
        [Authorize(Roles = nameof(Rol.Administrador))]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Paciente.ToListAsync());
        }
        

        // GET: Pacientes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Paciente.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }
            return View(paciente);
        }

        // POST: Pacientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ObraSocial,Id,Telefono,Direccion")] Paciente paciente)
        {
            if (id != paciente.Id)
            {
                return NotFound();
            }

            
            try
            {
                var pacienteUpdate = await _context.Paciente.FindAsync(id);

                pacienteUpdate.ObraSocial = paciente.ObraSocial;
                pacienteUpdate.Telefono = paciente.Telefono;
                pacienteUpdate.Direccion = paciente.Direccion;
                _context.Update(pacienteUpdate);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PacienteExists(paciente.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(datosGuardados));
        }

        public async Task<IActionResult> datosGuardados(Guid? id)
        {
            
            return View();
        }


        private bool PacienteExists(Guid id)
        {
            return _context.Paciente.Any(e => e.Id == id);
        }
    }
}
