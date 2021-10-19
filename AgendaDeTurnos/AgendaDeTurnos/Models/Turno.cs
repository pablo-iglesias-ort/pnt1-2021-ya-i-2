using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDeTurnos.Models
{
    public class Turno
    {
        #region Atributos o Propiedades

        [Key]
        [Required(ErrorMessage = "el id es requerido")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "la fecha es requerida")]
        public DateTime Fecha { get; set; }
        public bool Confirmado { get; set; } = false;
        public bool Activo { get; set; } = true;
        [Required(ErrorMessage = "la fecha de  alta es requerida")]
        public DateTime FechaAlta { get; set; }
        
        // Paciented 
        public int IdPaciente { get; set; } // Clave externa del Paciente
        public Paciente Paciente { get; set; } // proiedad de nevegacion para pientes

        // Profeciona 
        public int Idprofesional { get; set; } // Clave externa del Profecional
        public Profesional Profesional { get; set; } // proiedad de nevegacion para profesionales

        [Required(ErrorMessage = "la descripcion es requerida")]
        public string DescripcionCancelacion { get; set; }

    
        #endregion

        #region Metodos 

        #endregion
    }
}
