using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavitaWeb
{
    public static class SD
    {
        public static string APIBaseUrl = "https://localhost:44387";
        public static string MarcaAPIPath = APIBaseUrl + "/api/v1/marcas/";
        public static string PatrimonioAPIPath = APIBaseUrl + "/api/v1/patrimonios/";
        public static string AccountAPIPath = APIBaseUrl + "/api/v1/Users/";
    }
}