using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDeTurnos.Models
{
    public class Prestacion
    {
        #region Atributos o Propiedades

        [Required(ErrorMessage = "el id es requerido")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "el nombre es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "la descripcion es requerida")]
        public string Descripcion { get; set; }
        public DateTime Duracion { get; set; }

        [Required(ErrorMessage = "el precio es requerido")]
        public double Precio { get; set; }
        public Profecional Profesionales { get; set; }
     
        #endregion

        #region Metodos 

        #endregion
    }
}
