﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AgendaDeTurnos.Models
{
    public class Usuario
    {
        #region Atributos o Propiedades

        [Key]
        [Required(ErrorMessage ="el id es requerido")]
        public Guid Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "el nombre es requerido")]
        public string nombre { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Ingrese una dirección de correo electrónico válida")]
        public string email { get; set; }

        [Display(Name = "Fecha de Alta")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "la fecha de alta es requerido")]
        public DateTime fechaAlta { get; set; }

        [Display(Name = "Contraseña")]
        [StringLength(20, MinimumLength = 5,ErrorMessage = "La {0} debe tener {2} de mínimo")]
        [Required(ErrorMessage = "el password es requerido")]
        public string password { get; set; }

        [Display(Name = "Rol")]
        [Required(ErrorMessage = "el rol es requerido")]
        public string rol { get; set; }

        #endregion

        #region Metodos 

        #endregion

    }
}
