using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FinanceGO.Core.UserServices
{
    public interface ILoggedUserService
    {
        ClaimsPrincipal GetUser();
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaims();
        int GetUserId();
    }
}