using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Jet.Messages;

namespace Jet
{
    public static class Main
    {
        public static async Task<TokenResponse> RetrieveToken(string user, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(user))
                {
                    throw new Exception("user is required.");
                }

                if (string.IsNullOrWhiteSpace(password))
                {
                    throw new Exception("password is required.");
                }
                
                var request = new TokenRequest
                {
                    User = user,
                    Password = password,
                };

                var response = await RestClient.Post<TokenRequest, TokenResponse>(request, "token").ConfigureAwait(false);            
                return response;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Debugger.Break();
                return new TokenResponse(ex);
            }
        }
    }
}