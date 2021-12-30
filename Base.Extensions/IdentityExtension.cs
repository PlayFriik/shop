using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Base.Extensions
{
    public static class IdentityExtension
    {
        public static string GenerateJwt(IEnumerable<Claim> claims, string key, string issuer, string audience,
            DateTime expiration)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            
            var token = new JwtSecurityToken(
                issuer,
                audience, 
                claims, 
                expires: expiration, 
                signingCredentials: signingCredentials
                );
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}