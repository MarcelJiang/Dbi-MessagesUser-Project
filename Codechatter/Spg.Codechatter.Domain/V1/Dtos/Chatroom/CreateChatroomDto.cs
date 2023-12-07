using System.ComponentModel.DataAnnotations;

namespace Spg.Codechatter.Domain.V1.Dtos.Chatroom;

public record CreateChatroomDto(
    /// <summary>
    /// Gets or sets the name of the chatroom.
    /// </summary>
    [Required()]
    [MaxLength(35, ErrorMessage = "Cannot exceed the character limitation of 35")]
    string Name);