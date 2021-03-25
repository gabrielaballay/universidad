using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Universidad.Models
{
    public class Teacher
    {
        [Key]
        public int Id_teacher { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Debe ingresar al menos 2 caracteres")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Debe ingresar al menos 2 caracteres")]
        [Display(Name = "Apellido")]
        public string Last_name { get; set; }
        [Required]
        [RegularExpression("^[0-9]{8,9}$", ErrorMessage = "DNI incorrecto")]
        public int DNI { get; set; }
        public byte Active { get; set; }
        public int Id_user { get; set; }
    }
}
