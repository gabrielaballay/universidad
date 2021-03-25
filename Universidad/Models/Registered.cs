using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Universidad.Models
{
    public class Registered
    {
        [Key]
        public int Id_registered { get; set; }
        public int Id_subject { get; set; }
        public int Id_user { get; set; }
        public byte Active { get; set; }
    }
}
