using Microsoft.AspNetCore.Mvc;
using Spg.Codechatter.Application.V1.Interfaces.ChatroomService;
using Spg.Codechatter.Domain.V1.Dtos.Chatroom;


namespace Spg.Codechatter.API.Controllers.V1;

[ApiController]
[Route("Api/v{version:apiVersion}/[controller]s")]
[ApiVersion("1.0", Deprecated = true)]
public class ChatroomController : ControllerBase
{
    private readonly IReadChatroomService _readChatroomService;
    private readonly IModifyChatroomService _modifyChatroomService;
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="ChatroomController"/> class.
    /// </summary>
    /// <param name="readChatroomService">The read chatroom service.</param>
    /// <param name="modifyChatroomService">The modify chatroom service.</param>
    /// <param name="configuration">The configuration object.</param>
    public ChatroomController(IReadChatroomService readChatroomService, IModifyChatroomService modifyChatroomService, IConfiguration configuration)
    {
        _readChatroomService = readChatroomService;
        _modifyChatroomService = modifyChatroomService;
        _configuration = configuration;
    }

    /// <summary>
    /// Gets all chatrooms.
    /// </summary>
    /// <returns>Returns the list of all chatrooms.</returns>
    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            IEnumerable<ReadChatroomDto> result = _readChatroomService.GetAllChatrooms();

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
    /// Gets a chatroom by its GUID.
    /// </summary>
    /// <param name="guid">The GUID of the chatroom.</param>
    /// <returns>Returns the chatroom with the specified GUID.</returns>
    [HttpGet("{guid}")]
    public IActionResult GetById(Guid guid)
    {
        try
        {
            ReadChatroomDto result = _readChatroomService.GetChatroomById(guid);

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
    /// Adds a new chatroom.
    /// </summary>
    /// <param name="chatroom">The chatroom to be added.</param>
    /// <returns>Returns the newly created chatroom.</returns>
    [HttpPost]
    public IActionResult Add([FromBody] CreateChatroomDto chatroom)
    {
        ReadChatroomDto result = _modifyChatroomService.AddChatroom(chatroom);
        return Created($"{_configuration["PublicURL"]}api/v1/chatrooms/{result.Guid}?api-version=1", result);
    }
    
    /// <summary>
    /// Deletes a chatroom by its GUID.
    /// </summary>
    /// <param name="guid">The GUID of the chatroom to delete.</param>
    /// <returns>Returns an HTTP status code indicating the success of the operation.</returns>
    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        try
        {
            _modifyChatroomService.DeleteChatroom(guid);
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
    /// Updates an existing chatroom.
    /// </summary>
    /// <param name="chatroom">The updated chatroom data.</param>
    /// <returns>Returns an HTTP status code indicating the success of the operation.</returns>
    [HttpPut]
    public IActionResult Update([FromBody] UpdateChatroomDto chatroom)
    {
        try
        {
            _modifyChatroomService.UpdateChatroom(chatroom);
            return Ok();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest();
        }
    }
}