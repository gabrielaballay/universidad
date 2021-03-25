using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Universidad.Models
{
    public class User_type
    {
        [Key]
        public int Id_type { get; set; }
        public string Type { get; set; }
    }
}
