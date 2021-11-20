using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ApplicationUser, UserProfileRequestDTO>().ReverseMap();
            CreateMap<ApplicationUser, UserProfileResponseDTO>().ReverseMap();
            CreateMap<ApplicationUser, UserRequestDTO>().ReverseMap();
            CreateMap<ApplicationUser, UserResponseDTO>().ReverseMap();
        }
    }
}
