using BitcoinLoggerServer.Framework.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BitcoinLoggerServer.Framework.Security
{
    public class JWTService
    {
        private IJWTSettings _jwtSettings { get; }

        public JWTService(IJWTSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        public string GenerateUserSession(IUser user)
        {
            //=================================================================
            //GENERATE JWT TOKEN
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] privateKey = Encoding.ASCII.GetBytes(_jwtSettings.PrivateKey);

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor();

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("UserEmail", user.Email.ToString()));
            claims.Add(new Claim("UserId", user.Id.ToString()));

            securityTokenDescriptor.Subject = new ClaimsIdentity(claims);
            securityTokenDescriptor.Expires =
                _jwtSettings.ExpirationMode.ToLower().Trim() == "day" ? DateTime.Now.AddDays(_jwtSettings.ExpirationValue) : DateTime.Now.AddMinutes(_jwtSettings.ExpirationValue);
            securityTokenDescriptor.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(privateKey), SecurityAlgorithms.HmacSha256Signature);

            SecurityToken securityToken = tokenHandler.CreateToken(securityTokenDescriptor);
            string sessionKey = tokenHandler.WriteToken(securityToken);
            //=================================================================

            return sessionKey;

        }

    }
}
