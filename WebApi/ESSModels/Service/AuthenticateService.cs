using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ESSModels.Interface;
using ESSModels.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ESSModels.Service
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly AppSettings _appSettings;
        public AuthenticateService(IOptions<AppSettings> options)
        {
            _appSettings = options.Value;
        }
        private IList<User> users = new List<User>() {
                new User{Id =1, UserName = "ABC123", FirstName = "ABCD",Password = "ABC123"},
                new User{Id =2, UserName = "ABC1234", FirstName = "ABCDEF",Password = "ABC1234"}
        };
        public User Authenticate(string username, string password)
        {
            var user = users.SingleOrDefault(m => m.UserName == username && m.Password == password);
            if (user == null)
                return null;
            var tokenHanlder = new JwtSecurityTokenHandler();
            var securityKey = Encoding.ASCII.GetBytes(_appSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(ClaimTypes.Version, "V7.2")
                }),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(securityKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHanlder.CreateToken(tokenDescriptor);
            user.Token = tokenHanlder.WriteToken(token);
            user.Password = null;
            return user;
        }
    }
}
