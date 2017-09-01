using System.Collections.Generic;
using System.Security.Claims;
using AuthHost.WEB.Models.AccountApiModels;

namespace AuthHost.WEB.Authentication.Interfaces
{
    public interface IIdentityProvider
    {
		List<Claim> GetIdentity(LoginApiModel loginApiModel);
    }
}