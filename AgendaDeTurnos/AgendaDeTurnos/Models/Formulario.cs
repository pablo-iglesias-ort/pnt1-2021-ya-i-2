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

        [Required(ErrorMessage = "el campo fecha es requerido")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "el campo email es requerido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "el campo Nombre es requerido")]
        public string Nombre { get; set;}

        [Required(ErrorMessage = "el campo Apellido  es requerido")]
        public string Apellido { get; set;}

        public bool Leido { get; set; } = false;

        public string Titulo { get; set; }

        public string Mensaje { get; set; }

        public Usuario Usuario { get; set; }

        #endregion

        #region Metodos 

        #endregion

    }
}
