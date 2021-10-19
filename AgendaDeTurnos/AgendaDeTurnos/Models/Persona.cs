using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDeTurnos.Models
{   // Clases base para los datos basico de una persona 
    public class Persona
    {
        #region Atributos o  Propiedades

        [Key]
        [Required(ErrorMessage = "el id es requerido")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "el nombre es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "el apellido es requerido")]
        public string Apellido { get; set; }

        [RegularExpression(@"^(?!00000)[0-9]{8,8}$", ErrorMessage = "error en el formato DNI")]
        [Required(ErrorMessage = "el DNI es requerido")]
        public string DNI { get; set; }
        [RegularExpression(@"^(?!00000)[0-9]{8,8}$", ErrorMessage = "el numero de telefono no es valido ")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "la direccion es requerida")]
        public string Direccion { get; set; }

        #endregion

        #region Metodos 

        #endregion
    }
}
