using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Jet.Messages
{
    public class TokenResponse : BaseResponse
    {
        public TokenResponse(Exception ex, string message = "") : base(ex, message)
        {
            
        }

        [JsonProperty("id_token")]
        public string IdToken { get; set; }
        
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        
        [JsonProperty("expires_on")]
        public string ExpiresOn { get; set; }
    }
}
