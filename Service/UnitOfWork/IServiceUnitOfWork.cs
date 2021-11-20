using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.UnitOfWork
{
    public interface IServiceUnitOfWork : IDisposable
    {
        Lazy<IAuthService> Auth { get; set; }
        Lazy<IUserService> User { get; set; }
        Lazy<IExcelService> Excel { get; set; }
    }
}
