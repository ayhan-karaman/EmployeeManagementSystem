using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BaseLibrary.DTOs;
using Microsoft.AspNetCore.Components.Authorization;

namespace ClientLibrary.Helpers
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private LocalStorageService _localStorageService;
        private readonly ClaimsPrincipal _anonymous;

        public CustomAuthenticationStateProvider(LocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
            _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var stringToken = await _localStorageService.GetToken();
            if(string.IsNullOrEmpty(stringToken)) return await Task.FromResult(new AuthenticationState(_anonymous));

            var deserializeToken = Serializations.DeserializeJsonString<UserSession>(stringToken);
            if(deserializeToken == null) return await Task.FromResult(new AuthenticationState(_anonymous));

            var getUserClaims = DecryptToken(deserializeToken.Token!);
            if(getUserClaims == null) return await Task.FromResult(new AuthenticationState(_anonymous));

            var claimsPrincipal = SetClaimPrincipal(getUserClaims);
            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }
        public async Task UpdateAuthenticationState(UserSession userSession)
        {
            var claimsPrincipal = new ClaimsPrincipal();
            if(userSession.Token != null || userSession.RefreshToken != null)
            {
                var serializeSession = Serializations.SerializaObj(userSession);
                await _localStorageService.SetToken(serializeSession);
                var getUserClaims = DecryptToken(userSession.Token!);
                claimsPrincipal  = SetClaimPrincipal(getUserClaims);
            }
            else
            {
                await _localStorageService.RemoveToken();
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
        private ClaimsPrincipal SetClaimPrincipal(CustomUserClaims claims)
        {
             if(claims.Email is null) return new ClaimsPrincipal();
             return new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
             {
                 new Claim(ClaimTypes.NameIdentifier, claims.Id!),
                 new Claim(ClaimTypes.Name, claims.Name!),
                 new Claim(ClaimTypes.Email, claims.Email!),
                 new Claim(ClaimTypes.Role, claims.Role!)
             }, "JwtAuth"));
        }

        private static CustomUserClaims DecryptToken(string jwtToken)
        {
            if(string.IsNullOrEmpty(jwtToken)) return new CustomUserClaims();
            var handler =  new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwtToken);
            var userId = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var name = token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
            var email = token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
            var role = token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);
            return new CustomUserClaims(userId!.Value, name!.Value, email!.Value, role!.Value);
        }
    }
}