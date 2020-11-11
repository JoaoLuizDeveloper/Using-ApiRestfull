using NativaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NativaAPI.Repository.IRepository
{
    public interface IMarcaRepository
    {
        ICollection<Marca> GetMarcas();
        Marca GetMarca(int marcasId);
        bool MarcaExists(string name);
        bool MarcaExists(int id);
        bool CreateMarca(Marca nationalPark);
        bool UpdateMarca(Marca nationalPark);
        bool DeleteMarca(Marca nationalPark);
        bool Save();
    }
}