using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static NavitaAPI.Models.Patrimonio;

namespace NavitaAPI.Models.DTOs
{
    public class PatrimonioUpdateDto
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        public string Descricao { get; set; }

        [Required]
        public int MarcaId { get; set; }

        public DateTime Created { get; set; }
    }
}
