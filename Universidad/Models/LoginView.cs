using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Universidad.Models
{
    public class LoginView
    {
        [RegularExpression("^[0-9]{6,9}$", ErrorMessage = "Legajo o DNI incorrecto")]
        [Display(Name = "Legajo/DNI")]        
        public int Dni_Docket { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z]).{4,8}$", ErrorMessage = "Ingrese min. 6 caracteres entre numeros y letras")]
        public string Clave { get; set; }
    }
}
