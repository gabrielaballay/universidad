using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Universidad.Models
{
    public class Registered
    {
        public int Id_registered { get; set; }
        public int Id_subject { get; set; }
        public int Id_user { get; set; }
        public bool Active { get; set; }
    }
}
