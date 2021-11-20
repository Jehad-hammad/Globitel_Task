using System;
using System.Collections.Generic;

#nullable disable

namespace Migration.Models
{
    public partial class UserToken
    {
        public int UserId { get; set; }
        public string LoginProvider { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
