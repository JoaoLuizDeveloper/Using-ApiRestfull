using NavitaWeb.Models;
using NavitaWeb.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NavitaWeb.Repository
{
    public class MarcaRepository : Repository<Marca>, IMarcaRepository 
    {
        private readonly IHttpClientFactory _clientFactory;
        public MarcaRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
