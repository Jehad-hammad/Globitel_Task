using AutoMapper;
using Domain.DTO;
using Domain.Enums;
using Domain.Models;
using Domain.Models.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Repository.UnitOfWork;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;



namespace Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly LoggedInUserService _loggedInUserService;
        private IRepositoryUnitOfWork _repositoryUnitOfWork;
        private IMapper _mapper;
        AppConfiguration _appConfiguration = new AppConfiguration();

        public AuthService(UserManager<ApplicationUser> userManager,
            IRepositoryUnitOfWork repositoryUnitOfWork,
            SignInManager<ApplicationUser> signInManager,
            LoggedInUserService loggedInUserService,
            IMapper mapper)
        {
            _loggedInUserService = loggedInUserService;
            _userManager = userManager;
            _signInManager = signInManager;
            _repositoryUnitOfWork = repositoryUnitOfWork;
            _mapper = mapper;

        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO model)
        {
            ApplicationUser user = _userManager.Users.FirstOrDefault(x => x.UserName == model.Username);

            if (user != null)
            {
                SignInResult oSignInResult = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

                if (oSignInResult.Succeeded)
                {
                    IList<string> roles = await _userManager.GetRolesAsync(user);
                    IList<Claim> claims = await BuildClaims(user);

                    LoginResponseDTO oLoginResponseDTO = new LoginResponseDTO()
                    {
                        AccessToken = WriteToken(claims),
                        UserId = user.Id.ToString(),
                        UserName = user.UserName,
                        FullName = user.FullName,
                        PhoneNumber = user.PhoneNumber,
                        Email = user.EmailAddress,
                        Roles = roles
                    };

                    return oLoginResponseDTO;
                }
                else
                {
                    throw new ValidationException("Password Is Wrong");
                }
            }
            else
            {
                throw new ValidationException("UserName is Wrong");
            }
        }

        public async Task<UserResponseDTO> Register(RegisterDTO model)
        {
            
                if (_userManager.Users.Any(x => x.UserName == model.UserName))
                {
                    throw new ValidationException("UserName Already Exists");
                }

                ApplicationUser user = new ApplicationUser
                {
                    UserName = model.UserName,
                    FullName = model.FullName,
                    EmailAddress = model.EmailAddress,
                    UserStatus = UserStatus.Active.GetHashCode(),
                    PhoneNumber = model.PhoneNumber,
                    CreatedDate = DateTime.Now,
                    Age = model.Age,
                    Gender = model.Gender
                };

                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    _repositoryUnitOfWork.UserRole.Value.Add(new UserRole
                    {
                        RoleId = RoleTypes.Client.GetHashCode(),
                        UserId = user.Id,
                    });
                    return _mapper.Map<UserResponseDTO>(user);
                }
                else
                {
                    throw new ValidationException("Error While Creating User");
                }
        }

        private async Task<IList<Claim>> BuildClaims(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id > 0 ? user.Id.ToString() : string.Empty),
                new Claim(ClaimTypes.Name, !string.IsNullOrEmpty(user.FullName) ? user.FullName : ""),
                new Claim(ClaimTypes.MobilePhone, !string.IsNullOrEmpty(user.PhoneNumber) ? user.PhoneNumber : ""),
            };
            foreach (var item in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }

            return claims;
        }

        private string WriteToken(IList<Claim> claims)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appConfiguration.JWTKey));

            JwtSecurityToken jwtToken = new JwtSecurityToken(
                    issuer: _appConfiguration.Issuer,
                    audience: _appConfiguration.Audience,
                    claims: claims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddYears(100),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return token;
        }

    }
}
