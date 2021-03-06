using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDeTurnos.Models
{
    public class Prestacion
    {
        [Key]
        [Required(ErrorMessage = "el id es requerido")]
        public Guid PrestacionId { get; set; }

        [Required(ErrorMessage = "el nombre es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "la descripcion es requerida")]
        public string Descripcion { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Duracion (en horas)")]
        public DateTime Duracion { get; set; }

        [Required(ErrorMessage = "el precio es requerido")]
        [Display(Name = "Precio ($)")]
        public double Precio { get; set; }

        // Relaciones
        public IEnumerable<Profesional> Profesionales { get; set; }

       
    }
}
