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

        [Required(ErrorMessage = "el id es requerido")]
        public int Id { get; set; }
        [Required(ErrorMessage = "la fecha es requerida")]
        public DateTime Fecha { get; set; }
        public bool Confirmado { get; set; } = false;
        public bool Activo { get; set; } = true;
        [Required(ErrorMessage = "la fecha de  alta es requerida")]
        public DateTime FechaAlta { get; set; }
        // Paciented 
        public Paciente Paciented { get; set; }

        // Profeciona 
        public int Idprofesional { get; set; }
        public string DescripcionCancelacion { get; set; }

    
        #endregion

        #region Metodos 

        #endregion
    }
}
