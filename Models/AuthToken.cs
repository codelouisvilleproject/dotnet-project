using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_project.Models
{
    public class AuthToken
    {
        public AuthToken()
        { }

        public AuthToken(string token, DateTime expiration)
        {
            token = Token;
            expiration = Expiration;
        }

        [Key]
        public string Token { get; set; }
        public DateTime Expiration { get; set; }

    }
}
