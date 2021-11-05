using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AgendaDeTurnos.Models;

namespace AgendaDeTurnos.Data
{
    public class AgendaDeTurnosContext : DbContext
    {
        public AgendaDeTurnosContext (DbContextOptions<AgendaDeTurnosContext> options)
            : base(options)
        {
        }
        public DbSet<AgendaDeTurnos.Models.Paciente> Paciente { get; set; }
        public DbSet<AgendaDeTurnos.Models.Profesional> Profesional { get; set; }
        public DbSet<AgendaDeTurnos.Models.Administrador> Administrador { get; set; }
        public DbSet<AgendaDeTurnos.Models.Prestacion> Prestacion { get; set; }
        public DbSet<AgendaDeTurnos.Models.Turno> Turno { get; set; }
        public DbSet<AgendaDeTurnos.Models.Usuario> Usuario { get; set; }
    }
}
