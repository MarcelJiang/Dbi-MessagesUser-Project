using Microsoft.AspNetCore.Mvc;
using Spg.Codechatter.Application.V1.Interfaces.MessageService;
using Spg.Codechatter.Domain.V1.Dtos.Message;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Spg.Codechatter.API.Controllers.V1
{
    [ApiController]
    [Route("Api/v{version:apiVersion}/[controller]s")]
    [ApiVersion("1.0", Deprecated = true)]
    [AllowAnonymous]
    public class MessageController : ControllerBase
    {
        private readonly IReadMessageService _readMessageService;
        private readonly IModifyMessageService _modifyMessageService;
        private readonly IConfiguration _configuration;

        public MessageController(IReadMessageService readMessageService, IModifyMessageService modifyMessageService, IConfiguration configuration)
        {
            _readMessageService = readMessageService;
            _modifyMessageService = modifyMessageService;
            _configuration = configuration;
        }

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

        [HttpGet("all-with-messages")]
        public IActionResult GetWithMessages()
        {
            try
            {
                var usersWithMessages = _readMessageService.GetAllUsersWithMessages();
                return Ok(usersWithMessages);
            }
            catch (Exception ex)
            {
                // Log the exception and return an error response
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        
        
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

        [HttpPost]
        public IActionResult Add([FromBody] CreateMessageDto message)
        {
            try
            {
                ReadMessageDto result = _modifyMessageService.AddMessage(message);
                return Created($"{_configuration["PublicURL"]}api/v1/messages/{result.Id}?api-version=1", result);
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
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        
        
    }
}
