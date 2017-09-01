using System.Threading.Tasks;
using AuthHost.BLL.DTO;
using AuthHost.BLL.Interfaces;
using AuthHost.WEB.Models.AccountApiModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AuthHost.WEB.Controllers
{
	[Route("UserService/api/")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountService accountService, IMapper mapper, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _mapper = mapper;
            _logger = logger;
        }

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterApiModel model)
        {
            if (ModelState.IsValid)
            {
                var registerModelDto = _mapper.Map<RegisterModelDto>(model);
                await _accountService.RegisterAsync(registerModelDto);

                _logger.LogInformation($"User was registration with email: {model.Email}");

                return Ok();
            }

            return BadRequest(ModelState);
        }
    }
}