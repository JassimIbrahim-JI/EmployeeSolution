


using BaseLibrary.Class.DTOs;
using ClientLibrary.Services.Contracts;

namespace ClientLibrary.Helpers
{
    public class CustomHttpHandler(GetHttpClient _httpClient, LocalStorageService localStorage,IUserAcountService _userAccount ) : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            bool loginUrl = request.RequestUri!.AbsoluteUri.Contains("login");
            bool registerUrl = request.RequestUri.AbsoluteUri.Contains("register");
            bool refreshTokenUrl = request.RequestUri.AbsoluteUri.Contains("refresh-token");
            if (loginUrl || registerUrl || refreshTokenUrl) 
            {
              return await base.SendAsync(request, cancellationToken);
            }

            var result = await base.SendAsync(request, cancellationToken);
            if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized) 
            {
                //Get token from localStorage
                string stringToken = await localStorage.GetToken();
                if (stringToken == null) return result;
                
                //Check if the header contains token
                string token = string.Empty;
                try
                {
                    token = request.Headers.Authorization!.Parameter!;
                }
                catch 
                {
                }
                var deserializeToken = Serializations.DeserializeJsonString<UserSession>(stringToken);
                if (deserializeToken is null) return result;
                  
                if (string.IsNullOrEmpty(token)) 
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", deserializeToken.Token);
                    return await base.SendAsync(request, cancellationToken);
                }

                //Call for refresh-token
                string newJwtToken = await GetRefreshToken(deserializeToken.RefreshToken!);
                if (string.IsNullOrEmpty(newJwtToken)) 
                   return result;


                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", newJwtToken);
                return await base.SendAsync(request, cancellationToken);
            }
            return result;
        }

        private async Task<string> GetRefreshToken(string refreshToken) 
        {
            var result = await _userAccount.RefreshTokenAsync(new RefreshToken() { Token = refreshToken });
            string serializedToken = Serializations.SerializeObj(new UserSession() { Token = result.Token, RefreshToken = result.RefreshToken });
            await localStorage.SetToken(serializedToken);
            return result.Token; 

        }

    }
}
