using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDTO> UpdateUserInfo(UserRequestDTO userInfo);
        Task<bool> RemoveUser(int userId);
        Task<List<UserResponseDTO>> GetUsersList(UserSearchModel search);
        Task<UserResponseDTO> GetUserInfo(int userId);
        Task<UserProfileResponseDTO> GetUserProfile();
        Task<UserProfileResponseDTO> UpdateUserProfile(UserProfileRequestDTO userProfile);
        Task<ChartDataDTO> GetChartData();
    }
}
