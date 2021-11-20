using System;
using System.Collections.Generic;

#nullable disable

namespace Migration.Models
{
    public partial class UserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
