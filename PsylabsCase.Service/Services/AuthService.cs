using PsylabsCase.Core.Common;
using PsylabsCase.Data;

namespace PsylabsCase.Service.Services
{
    public class AuthService 
    {
        private readonly TokenGenerator _tokenGenerator;
        private readonly PsylabsDbContext _context;

        public AuthService(TokenGenerator tokenGenerator, PsylabsDbContext context)
        {
            _tokenGenerator = tokenGenerator;
            _context = context;
        }

        public AuthResult Authenticate(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(t => t.Username == username);
            if (user == null)
            {
                return new AuthResult()
                {
                    IsSuccessful = false,
                    ErrorMessage = "User with given username doesn't exist."
                };
            }

            if (user.Password != password)
            {
                return new AuthResult()
                {
                    IsSuccessful = false,
                    ErrorMessage = "Username or password is invalid"
                };
            }

            AuthResult res = new()
            {
                User = user,
                Token = _tokenGenerator.GenerateToken(user),
                IsSuccessful = true,
            };

            return res;
        }
    }
}
