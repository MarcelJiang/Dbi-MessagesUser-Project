using Microsoft.AspNetCore.Mvc;
using Spg.Codechatter.Application.V1.Interfaces.UserService;
using Spg.Codechatter.Domain.V1.Dtos.User;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Spg.Codechatter.API.Controllers.V1
{
    [ApiController]
    [Route("Api/v{version:apiVersion}/[controller]s")]
    [ApiVersion("1.0", Deprecated = true)]
    [AllowAnonymous]
    public class UserController : ControllerBase
    {
        private readonly IReadUserService _readUserService;
        private readonly IModifyUserService _modifyUserService;
        private readonly IConfiguration _configuration;

        public UserController(IReadUserService readUserService, IModifyUserService modifyUserService, IConfiguration configuration)
        {
            _readUserService = readUserService;
            _modifyUserService = modifyUserService;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                IEnumerable<ReadUserDto> result = _readUserService.GetAllUsers();

                if (!result.GetEnumerator().MoveNext())
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("{guid}")]
        public IActionResult GetById(Guid guid)
        {
            try
            {
                ReadUserDto result = _readUserService.GetUserById(guid);

                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] CreateUserDto user)
        {
            try
            {
                ReadUserDto result = _modifyUserService.AddUser(user);
                return Created($"{_configuration["PublicURL"]}api/v1/users/{result.Id}?api-version=1", result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                _modifyUserService.DeleteUser(guid);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] UpdateUserDto user)
        {
            try
            {
                _modifyUserService.UpdateUser(user);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
