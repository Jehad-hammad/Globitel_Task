using System;
using System.Collections.Generic;

#nullable disable

namespace Migration.Models
{
    public partial class UserClaim
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
