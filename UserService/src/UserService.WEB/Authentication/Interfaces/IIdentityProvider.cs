using System.Collections.Generic;
using System.Security.Claims;
using UserService.WEB.Models.AccountApiModels;

namespace UserService.WEB.Authentication.Interfaces
{
    public interface IIdentityProvider
    {
		List<Claim> GetIdentity(LoginApiModel loginApiModel);
    }
}