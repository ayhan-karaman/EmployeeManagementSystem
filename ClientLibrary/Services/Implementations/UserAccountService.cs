
using System.Net.Http.Json;
using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using ClientLibrary.Helpers;
using ClientLibrary.Services.Contracts;

namespace ClientLibrary.Services.Implementations
{
    public class UserAccountService : IUserAccountService
    {
        private GetHttpClient _getHttpClient;
        public const string AuthUrl = "api/authentication";
        public UserAccountService(GetHttpClient getHttpClient)
        {
            _getHttpClient = getHttpClient;
        }

        public async Task<GeneralResponse> CreateAsync(Register user)
        {
            var httpClient = _getHttpClient.GetPublicHttpClient();
            var result = await httpClient.PostAsJsonAsync($"{AuthUrl}/register", user);
            if(!result.IsSuccessStatusCode) return new GeneralResponse(false, "Error occured");
            return await result.Content.ReadFromJsonAsync<GeneralResponse>()!;
        }
        
        public async Task<LoginResponse> SignInAsync(Login user)
        {
            var httpClient = _getHttpClient.GetPublicHttpClient();
            var result = await httpClient.PostAsJsonAsync($"{AuthUrl}/login", user);
            if(!result.IsSuccessStatusCode) return new  LoginResponse(false, "Error occured");
            return await result.Content.ReadFromJsonAsync<LoginResponse>()!;
        }
        public async Task<LoginResponse> RefreshTokenAsync(RefreshToken refreshToken)
        {
            var httpClient = _getHttpClient.GetPublicHttpClient();
            var result = await httpClient.PostAsJsonAsync($"{AuthUrl}/refresh-token", refreshToken);
            if(!result.IsSuccessStatusCode) new  LoginResponse(false, "");
            return await result.Content.ReadFromJsonAsync<LoginResponse>();
        }

        public async Task<ICollection<ManageUser>> GetUsersAsync()
        {
            var httpClient = await _getHttpClient.GetPrivateHttpClient();
            var result = await httpClient.GetFromJsonAsync<ICollection<ManageUser>>($"{AuthUrl}/users");
            return result!;
        }

        public async Task<GeneralResponse> UpdateUserAsync(ManageUser user)
        {
            var httpClient =  _getHttpClient.GetPublicHttpClient();
            var result = await httpClient.PutAsJsonAsync($"{AuthUrl}/update-user", user);
            if(!result.IsSuccessStatusCode) return new GeneralResponse(false, "Error occured");
            return await result.Content.ReadFromJsonAsync<GeneralResponse>();
        }

        public async Task<ICollection<SystemRole>> GetSystemRolesAsync()
        {
            var httpClient = await _getHttpClient.GetPrivateHttpClient();
            var result = await httpClient.GetFromJsonAsync<ICollection<SystemRole>>($"{AuthUrl}/roles");
            return result!;
        }

        public async Task<GeneralResponse> DeleteUserAsync(int id)
        {
            var httpClient = await _getHttpClient.GetPrivateHttpClient();
            var result = await httpClient.DeleteAsync($"{AuthUrl}/delete-user/{id}");
            if(!result.IsSuccessStatusCode) 
                return new GeneralResponse(false, "Error occured");
            return await result.Content.ReadFromJsonAsync<GeneralResponse>();
        }
    }
}