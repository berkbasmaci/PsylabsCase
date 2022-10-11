using PsylabsCase.Core.Common;
using PsylabsCase.Data;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace PsylabsCase.API.Services
{
    public class LoginUserInfoProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly PsylabsDbContext _context;

        public LoginUserInfoProvider(IHttpContextAccessor httpContextAccessor, PsylabsDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public int UserId
        {
            get
            {
                //_httpContextAccessor.HttpContext?.User?.FindFirstValue(JwtRegisteredClaimNames.Sub);

                //TEST
                var user = _context.Users.First(t => t.Username == "admin"); 

                return user.Id;
            }
        }
    }
}
