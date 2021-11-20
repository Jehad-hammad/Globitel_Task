using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ILoggedInUserService
    {
        int GetUserId();
        List<string> GetUserRoles();
        string GetUserName();
    }
}
