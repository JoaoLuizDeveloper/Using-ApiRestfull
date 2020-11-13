using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavitaWeb.Models.ViewModel
{
    public class IndexVM
    {
        public IEnumerable<Marca> MarcaList { get; set; }
        public IEnumerable<Patrimonio> PatrimonioList { get; set; }
    }
}