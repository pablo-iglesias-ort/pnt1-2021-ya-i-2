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
        [Required]
        public string Email { get; set; }

        [Required(ErrorMessage = "el Telefono es requerido")]
        public string Telefono { get; set; }

        public string Direccion { get; set; }

        [Display(Name = "Fecha de Alta")]
        [DataType(DataType.Date)]
        public DateTime FechaAlta { get; set; }

        [Display(Name = "Contraseña")]
        public byte[] Password { get; set; }

        [Display(Name = "Rol")]
        public abstract Rol Rol { get;}

    }
}
