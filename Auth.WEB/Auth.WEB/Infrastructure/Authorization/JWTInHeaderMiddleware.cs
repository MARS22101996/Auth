using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.WEB.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Auth.WEB.Infrastructure.Authorization
{
	public class JWTInHeaderMiddleware
	{
		private readonly RequestDelegate _next;

		public JWTInHeaderMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			var authenticationCookieName = "access_token";
			var cookie = context.Request.Cookies[authenticationCookieName];
			if (cookie != null)
			{
				var token = JsonConvert.DeserializeObject<TokenApiModel>(cookie);
				context.Request.Headers.Append("Authorization", "Bearer " + token.Token);
			}

			await _next.Invoke(context);
		}
	}
}
