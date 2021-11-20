using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Repository.Context;
using Microsoft.AspNetCore.Http;
using Repository.UnitOfWork;
using Service.Interfaces;
using Service.Services;
using System;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;

namespace Service.UnitOfWork
{
    public class ServiceUnitOfWork : IServiceUnitOfWork
    {
        private readonly IRepositoryUnitOfWork _repositoryUnitOfWork;
        private readonly LoggedInUserService _loggedInUserService;
        private readonly IMapper _mapper;

        public Lazy<IAuthService> Auth { get; set; }
        public Lazy<IUserService> User { get; set; }
        public Lazy<IExcelService> Excel { get; set; }

        public ServiceUnitOfWork(GlobitelDBContext context, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, SignInManager<ApplicationUser> signInManager, IMapper mapper, IWebHostEnvironment _hostingEnvironment)
        {
            _repositoryUnitOfWork = new RepositoryUnitOfWork(context);
            _loggedInUserService = new LoggedInUserService(httpContextAccessor);
            _mapper = mapper;

            Auth = new Lazy<IAuthService>(() => new AuthService(userManager, _repositoryUnitOfWork, signInManager, _loggedInUserService, mapper));
            User = new Lazy<IUserService>(() => new UserService(userManager, _repositoryUnitOfWork, _loggedInUserService, _mapper));
            Excel = new Lazy<IExcelService>(() => new ExcelService(userManager, _hostingEnvironment , mapper));
        }

        public void Dispose() { }
    }
}
