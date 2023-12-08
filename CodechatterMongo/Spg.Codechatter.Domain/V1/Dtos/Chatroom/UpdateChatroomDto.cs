using System.ComponentModel.DataAnnotations;

namespace Spg.Codechatter.Domain.V1.Dtos.Chatroom;

public record UpdateChatroomDto(
    /// <summary>
    /// Gets or sets the unique identifier of the chatroom.
    /// </summary>
    [Required()]
    Guid Guid,
    /// <summary>
    /// Gets or sets the updated name of the chatroom.
    /// </summary>
    [Required()]
    [MaxLength(35, ErrorMessage = "Cannot exceed the character limitation of 35")]
    string Name);