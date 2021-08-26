using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDeTurnos.Models
{
    public class Prestacion
    {
        #region Actributos o Propiedades

        [Required(ErrorMessage = "el id es requerido")]
        public int id { get; set; } 
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime Duracion { get; set; }
        public double Precio { get; set; }
        public Profecional Profesionales { get; set; }
     
        #endregion

        #region Metodos 

        #endregion
    }
}
