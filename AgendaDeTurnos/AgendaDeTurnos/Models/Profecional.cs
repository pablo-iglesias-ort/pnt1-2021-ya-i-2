using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDeTurnos.Models
{
    public class Profecional: Persona
    {
        #region Actributos o Propiedades

        [Required(ErrorMessage = "el id es requerido")]
        public int Id { get; set; }

        [Required(ErrorMessage = "la fecha de alta es requerido")]
        public DateTime FechaAlta { get; set; }


        [EmailAddress(ErrorMessage = "Ingrese una dirección de correo electrónico válida")]
        public string Email { get; set; }

        [Required(ErrorMessage = "la matricula es requerido")]
        public string Matricula { get; set; }

        public Prestacion Prestacion { get; set; }

        public String HoraInicio { get; set; }

        public DateTime HoraFin { get; set; }

        public virtual ICollection<Turno> turnos { get; set; }
      
        #endregion

        #region Metodos 

        #endregion

    }
}
