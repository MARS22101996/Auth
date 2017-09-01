using System;
using Microsoft.IdentityModel.Tokens;

namespace AuthHost.WEB.Authentication
{
    public class TokenProviderOptions
    {
        public TokenProviderOptions()
        {
            Path = "/UserService/token";
            Expiration = TimeSpan.FromMinutes(30);
        }

        public string SecretKey { get; set; }

        public string Path { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public TimeSpan Expiration { get; set; }

        public SigningCredentials SigningCredentials { get; set; }
    }
}