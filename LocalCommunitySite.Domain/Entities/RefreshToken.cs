﻿using Microsoft.AspNetCore.Identity;
using System;

namespace LocalCommunitySite.Domain.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Token { get; set; }

        public string JwtId { get; set; }

        public bool IsUsed { get; set; }

        public bool IsRevoked { get; set; }

        public DateTime AddedDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public virtual User User { get; set; }
    }
}
