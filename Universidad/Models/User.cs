using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Universidad.Models
{
    public class User
    {
        [Key]
        public int Id_user { get; set; }
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
        [Required]
        [RegularExpression("^[0-9]{6}$", ErrorMessage = "Legajo incorrecto")]
        [Display(Name = "Legajo")]
        public int Docket { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z]).{6,8}$", ErrorMessage = "Contraseña NO valida!!!")]
        public string Password { get; set; }
        public byte Active { get; set; }
        public int Id_type { get; set; }
    }
}
