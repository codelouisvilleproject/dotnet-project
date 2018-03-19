using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_project.Models
{
    [Table("authtokens")]
    public class AuthToken
    {

        [Key]
        [Column("token")]
        public string Token { get; set; }
        [Column("expiration")]
        public DateTime Expiration { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
    }
}