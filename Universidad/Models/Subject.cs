using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Universidad.Models
{
    public class Subject
    {
        [Key]
        public int Id_subject { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Debe ingresar al menos 2 caracteres")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }        
        [Required]
        [Display(Name = "Cupo Maximo")]
        public int Max_flake { get; set; }
        public int Id_teacher { get; set; }
        [ForeignKey("Id_teacher")]
        public Teacher profe { get; set; }
        public int Id_User { get; set; }
        public byte Active { get; set; }
        [Display(Name ="Hora Inicio")]
        [Required]
        public TimeSpan Start_time { get; set; }
        [Display(Name = "Hora Fin")]
        [Required]
        public TimeSpan End_time { get; set; }

    }
}
