using System;
using System.Threading.Tasks;
using Jet.Messages;

namespace Jet
{
    public class Main
    {
        public void Log(string text)
        {
            Console.WriteLine(text);
        }

        private async Task<TokenResponse> RetrieveToken(string user, string password)
        {
            var endpoint = "token";
         
            var request = new TokenRequest
            {
                User = user,
                Password = password,
            };

            var response = await RestClient.Post<TokenRequest, TokenResponse>(request, endpoint).ConfigureAwait(false);            
            return response;
        }
    }
}