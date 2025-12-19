using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Streetcode.DAL.Entities.Users;

namespace Streetcode.DAL.Entities.Jwt
{
    [Table("RefreshTokens", Schema = "RefreshTokens")]
    public class RefreshToken
    {
        public int Id { get; set; }

        public string Token { get; set; } = null!;

        public int UserId { get; set; }

        public User User { get; set; } = null!;

        public DateTime ExpiresOn { get; set; }

        public bool IsRevoked { get; set; }
    }
}
