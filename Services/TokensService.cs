using Microsoft.IdentityModel.Tokens;
using PikyServer.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace PikyServer.Services
{
    public class TokensService : ITokensService
    {     
        private readonly IConfiguration _configuration;

        public TokensService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateAccessToken( User CorrectUser )
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes( _configuration["SecurityKeyJwt"] ));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, CorrectUser.User_Name),
                    new Claim(ClaimTypes.Role, CorrectUser.User_Role)
                };


            var tokeOptions = new JwtSecurityToken(
                issuer: "https://localhost:44375",
                audience: "https://localhost:44375",
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signinCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken( tokeOptions );
        }

        public string CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        /*
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = "https://localhost:44375",
                ValidAudience = "https://localhost:44375",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityKeyJwt"]))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
        */
    }
}
