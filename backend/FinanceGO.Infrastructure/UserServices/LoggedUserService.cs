using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FinanceGO.Core.UserServices;
using Microsoft.AspNetCore.Http;

namespace FinanceGO.Infrastructure.UserServices
{
    public class LoggedUserService : ILoggedUserService
    {     
        private readonly IHttpContextAccessor _accessor;
        public LoggedUserService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;            
        }

        public IEnumerable<Claim> GetClaims()
        {
            return _accessor.HttpContext.User.Claims;
        }

        public ClaimsPrincipal GetUser()
        {
            return _accessor.HttpContext.User;
        }

        public int GetUserId()
        {
            var claim = _accessor.HttpContext.User.Claims.SingleOrDefault(c => c.Type == "UserId");

            var claimValue = claim.Value;

            return Convert.ToInt32(claimValue);
        }

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}