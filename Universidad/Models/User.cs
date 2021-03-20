using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Universidad.Models
{
    public class User
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int DNI { get; set; }
        public int Legajo { get; set; }
        public string Password { get; set; }
        public bool Activo { get; set; }
    }
}
