using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDeTurnos.Models
{
    public class Turno
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "La fecha es requerida")]
        public DateTime Fecha { get; set; }
        public bool Confirmado { get; set; } = false;
        public bool Activo { get; set; } = false;

         public DateTime FechaAlta { get; set; }
        
        [Display(Name = "Descripcion De Cancelacion")]        
        public string DescripcionCancelacion { get; set; }
        // Relaciones

        [ForeignKey(nameof(Paciente))]
        public Guid PacienteId { get; set; }
        public Paciente Paciente { get; set; }


        [ForeignKey(nameof(Profesional))]
        [Display(Name = "Profesional")]
        public Guid ProfesionalId { get; set; }
    
        public Profesional Profesional { get; set; }
    }
}
