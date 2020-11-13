using NavitaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavitaAPI.Repository.IRepository
{
    public interface IMarcaRepository
    {
        ICollection<Marca> GetMarcas();
        Marca GetMarca(int marcasId);
        bool MarcaExists(string name);
        bool MarcaExists(int id);
        bool CreateMarca(Marca marca);
        bool UpdateMarca(Marca marca);
        bool DeleteMarca(Marca marca);
        bool Save();
    }
}