using Microsoft.AspNetCore.Mvc;
using Spg.Codechatter.Application.V1.Interfaces.TextChannelService;
using Spg.Codechatter.Domain.V1.Dtos.TextChannel;
using System;
using System.Collections.Generic;

namespace Spg.Codechatter.API.Controllers.V1
{
    [ApiController]
    [Route("Api/v{version:apiVersion}/[controller]s")]
    [ApiVersion("1.0", Deprecated = true)]
    public class TextChannelController : ControllerBase
    {
        private readonly IReadTextChannelService _readTextChannelService;
        private readonly IModifyTextChannelService _modifyTextChannelService;
        private readonly IConfiguration _configuration;

        public TextChannelController(IReadTextChannelService readTextChannelService, IModifyTextChannelService modifyTextChannelService, IConfiguration configuration)
        {
            _readTextChannelService = readTextChannelService;
            _modifyTextChannelService = modifyTextChannelService;
            _configuration = configuration;
        }

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

        [HttpPost]
        public IActionResult Add([FromBody] CreateTextChannelDto textChannel)
        {
            try
            {
                ReadTextChannelDto result = _modifyTextChannelService.AddTextChannel(textChannel);
                return Created($"{_configuration["PublicURL"]}api/v1/textchannels/{result.Id}?api-version=1", result);
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
}
