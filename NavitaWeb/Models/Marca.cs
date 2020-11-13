using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NavitaWeb.Models
{
    public class Marca
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        public DateTime Created { get; set; }
    }
}
