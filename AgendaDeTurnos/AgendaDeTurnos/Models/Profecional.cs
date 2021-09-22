using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDeTurnos.Models
{
    public class Profecional: Persona
    {
        #region Atributos o Propiedades

        [Required(ErrorMessage = "el id es requerido")]
        public Guid Idprofesional { get; set; }

        [Required(ErrorMessage = "la fecha de alta es requerida")]
        public DateTime FechaAlta { get; set; }

        [EmailAddress(ErrorMessage = "Ingrese una dirección de correo electrónico válida")]
        public string Email { get; set; }

        [Required(ErrorMessage = "la matricula es requerida")]
        public string Matricula { get; set; }

        public Prestacion Prestacion { get; set; }

        [Required(ErrorMessage = "la hora de inicio es requerida")]
        public String HoraInicio { get; set; }

        [Required(ErrorMessage = "la hora de fin es requerida")]
        public DateTime HoraFin { get; set; }

        public  List<Turno> Turno { get; set; } // propiedad de navegacion de turnos del profeconal
      
        #endregion

        #region Metodos 

        #endregion

    }
}
