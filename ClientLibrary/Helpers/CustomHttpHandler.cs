

using System.Net;
using System.Net.Http.Headers;
using BaseLibrary.DTOs;
using ClientLibrary.Services.Contracts;

namespace ClientLibrary.Helpers
{
    public class CustomHttpHandler:DelegatingHandler
    {
        private LocalStorageService _localStorageService;
        private GetHttpClient _getHttpClient;
        private readonly IUserAccountService _userAccountService;

        public CustomHttpHandler(LocalStorageService localStorageService, GetHttpClient getHttpClient, IUserAccountService userAccountService)
        {
            _localStorageService = localStorageService;
            _getHttpClient = getHttpClient;
            _userAccountService = userAccountService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            bool loginUrl = request.RequestUri!.AbsoluteUri.Contains("login");
            bool registerUrl = request.RequestUri!.AbsoluteUri.Contains("register");
            bool refreshTokenUrl = request.RequestUri!.AbsoluteUri.Contains("refresh-token");
            if (loginUrl || registerUrl || refreshTokenUrl)
                 return await base.SendAsync(request, cancellationToken);
            var result = await  base.SendAsync(request, cancellationToken);
            if(result.StatusCode == HttpStatusCode.Unauthorized)
            {
                 var stringToken = await _localStorageService.GetToken();
                 if(stringToken == null)
                    return result;
                string token = string.Empty;
                try
                {
                    token = request.Headers.Authorization!.Parameter!;
                }
                catch {}
                var deserializedToken = Serializations.DeserializeJsonString<UserSession>(stringToken);
                if(deserializedToken == null) return result;
                if(string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", deserializedToken.Token);
                    return await base.SendAsync(request, cancellationToken);
                }

                var newJwtToken = await GetRefreshToken(deserializedToken.RefreshToken!);
                if(string.IsNullOrEmpty(newJwtToken)) return result;
                
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newJwtToken);
                return await base.SendAsync(request, cancellationToken);
            }
            return result;
        }

        private async Task<string?> GetRefreshToken(string? refreshToken)
        {
            var result = await _userAccountService.RefreshTokenAsync(new RefreshToken(){Token = refreshToken});
            string serializedToken = Serializations.SerializaObj(new UserSession()
            {
                Token = result.Token,
                RefreshToken = result.RefreshToken
            });
            await _localStorageService.SetToken(serializedToken);
            // if(result == null) return null!;
            return result.Token;
        }
    }
}