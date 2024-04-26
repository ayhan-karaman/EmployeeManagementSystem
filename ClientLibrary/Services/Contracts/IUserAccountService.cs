
using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;

namespace ClientLibrary.Services.Contracts
{
    public interface IUserAccountService
    {
        Task<GeneralResponse> CreateAsync(Register user);
        Task<LoginResponse> SignInAsync(Login user);
        Task<LoginResponse> RefreshTokenAsync(RefreshToken refreshToken);
        Task<ICollection<ManageUser>> GetUsersAsync();
        Task<GeneralResponse> UpdateUserAsync(ManageUser user);
        Task<ICollection<SystemRole>> GetSystemRolesAsync();
        Task<GeneralResponse> DeleteUserAsync(int id);
        
    }
}