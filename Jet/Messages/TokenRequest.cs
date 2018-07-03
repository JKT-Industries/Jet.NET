using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Jet.Messages
{
    public class TokenRequest : BaseRequest
    {
        [JsonProperty("user")]
        public string User { get; set; }
        
        [JsonProperty("pass")]
        public string Password { get; set; }
    }
}
