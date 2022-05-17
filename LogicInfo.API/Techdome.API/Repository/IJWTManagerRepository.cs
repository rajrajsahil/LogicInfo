using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Techdome.API.Model;

namespace Techdome.API.Repository
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(Users users);
    }
}
