using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavitaWeb.Models.ViewModel
{
    public class PatrimoniosVM
    {
        public IEnumerable<SelectListItem> MarcasList { get; set; }
        public Patrimonio Patrimonio { get; set; }
    }
}
