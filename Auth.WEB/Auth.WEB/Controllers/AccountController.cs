using System;
using System.Threading.Tasks;
using Auth.WEB.Models;
using Auth.WEB.RequestSettings.Inerfaces;
using Auth.WEB.ViewModels.AccountViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.WEB.Controllers
{
    public class AccountController : BaseController
    {
		private readonly IRequestService _communicationService;

		private const string CookieTokenKeyName = "token";

		public AccountController(IRequestService service)
		{
			_communicationService = service;
		}


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await SetTokenCookie(model.Email, model.Password);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _communicationService.PostAsync("api/register", model, FormHeaders(JsonType));

            await SetTokenCookie(model.Email, model.Password);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public IActionResult LogOff()
        {
            Response.Cookies.Delete(CookieTokenKeyName);

            return RedirectToAction("Index", "Home");
        }

        private async Task SetTokenCookie(string email, string password)
        {
			var body = $"username={email}&password={password}";

			var token = await _communicationService.PostAsync<TokenApiModel, string>("token", body, FormHeaders(FormType));

			Response.Cookies.Append(CookieTokenKeyName, token.Token, new CookieOptions
			{
				Expires = DateTimeOffset.UtcNow + TimeSpan.FromSeconds(token.ExpiresIn)
			});
			
		}
    }
}