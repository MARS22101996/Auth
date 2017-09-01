using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthHost.WEB.Authentication.Interfaces;
using AuthHost.WEB.Models.AccountApiModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AuthHost.WEB.Authentication.Middlewares
{
    public class TokenProviderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TokenProviderOptions _options;
        private readonly IIdentityProvider _identityProvider;

        public TokenProviderMiddleware(
            RequestDelegate next,
            IOptions<TokenProviderOptions> options,
            IIdentityProvider identityProvider)
        {
            _next = next;
            _options = options.Value;
            _identityProvider = identityProvider;
        }

        public Task Invoke(HttpContext context)
        {
            string response;

            if (!context.Request.Path.HasValue ||
                !context.Request.Path.Value.TrimEnd('/').Equals(_options.Path, StringComparison.Ordinal))
            {
                return _next(context);
            }

            if (!context.Request.Method.Equals("POST"))
            {
                context.Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                response = "Wrong HTTP method.";
            }
            else if (!context.Request.HasFormContentType)
            {
                context.Response.StatusCode = (int)HttpStatusCode.UnsupportedMediaType;
                response = "Unsupported Media Type. Required: x-www-form-urlencoded";
            }
            else
            {
                var token = GenerateToken(context);

                if (token == null)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response = "Invalid username or password.";
                }
                else
                {
                    context.Response.ContentType = "application/json";
                    response = JsonConvert
                        .SerializeObject(token, new JsonSerializerSettings { Formatting = Formatting.Indented });
                }
            }

            return context.Response.WriteAsync(response);
        }

        private TokenApiModel GenerateToken(HttpContext context)
        {
            var username = context.Request.Form["username"];
            var password = context.Request.Form["password"];
            var now = DateTime.UtcNow;

            var identity = _identityProvider.GetIdentity(new LoginApiModel { Email = username, Password = password });

            if (identity == null)
            {
                return null;
            }

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().Second.ToString(), ClaimValueTypes.Integer64)
            };

            claims.AddRange(identity);


            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(_options.Expiration),
                signingCredentials: _options.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var token = new TokenApiModel
            {
                Token = encodedJwt,
                ExpiresIn = (long)_options.Expiration.TotalSeconds
            };

            return token;
        }
    }
}