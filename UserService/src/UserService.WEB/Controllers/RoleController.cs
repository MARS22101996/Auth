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
    [SwaggerResponse((int)HttpStatusCode.OK, Description = "Success")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, Description = "Unauthorized user")]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        private readonly ILogger<RoleController> _logger;

        public RoleController(IRoleService roleService, IMapper mapper, ILogger<RoleController> logger)
        {
            _roleService = roleService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Returns role with id
        /// </summary>
        /// <param name="id">Role id</param>
        [Route("roles/{id}")]
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(RoleApiModel), Description = "Role entity")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, typeof(JsonResult), Description = "Role with such id does not exist")]
        public async Task<IActionResult> Get(Guid id)
        {
            var roleDto = await _roleService.GetAsync(id);
            var roleApiModel = _mapper.Map<RoleApiModel>(roleDto);

            _logger.LogInformation($"Get role with id: {id}");

            return Json(roleApiModel);
        }

        /// <summary>
        /// Returns all roles
        /// </summary>
        [Route("roles")]
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(IEnumerable<RoleApiModel>), Description = "A list of roles entities")]
        public IActionResult Get()
        {
            var roleDtos = _roleService.GetAll();
            var roleApiModels = _mapper.Map<IEnumerable<RoleApiModel>>(roleDtos);

            _logger.LogInformation("Get all logs");

            return Json(roleApiModels);
        }

        /// <summary>
        /// Creates new role
        /// </summary>
        /// <param name="roleApiModel">Role model</param>
        [Route("roles")]
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Invalid model")]
        public async Task<IActionResult> Post([FromBody] RoleApiModel roleApiModel)
        {
            if (ModelState.IsValid)
            {
                await _roleService.CreateAsync(roleApiModel.Name);

                _logger.LogInformation($"New role {roleApiModel.Name} was created");

                return Ok();
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Deletes role with id 
        /// </summary>
        /// <param name="id">Role id</param>
        [Route("roles/{id}")]
        [HttpDelete]
        [SwaggerResponse((int)HttpStatusCode.NotFound, typeof(JsonResult), Description = "Role with such id does not exist")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _roleService.DeleteAsync(id);

            _logger.LogInformation($"Delete role with id: {id}");

            return Ok();
        }
    }
}