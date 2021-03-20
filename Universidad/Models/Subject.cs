using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Universidad.Models
{
    public class Subject
    {
        public string Nombre { get; set; }
        public string Horario { get; set; }
        public int CupoMaximo { get; set; }
        public int id_teacher { get; set; }
    }
}
