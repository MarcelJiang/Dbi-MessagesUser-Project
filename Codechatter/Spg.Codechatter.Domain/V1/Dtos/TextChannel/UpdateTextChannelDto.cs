using System.ComponentModel.DataAnnotations;

namespace Spg.Codechatter.Domain.V1.Dtos.TextChannel;

public record UpdateTextChannelDto(
    /// <summary>
    /// Gets or sets the unique identifier of the text channel.
    /// </summary>
    [Required()]
    Guid Guid,
    /// <summary>
    /// Gets or sets the updated name of the text channel.
    /// </summary>
    [Required()]
    [MaxLength(35, ErrorMessage = "Cannot exceed the character limitation of 35")]
    string Name,
    /// <summary>
    /// Gets or sets the unique identifier of the chatroom the text channel belongs to.
    /// </summary>
    [Required()]
    Guid ChatroomId);