using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

#nullable disable

namespace Domain.Models
{
    public partial class UserToken : IdentityUserToken<int>
    {
        public virtual ApplicationUser User { get; set; }
    }
}
