using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDeTurnos.Models
{
    public class Paciente:Persona
    {
        #region Atributos o Propiedades
        [Required(ErrorMessage = "el id es requerido")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "la fecha de alta requerida")]
        public DateTime fechaAlta { get; set; }

        [Required(ErrorMessage = "el email es requerido")]
        [EmailAddress(ErrorMessage = "Ingrese una dirección de correo electrónico válida")]
        public string email { get; set; }

        public string obraSocial { get; set; }

        public List<Turno> turnos { get; set; }

        #endregion

        #region Metodos 

        #endregion
    }
}
