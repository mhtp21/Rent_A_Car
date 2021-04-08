using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Jwt
{
    public class TokenOptions
    {
        public string Audience { get; set; }
        public string Issure { get; set; }
        public int AccessTokenExpiraton { get; set; }
        public string SecurityKey { get; set; }
    }
}
