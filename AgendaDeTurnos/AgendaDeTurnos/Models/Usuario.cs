using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AgendaDeTurnos.Models
{
    public abstract class Usuario
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El Nombre es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El Apellido es requerido")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El Dni es requerido")]
        public string Dni { get; set; }

        [EmailAddress(ErrorMessage = "Ingrese un Email válida")]
        public string Email { get; set; }

        [Required(ErrorMessage = "el Telefono es requerido")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "el Telefono es requerido")]
        public string Direccion { get; set; }

        [Display(Name = "Fecha de Alta")]
        [Required(ErrorMessage = "La Fecha de Alta es requerido")]
        [DataType(DataType.Date)]
        public DateTime FechaAlta { get; set; }

        [Display(Name = "Contraseña")]
        [StringLength(20, MinimumLength = 5,ErrorMessage = "La {0} debe tener {2} de mínimo")]
        [Required(ErrorMessage = "El Contraseña es requerido")]
        public string Password { get; set; }

        [Display(Name = "Rol")]
        [Required(ErrorMessage = "El Rol es requerido")]
        public abstract Rol Rol { get;}

    }
}
