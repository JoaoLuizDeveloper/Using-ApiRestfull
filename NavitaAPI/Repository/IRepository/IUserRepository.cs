using NavitaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavitaAPI.Repository.IRepository
{
    public interface IUserRepository
    {
        bool isUniqueUser(string username);
        User Authenticate(string username, string email, string password);
        User Register(string username, string email, string password);
    }
}