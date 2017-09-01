using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AuthHost.BLL.DTO;
using AuthHost.BLL.Infrastructure.Exceptions;
using AuthHost.BLL.Interfaces;
using AuthHost.WEB.Authentication.Interfaces;
using AuthHost.WEB.Models.AccountApiModels;
using AutoMapper;

namespace AuthHost.WEB.Authentication
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

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.Email, loginApiModel.Email),
                new Claim("userId", userDto.Id.ToString())
            };

            return claims;
        }
    }
}