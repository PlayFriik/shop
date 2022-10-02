using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Shop.Presentation.Extensions;

public static class ClaimsPrincipalExtension
{
    public static string CreateJwt(this ClaimsPrincipal claimsPrincipal, IConfiguration configuration)
    {
        if (!double.TryParse(configuration["JwtBearer:Active:Value"], out var value))
        {
            throw new FormatException("An error occurred while parsing JwtBearer:Active:Value");
        }

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtBearer:PrivateKey"]));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var expires = configuration["JwtBearer:Active:Unit"].ToLower() switch
        {
            "days" => DateTime.Now.AddDays(value),
            "hours" => DateTime.Now.AddHours(value),
            "milliseconds" => DateTime.Now.AddMilliseconds(value),
            "minutes" => DateTime.Now.AddMinutes(value),
            "months" => DateTime.Now.AddMonths((int)value),
            "seconds" => DateTime.Now.AddSeconds(value),
            "years" => DateTime.Now.AddYears((int)value),
            _ => throw new FormatException("An error occurred while parsing JwtBearer:Active:Unit")
        };

        var token = new JwtSecurityToken(
            configuration["JwtBearer:Issuer"],
            configuration["JwtBearer:Audience"],
            claimsPrincipal.Claims,
            expires: expires,
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}