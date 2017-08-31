using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.SwaggerGen;
using UserService.BLL.Interfaces;
using UserService.WEB.Models.AccountApiModels;

namespace UserService.WEB.Controllers
{
    [Route("UserService/api/")]
    [Authorize]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, Description = "Unauthorized user")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        public UserController(IMapper mapper, IUserService userService, ILogger<UserController> logger)
        {
            _mapper = mapper;
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Returns user's entity with id
        /// </summary>
        /// <param name="id">User's id</param>
        [Route("users/{id}")]
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(UserApiModel), Description = "User's entity")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, typeof(JsonResult), Description = "User with such id does not exist")]
        public async Task<IActionResult> Get(Guid id)
        {
            var userDto = await _userService.GetAsync(id);
            var userApiModel = _mapper.Map<UserApiModel>(userDto);

            _logger.LogInformation($"Get user with id: {id}");

            return Json(userApiModel);
        }

        /// <summary>
        /// Returns user's entity with email (or a list of all users when email is not defined)
        /// </summary>
        /// <param name="email">User's email(optional)</param>
        [Route("users")]
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(UserApiModel), Description = "User's entity (by default if email is null returns a list of all user's entities)")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, typeof(JsonResult), Description = "User with such id does not exist")]
        public IActionResult Get([FromQuery]string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                var userDto = _userService.Get(email);
                var userApiModel = _mapper.Map<UserApiModel>(userDto);

                _logger.LogInformation($"Get user by email: {email}");

                return Json(userApiModel);
            }

            var usersDto = _userService.GetAll();

            var users = _mapper.Map<List<UserApiModel>>(usersDto);

            _logger.LogInformation("Get all users");

            return Json(users);
        }

        /// <summary>
        /// Gets emails by key letters
        /// </summary>
        /// <param name="term">Key letters for searching</param>
        [Route("users/term/{term}")]
        [HttpGet]
        public JsonResult AutocompleteSearch(string term)
        {
            var model = _userService.GetEmailsForSearch(term);

            return Json(model);
        }

        /// <summary>
        /// Adds role for user
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="roleName">Role name</param>
        [Route("users/{userId}/roles/{roleName}")]
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(UserApiModel), Description = "User's entity")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, typeof(JsonResult), Description = "User with such id or role with such name does not exist")]
        public async Task<IActionResult> AddToRole(Guid userId, string roleName)
        {
            await _userService.AddToRoleAsync(userId, roleName);

            var userDto = await _userService.GetAsync(userId);
            var userApiModel = _mapper.Map<UserApiModel>(userDto);

            _logger.LogInformation($"Adding role {roleName} to user {userId}");

            return Ok(userApiModel);
        }

        /// <summary>
        /// Removes role from user
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="roleName">Role name</param>
        [Route("users/{userId}/roles/{roleName}")]
        [HttpDelete]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(UserApiModel), Description = "User's entity")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, typeof(JsonResult), Description = "User with such id does not exist or user isn't in specified role")]
        public async Task<IActionResult> RemoveFromRole(Guid userId, string roleName)
        {
            await _userService.RemoveRoleAsync(userId, roleName);

            var userDto = await _userService.GetAsync(userId);
            var userApiModel = _mapper.Map<UserApiModel>(userDto);

            _logger.LogInformation($"Remove role {roleName} from user {userId}");

            return Ok(userApiModel);
        }
    }
}