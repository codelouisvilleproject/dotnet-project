using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Column("token")]
        public string Token { get; set; }
        [Column("expiration")]
        public DateTime Expiration { get; set; }

    }
}

