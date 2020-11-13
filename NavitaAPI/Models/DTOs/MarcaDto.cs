using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NavitaAPI.Models.DTOs
{
    public class MarcaDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        public DateTime Created { get; set; }
    }
}
