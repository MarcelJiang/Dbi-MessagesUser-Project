using Microsoft.AspNetCore.Mvc;
using Spg.Codechatter.Application.V1.Interfaces.MessageService;
using Spg.Codechatter.Domain.V1.Dtos.Message;

namespace Spg.Codechatter.API.Controllers.V1;

[ApiController]
[Route("Api/v{version:apiVersion}/[controller]s")]
[ApiVersion("1.0", Deprecated = true)]
public class MessageController : ControllerBase
{
    private readonly IReadMessageService _readMessageService;
    private readonly IModifyMessageService _modifyMessageService;
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="MessageController"/> class.
    /// </summary>
    /// <param name="readMessageService">The read message service.</param>
    /// <param name="modifyMessageService">The modify message service.</param>
    /// <param name="configuration">The configuration object.</param>
    public MessageController(IReadMessageService readMessageService, IModifyMessageService modifyMessageService, IConfiguration configuration)
    {
        _readMessageService = readMessageService;
        _modifyMessageService = modifyMessageService;
        _configuration = configuration;
    }

    /// <summary>
    /// Gets all messages.
    /// </summary>
    /// <returns>Returns the list of all messages.</returns>
    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            IEnumerable<ReadMessageDto> result = _readMessageService.GetAllMessages();

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
    /// Gets a message by GUID.
    /// </summary>
    /// <param name="guid">The GUID of the message.</param>
    /// <returns>Returns the message with the specified GUID.</returns>
    [HttpGet("{guid}")]
    public IActionResult GetById(Guid guid)
    {
        try
        {
            ReadMessageDto result = _readMessageService.GetMessageById(guid);

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
    /// Adds a new message.
    /// </summary>
    /// <param name="message">The message to be added.</param>
    /// <returns>Returns the newly added message.</returns>
    [HttpPost]
    public IActionResult Add([FromBody] CreateMessageDto message)
    {
        try
        {
            ReadMessageDto result = _modifyMessageService.AddMessage(message);
            return Created($"{_configuration["PublicURL"]}api/v1/messages/{result.Guid}?api-version=1", result);
        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }

    /// <summary>
    /// Deletes a message by GUID.
    /// </summary>
    /// <param name="guid">The GUID of the message.</param>
    /// <returns>Returns an HTTP status code indicating the success of the operation.</returns>
    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        try
        {
            _modifyMessageService.DeleteMessage(guid);
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
    /// Updates a message.
    /// </summary>
    /// <param name="message">The updated message object.</param>
    /// <returns>Returns an HTTP status code indicating the success of the operation.</returns>
    [HttpPut]
    public IActionResult Update([FromBody] UpdateMessageDto message)
    {
        try
        {
            _modifyMessageService.UpdateMessage(message);
            return Ok();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound();
            //return Add(message);
        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }
}