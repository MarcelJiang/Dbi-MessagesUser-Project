using Microsoft.AspNetCore.Mvc;
using Spg.Codechatter.Application.V1.Interfaces.TextChannelService;
using Spg.Codechatter.Domain.V1.Dtos.TextChannel;

namespace Spg.Codechatter.API.Controllers.V1;

[ApiController]
[Route("Api/v{version:apiVersion}/[controller]s")]
[ApiVersion("1.0", Deprecated = true)]
public class TextChannelController : ControllerBase
{
    private readonly IReadTextChannelService _readTextChannelService;
    private readonly IModifyTextChannelService _modifyTextChannelService;
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="TextChannelController"/> class.
    /// </summary>
    /// <param name="readTextChannelService">The read text channel service.</param>
    /// <param name="modifyTextChannelService">The modify text channel service.</param>
    /// <param name="configuration">The configuration object.</param>
    public TextChannelController(IReadTextChannelService readTextChannelService, IModifyTextChannelService modifyTextChannelService, IConfiguration configuration)
    {
        _readTextChannelService = readTextChannelService;
        _modifyTextChannelService = modifyTextChannelService;
        _configuration = configuration;
    }

    /// <summary>
    /// Gets all text channels.
    /// </summary>
    /// <returns>Returns the list of all text channels.</returns>
    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            IEnumerable<ReadTextChannelDto> result = _readTextChannelService.GetAllTextChannels();

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
    /// Gets a text channel by ID.
    /// </summary>
    /// <param name="guid">The ID of the text channel.</param>
    /// <returns>Returns the text channel with the specified ID.</returns>
    [HttpGet("{guid}")]
    public IActionResult GetById(Guid guid)
    {
        try
        {
            ReadTextChannelDto result = _readTextChannelService.GetTextChannelById(guid);

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
    /// Adds a new text channel.
    /// </summary>
    /// <param name="textChannel">The text channel to be added.</param>
    /// <returns>Returns the newly added text channel.</returns>
    [HttpPost]
    public IActionResult Add([FromBody] CreateTextChannelDto textChannel)
    {
        try
        {
            ReadTextChannelDto result = _modifyTextChannelService.AddTextChannel(textChannel);
            return Created($"{_configuration["PublicURL"]}api/v1/textchannels/{result.Guid}?api-version=1", result);
        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }

    /// <summary>
    /// Deletes a text channel by ID.
    /// </summary>
    /// <param name="guid">The ID of the text channel.</param>
    /// <returns>Returns an HTTP status code indicating the success of the operation.</returns>
    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        try
        {
            _modifyTextChannelService.DeleteTextChannel(guid);
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
    /// Updates a text channel.
    /// </summary>
    /// <param name="textChannel">The text channel to be updated.</param>
    /// <returns>Returns an HTTP status code indicating the success of the operation.</returns>
    [HttpPut]
    public IActionResult Update([FromBody] UpdateTextChannelDto textChannel)
    {
        try
        {
            _modifyTextChannelService.UpdateTextChannel(textChannel);
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