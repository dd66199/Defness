using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.AuthDTOs
{
    public static class Auth
    {
        public class Login 
        {
            [Required]

            public string Username { get; set; }

            [Required]
            public string Password { get; set; }
        }

        public sealed class Refresh
        {
            [Required]
            public string RefreshToken { get; set; }
        }

        public sealed class Responce 
        {
            public string AccessToken { get; set; }

            public DateTime AccessTokenExpiteDate { get; set; }

            public string RefreshToken { get; set; }

            public DateTime RefreshTokenExpireTime { get; set; }

            public string UserName { get; set; }

            public IEnumerable<string> Roles { get; set; }
        }

        public class Jwt
        {
            public string Key { get; set; }

            public string Issuer { get; set; }

            public string Audience { get; set; }

            public int AccessTokenLifeTimeInMinutes { get; set; }

            public int RefreshTokenLifeTimeInMinutes { get; set; }
        }

    }
}
