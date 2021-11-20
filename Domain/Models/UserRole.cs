using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

#nullable disable

namespace Domain.Models
{
    public partial class UserRole : IdentityUserRole<int>
    {

        public virtual Role Role { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
