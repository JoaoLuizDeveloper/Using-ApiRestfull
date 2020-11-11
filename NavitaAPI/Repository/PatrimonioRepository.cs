using Microsoft.EntityFrameworkCore;
using NativaAPI.Data;
using NativaAPI.Models;
using NativaAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NativaAPI.Repository
{
    public class PatrimonioRepository : IPatrimonioRepository
    {
        private readonly ApplicationDbContext _db;
        public PatrimonioRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool CreatePatrimonio(Patrimonio patrimonio)
        {
            _db.Patrimonios.Add(patrimonio);
            return Save();
        }

        public bool DeletePatrimonio(Patrimonio patrimonio)
        {
            _db.Patrimonios.Remove(patrimonio);
            return Save();
        }

        public Patrimonio GetPatrimonio(int trailId)
        {
            return _db.Patrimonios.Include(c => c.Marca).FirstOrDefault(n => n.Id == trailId);
        }

        public ICollection<Patrimonio> GetPatrimonios()
        {
            return _db.Patrimonios.Include(c => c.Marca).OrderBy(n => n.Nome).ToList();
        }

        public bool PatrimonioExists(string name)
        {
            bool value = _db.Patrimonios.Any(n => n.Nome.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool PatrimonioExists(int id)
        {
            bool value = _db.Patrimonios.Any(n => n.Id == id);
            return value;
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdatePatrimonio(Patrimonio patrimonio)
        {
            _db.Patrimonios.Update(patrimonio);
            return Save();
        }

        public ICollection<Patrimonio> GetPatrimoniosInMarcas(int npId)
        {
            return _db.Patrimonios.Include(c=>c.Marca).Where(c=>c.Id == npId).ToList();
        }
    }
}