using NativaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NativaAPI.Repository.IRepository
{
    public interface IPatrimonioRepository
    {
        ICollection<Patrimonio> GetPatrimonios();
        ICollection<Patrimonio> GetPatrimoniosInMarcas(int npId);
        Patrimonio GetPatrimonio(int patrimonioId);
        bool PatrimonioExists(string name);
        bool PatrimonioExists(int id);
        bool CreatePatrimonio(Patrimonio patrimonio);
        bool UpdatePatrimonio(Patrimonio patrimonio);
        bool DeletePatrimonio(Patrimonio patrimonio);
        bool Save();
    }
}