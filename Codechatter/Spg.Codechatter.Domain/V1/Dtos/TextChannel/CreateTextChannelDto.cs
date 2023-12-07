using System.ComponentModel.DataAnnotations;

namespace Spg.Codechatter.Domain.V1.Dtos.TextChannel;

public record CreateTextChannelDto(
    /// <summary>
    /// Gets or sets the name of the text channel.
    /// </summary>
    [Required()]
    [MaxLength(35, ErrorMessage = "Cannot exceed the character limitation of 35")]
    string Name,
    /// <summary>
    /// Gets or sets the unique identifier of the chatroom the text channel belongs to.
    /// </summary>
    [Required()]
    Guid ChatroomId);