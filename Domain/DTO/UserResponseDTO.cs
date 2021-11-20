using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class UserResponseDTO
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public int? Age { get; set; }
        public int? Gender { get; set; }
        public int UserStatus { get; set; }
    }
}
