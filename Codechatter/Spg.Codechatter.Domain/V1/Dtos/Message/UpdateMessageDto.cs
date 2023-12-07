using System.ComponentModel.DataAnnotations;

namespace Spg.Codechatter.Domain.V1.Dtos.Message;

public record UpdateMessageDto(
    /// <summary>
    /// Gets or sets the unique identifier of the message.
    /// </summary>
    [Required()]
    Guid Guid,
    /// <summary>
    /// Gets or sets the updated content of the message.
    /// </summary>
    [Required()]
    [MaxLength(200, ErrorMessage = "Cannot exceed message character limit of 200")]
    string Content, 
    /// <summary>
    /// Gets or sets the unique identifier of the text channel the message belongs to.
    /// </summary>
    [Required()]
    Guid TextChannelId,
    /// <summary>
    /// Gets or sets the unique identifier of the user who sent the message.
    /// </summary>
    [Required()]
    Guid UserId,
    /// <summary>
    /// Gets or sets the unique identifier of the chatroom the message belongs to.
    /// </summary>
    Guid ChatroomId);