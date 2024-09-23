using BaseLibrary.Class.DTOs;
using BaseLibrary.Class.Entities;
using BaseLibrary.Class.Response;

namespace ServerLibrary.Repositories.Contracts
{
    public interface IUserAccount
    {
        Task<GeneralResponse> CreateAsync(Register register);
        Task<LoginResponse> SignInAsync(Login login);
        Task<LoginResponse> RefreshTokenAsync(RefreshToken refreshToken);
        Task<List<ManageUser>> GetUsers();
        Task<GeneralResponse> UpdateUser(ManageUser user);
        Task<List<SystemRole>> GetRoles();
        Task<GeneralResponse> DeleteUser(int userId);
        Task<string> GetUserImage(int id);
        Task<bool> UpdateProfile(UserProfile userProfile);
    }
}
