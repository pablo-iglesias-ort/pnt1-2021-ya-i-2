using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDeTurnos.Models
{
    public class Formulario
    {
        #region Atributos o Propiedades

        [Required(ErrorMessage = "el id es requerido")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "la fecha es requerida")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "el email es requerido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "el Nombre es requerido")]
        public string Nombre { get; set;}

        [Required(ErrorMessage = "el Apellido  es requerido")]
        public string Apellido { get; set;}

        public bool Leido { get; set; } = false;

        [Required(ErrorMessage = "el titulo es requerido")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "el mensaje es requerido")]
        public string Mensaje { get; set; }

        [Required(ErrorMessage = "el usuario es requerido")]
        public Usuario Usuario { get; set; }

        #endregion

        #region Metodos 

        #endregion

    }
}
