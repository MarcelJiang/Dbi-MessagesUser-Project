using Microsoft.AspNetCore.Mvc;
using Spg.Codechatter.Application.V1.Interfaces.ChatroomService;
using Spg.Codechatter.Domain.V1.Dtos.Chatroom;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Spg.Codechatter.API.Controllers.V1
{
    [ApiController]
    [Route("Api/v{version:apiVersion}/[controller]s")]
    [ApiVersion("1.0", Deprecated = true)]
    [AllowAnonymous]
    public class ChatroomController : ControllerBase
    {
        private readonly IReadChatroomService _readChatroomService;
        private readonly IModifyChatroomService _modifyChatroomService;
        private readonly IConfiguration _configuration;

        public ChatroomController(IReadChatroomService readChatroomService, IModifyChatroomService modifyChatroomService, IConfiguration configuration)
        {
            _readChatroomService = readChatroomService;
            _modifyChatroomService = modifyChatroomService;
            _configuration = configuration;
        }

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

        [HttpPost]
        public IActionResult Add([FromBody] CreateChatroomDto chatroom)
        {
            ReadChatroomDto result = _modifyChatroomService.AddChatroom(chatroom);
            return Created($"{_configuration["PublicURL"]}api/v1/chatrooms/{result.Id}?api-version=1", result);
        }

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
}
