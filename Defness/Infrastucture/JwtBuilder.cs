using DTOs.AuthDTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace FCP_BACKEND.Infrastructure
{
    internal static class JwtBuilder
    {
        /// <summary>
        /// метод, который устанавливает базовые параметры валидации токена
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        internal static TokenValidationParameters Parameters(IConfiguration configuration)
        {
            var settings = configuration.GetSection(nameof(Auth.Jwt)).Get<Auth.Jwt>();

            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = settings.Issuer,
                ValidAudience = settings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Key)),
                ClockSkew = TimeSpan.Zero
            };
        }

        /// <summary>
        /// метод, создающий новый JWT-токен
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="jwt"></param>
        /// <returns></returns>
        internal static (JwtSecurityToken Token, DateTime Expire) SecurityToken(IEnumerable<Claim> claims, Auth.Jwt jwt)
        {
            var utc = DateTime.UtcNow;
            var expires = utc.Add(TimeSpan.FromMinutes(jwt.AccessTokenLifeTimeInMinutes));
            return (new JwtSecurityToken(
                jwt.Issuer,
                jwt.Audience,
                claims,
                utc,
                expires,
                new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwt.Key)), SecurityAlgorithms.HmacSha256Signature)),
                expires);
        }

        /// <summary>
        /// возвращает Bearer в нижнем регистре в качестве строки (нужно чтобы при отладке в Swagger'е не прописывать bearer...)
        /// </summary>
        /// <returns></returns>
        internal static string Bearer()
        {
            return nameof(Bearer).ToLower();
        }

        /// <summary>
        /// возвращает Authorization в качестве строки
        /// </summary>
        /// <returns></returns>
        internal static string Authorization()
        {
            return nameof(Authorization);
        }

    }
}
