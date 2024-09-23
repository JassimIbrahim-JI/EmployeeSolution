
using BaseLibrary.Class.DTOs;
using BaseLibrary.Class.Entities;
using BaseLibrary.Class.Response;

namespace ClientLibrary.Services.Contracts
{
    public interface IUserAcountService
    {
        Task<GeneralResponse> CreateAsync(Register user);
        Task<LoginResponse> LoginAsync(Login user);
        Task<LoginResponse> RefreshTokenAsync(RefreshToken token);
        Task<List<ManageUser>> GetUsers();
        Task<GeneralResponse>UpdateUser(ManageUser user);
        Task<List<SystemRole>>GetRoles();
        Task<GeneralResponse> DeleteUser(int userId);
        Task<string> GetUserImage(int id);
        Task<bool> UpdateProfile(UserProfile userProfile);

    }
}
