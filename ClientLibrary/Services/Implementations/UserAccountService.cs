

using BaseLibrary.Class.DTOs;
using BaseLibrary.Class.Entities;
using BaseLibrary.Class.Response;
using ClientLibrary.Helpers;
using ClientLibrary.Services.Contracts;
using System.Net.Http.Json;
using System.Reflection;

namespace ClientLibrary.Services.Implementations
{
    public class UserAccountService(GetHttpClient _httpClient) : IUserAcountService
    {
        public const string AuthUrl = "api/authentication"; 

        public async Task<GeneralResponse> CreateAsync(Register user)
        {
          var httpClient = _httpClient.GetPublicHttpCLient();
            var result = await httpClient.PostAsJsonAsync($"{AuthUrl}/register", user);
            if (!result.IsSuccessStatusCode)
                return new GeneralResponse(false, "Error ocurred");

            return await result.Content.ReadFromJsonAsync<GeneralResponse>()!;
        }

        public async Task<GeneralResponse> DeleteUser(int userId)
        {
            var http = await _httpClient.GetPrivateHttpClient();
            var result = await http.DeleteAsync($"{AuthUrl}/delete-user/{userId}");
            if (!result.IsSuccessStatusCode) return new GeneralResponse(false, "Error occured");

            return await result.Content.ReadFromJsonAsync<GeneralResponse>()!;
        }

        public async Task<List<SystemRole>> GetRoles()
        {
            var http = await _httpClient.GetPrivateHttpClient();
            var result = await http.GetFromJsonAsync<List<SystemRole>>($"{AuthUrl}/roles");
            return result!;
        }

        public async Task<List<ManageUser>> GetUsers()
        {
            var http = await _httpClient.GetPrivateHttpClient();
            var result = await http.GetFromJsonAsync<List<ManageUser>>($"{AuthUrl}/users");
            return result!;
        }

        public async Task<LoginResponse> LoginAsync(Login user)
        {
            var httpClient = _httpClient.GetPublicHttpCLient();
            var result = await httpClient.PostAsJsonAsync($"{AuthUrl}/login", user);
            if (!result.IsSuccessStatusCode) return new LoginResponse(false, "Error ocurred");

            return await result.Content.ReadFromJsonAsync<LoginResponse>()!;
        }

        public async Task<LoginResponse> RefreshTokenAsync(RefreshToken token)
        {
            var httpClient = _httpClient.GetPublicHttpCLient();
            var result = await httpClient.PostAsJsonAsync($"{AuthUrl}/refresh-token", token);
            if (!result.IsSuccessStatusCode) 
            {
                return new LoginResponse(false, "Error occured");
            }
            return (await result.Content.ReadFromJsonAsync<LoginResponse>())!;
        }

        public async Task<GeneralResponse> UpdateUser(ManageUser user)
        {
            var http =  _httpClient.GetPublicHttpCLient();
            var result = await http.PutAsJsonAsync($"{AuthUrl}/update-user", user);
            if (!result.IsSuccessStatusCode) return new GeneralResponse(false, "Error occured");

            return await result.Content.ReadFromJsonAsync<GeneralResponse>();
        }

        public Task<string> GetUserImage(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateProfile(UserProfile userProfile)
        {
            var client = await _httpClient.GetPrivateHttpClient();
            var response = await client.PutAsJsonAsync($"{AuthUrl}/update-profile", userProfile);
             if (!response.IsSuccessStatusCode) { return false; }

            return await response.Content.ReadFromJsonAsync<bool>();

        }
    }
}
