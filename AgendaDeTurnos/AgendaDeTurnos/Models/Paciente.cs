using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDeTurnos.Models
{
    public class Paciente : Usuario
    {
        [Required(ErrorMessage = "la obra social es requerida")]
        [Display(Name = "Obra Social")]
        public string ObraSocial { get; set; }

        public override Rol Rol => Rol.Paciente;


        // Relaciones

        [NotMapped]
        public IEnumerable<Turno> Turnos { get; set; }
    }
}