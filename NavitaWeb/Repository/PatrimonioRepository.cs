using NavitaWeb.Models;
using NavitaWeb.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NavitaWeb.Repository
{
    public class PatrimonioRepository : Repository<Patrimonio>, IPatrimonioRepository
    {
        private readonly IHttpClientFactory _clientFactory;
        public PatrimonioRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
