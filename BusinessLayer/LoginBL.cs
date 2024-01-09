using BusinessObjectLayer;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLayer
{
    public class LoginBL
    {
        private LoginDL loginDL;
        public LoginBL()
        {
            loginDL = new LoginDL();
        }
        public string UserLogin(Login login)
        {
            var user = loginDL.UserLogin(login);
            if (user.Id == 0)
                return null;
            else
                return JWTTokenGenerator(user);
        }

        private string JWTTokenGenerator(User user)
        {
            var secret = WebConfigurationManager.AppSettings["Secret"];
            var issuer = WebConfigurationManager.AppSettings["Issuer"];
            var audience = WebConfigurationManager.AppSettings["Audience"];
            if (!Int32.TryParse(WebConfigurationManager.AppSettings["Expires"], out int _Expires))
                _Expires = 24;


            var _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var _signingCredentials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256
                );
            var _Header = new JwtHeader(_signingCredentials);

            var _Claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim("nombre", user.Name),
                new Claim("user_name", user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            };


            var _Payload = new JwtPayload(
                    issuer: issuer,
                    audience: audience,
                    claims: _Claims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddHours(_Expires)
                );

            var _Token = new JwtSecurityToken(
                    _Header,
                    _Payload
                );

            return new JwtSecurityTokenHandler().WriteToken(_Token);
        }
    }
}
