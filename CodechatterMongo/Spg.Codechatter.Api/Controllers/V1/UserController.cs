using Microsoft.AspNetCore.Mvc;
using Spg.Codechatter.Application.V1.Interfaces.UserService;
using Spg.Codechatter.Domain.V1.Dtos.User;

namespace Spg.Codechatter.API.Controllers.V1;

[ApiController]
[Route("Api/v{version:apiVersion}/[controller]s")]
[ApiVersion("1.0", Deprecated = true)]
public class UserController : ControllerBase
{
    private readonly IReadUserService _readUserService;
    private readonly IModifyUserService _modifyUserService;
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserController"/> class.
    /// </summary>
    /// <param name="readUserService">The read user service.</param>
    /// <param name="modifyUserService">The modify user service.</param>
    /// <param name="configuration">The configuration object.</param>
    public UserController(IReadUserService readUserService, IModifyUserService modifyUserService, IConfiguration configuration)
    {
        _readUserService = readUserService;
        _modifyUserService = modifyUserService;
        _configuration = configuration;
    }

    /// <summary>
    /// Gets all users.
    /// </summary>
    /// <returns>Returns the list of all users.</returns>
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

    /// <summary>
    /// Gets a user by ID.
    /// </summary>
    /// <param name="guid">The ID of the user.</param>
    /// <returns>Returns the user with the specified ID.</returns>
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

    /// <summary>
    /// Adds a new user.
    /// </summary>
    /// <param name="user">The user to be added.</param>
    /// <returns>Returns the newly added user.</returns>
    [HttpPost]
    public IActionResult Add([FromBody] CreateUserDto user)
    {
        try
        {
            ReadUserDto result = _modifyUserService.AddUser(user);
            return Created($"{_configuration["PublicURL"]}api/v1/users/{result.Guid}?api-version=1", result);
        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }

    /// <summary>
    /// Deletes a user by ID.
    /// </summary>
    /// <param name="guid">The ID of the user.</param>
    /// <returns>Returns an HTTP status code indicating the success of the operation.</returns>
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

    /// <summary>
    /// Updates a user.
    /// </summary>
    /// <param name="user">The user to be updated.</param>
    /// <returns>Returns an HTTP status code indicating the success of the operation.</returns>
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