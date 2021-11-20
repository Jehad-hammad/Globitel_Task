using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class UserRequestDTO
    {
        public long Id { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public int? Gender { get; set; }
        public int? Age { get; set; }
        public string Address { get; set; }
        public int? Status { get; set; }
        
    }
}
