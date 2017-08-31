using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using AutoMapper;
using UserService.BLL.DTO;
using UserService.BLL.Infrastructure.Exceptions;
using UserService.BLL.Interfaces;
using UserService.WEB.Authentication.Interfaces;
using UserService.WEB.Models.AccountApiModels;
using System.Collections.Generic;

namespace UserService.WEB.Authentication
{
    public class IdentityProvider : IIdentityProvider
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public IdentityProvider(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

	    public List<Claim> GetIdentity(LoginApiModel loginApiModel)
        {
            var loginModelDto = _mapper.Map<LoginModelDto>(loginApiModel);
            UserDto userDto;

            try
            {
                userDto = _accountService.Login(loginModelDto);
            }
            catch (ServiceException)
            {
                return null;
            }

			var role = userDto.Roles.FirstOrDefault();

			var claims = new List<Claim>();

			claims.Add(new Claim(ClaimTypes.Role, role));

			claims.Add(new Claim(ClaimTypes.Email, loginApiModel.Email));

			claims.Add(new Claim("userId", userDto.Id.ToString()));

			return claims;
        }
    }
}