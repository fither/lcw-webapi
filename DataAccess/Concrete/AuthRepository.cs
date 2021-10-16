using DataAccess.Abstract;
using System;
using System.Linq;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Entities.Models;
using Microsoft.Extensions.Options;
using BC = BCrypt.Net.BCrypt;

namespace DataAccess.Concrete
{
    public class AuthRepository: IAuthRepository
    {
        private readonly AppSettings _appSettings;
        private DataContext _context;
        public AuthRepository(IOptions<AppSettings> appSettings, DataContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public User Authenticate(AuthDto auth)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username.Equals(auth.Username));

            if(user == null || !BC.Verify(auth.Password, user.Password))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            user.Password = null;

            return user;
        }
    }
}
