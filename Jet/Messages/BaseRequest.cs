using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Jet.Messages
{
    public abstract class BaseRequest
    {
        [JsonIgnore]
        public virtual string MediaContentType => "application/json";

        [JsonIgnore]
        public virtual Encoding Encoding => Encoding.ASCII;
    }
}
