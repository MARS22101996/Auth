using System.Security.Claims;
using UserService.WEB.Models.AccountApiModels;

namespace UserService.WEB.Authentication.Interfaces
{
    public interface IIdentityProvider
    {
        ClaimsIdentity GetIdentity(LoginApiModel loginApiModel);
    }
}