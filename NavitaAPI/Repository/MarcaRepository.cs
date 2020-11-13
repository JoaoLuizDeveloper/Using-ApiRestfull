using NavitaAPI.Data;
using NavitaAPI.Models;
using NavitaAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavitaAPI.Repository
{
    public class MarcaRepository : IMarcaRepository
    {
        private readonly ApplicationDbContext _db;
        public MarcaRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool CreateMarca(Marca marcas)
        {
            _db.Marcas.Add(marcas);
            return Save();
        }

        public bool DeleteMarca(Marca marcas)
        {
            _db.Marcas.Remove(marcas);
            return Save();
        }

        public Marca GetMarca(int marcaId)
        {
            return _db.Marcas.FirstOrDefault(n => n.Id == marcaId);
        }

        public ICollection<Marca> GetMarcas()
        {
            return _db.Marcas.OrderBy(n => n.Nome).ToList();
        }

        public bool MarcaExists(string name)
        {
            bool value = _db.Marcas.Any(n => n.Nome.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool MarcaExists(int id)
        {
            bool value = _db.Marcas.Any(n => n.Id == id);
            return value;
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateMarca(Marca marcas)
        {
            _db.Marcas.Update(marcas);
            return Save();
        }
    }
}
