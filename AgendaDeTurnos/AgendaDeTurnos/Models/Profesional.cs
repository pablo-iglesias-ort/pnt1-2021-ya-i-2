using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDeTurnos.Models
{
    public class Profesional : Usuario
    {
        [Required(ErrorMessage = "La matricula es requerida")]
        public string Matricula { get; set; }

        [Required(ErrorMessage = "La hora de inicio es requerida")]
        [DataType(DataType.Time)]
        public DateTime HoraInicio { get; set; }

        [Required(ErrorMessage = "La hora de fin es requerida")]
        [DataType(DataType.Time)]
        public DateTime HoraFin { get; set; }

        public override Rol Rol => Rol.Profesional;

        // Relaciones

        [ForeignKey(nameof(Prestacion))]
        public Guid PrestacionId { get; set; }
        public Prestacion Prestacion { get; set; }

        public  IEnumerable<Turno> Turnos { get; set; }

    }
}
