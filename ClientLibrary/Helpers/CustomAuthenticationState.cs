using BaseLibrary.Class.DTOs;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ClientLibrary.Helpers
{
    public class CustomAuthenticationState : AuthenticationStateProvider
    {
        private readonly LocalStorageService _storageService;
        private readonly ClaimsPrincipal NormalClaim = new(new ClaimsIdentity());
        public CustomAuthenticationState(LocalStorageService storageService)
        {
            _storageService = storageService;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var stringToken = await _storageService.GetToken();
            if (string.IsNullOrEmpty(stringToken))
                return await Task.FromResult(new AuthenticationState(NormalClaim));

            var deserializeToken = Serializations.DeserializeJsonString<UserSession>(stringToken);
            if (deserializeToken == null)
                return await Task.FromResult(new AuthenticationState(NormalClaim));

            var userClaims = DecryptToken(deserializeToken.Token!);
            if (userClaims == null)
                return await Task.FromResult(new AuthenticationState(NormalClaim));

            var claimPrincipals = SetClaimPrincipals(userClaims);
            return await Task.FromResult(new AuthenticationState(claimPrincipals));
        }


        public async Task UpdateAuthenticationState(UserSession userSession) 
        {
            var claimPrincipals = new ClaimsPrincipal();
            if(userSession.Token != null || userSession.RefreshToken != null) 
            {
             var serializeSession = Serializations.SerializeObj(userSession);
                await _storageService.SetToken(serializeSession);
                var userClaims = DecryptToken(userSession.Token!);
                claimPrincipals = SetClaimPrincipals(userClaims);
            }
            else 
            {
                await _storageService.RemoveToken();
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimPrincipals)));
        
        }

        public static CustomUserClaims DecryptToken(string jwtToken) 
        {
            if (string.IsNullOrEmpty(jwtToken))
                return new CustomUserClaims();

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwtToken);

            var userId = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var name = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
            var email = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            var role = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

            return new CustomUserClaims(userId!.Value!, name!.Value, email!.Value, role!.Value);
        }

        public static ClaimsPrincipal SetClaimPrincipals(CustomUserClaims claims) 
        {
            if(claims.email is null)
                return new ClaimsPrincipal();

            return new ClaimsPrincipal(new ClaimsIdentity
                (
                 new List<Claim> 
                 {
                 new Claim(ClaimTypes.NameIdentifier,claims.id!),
                 new Claim(ClaimTypes.Name,claims.name!),
                 new Claim(ClaimTypes.Email,claims.email!),
                 new Claim(ClaimTypes.Role,claims.role!),
                 },
                 "JwtAuth"
                 ));
        }
    }
}
