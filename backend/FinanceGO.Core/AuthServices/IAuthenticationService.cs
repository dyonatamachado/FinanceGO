using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceGO.Core.AuthServices
{
    public interface IAuthenticationService
    {
        string GenerateJwtToken(int id);
        string ComputeSha256Hash(string password);
    }
}