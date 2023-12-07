namespace Spg.Codechatter.Domain.V1.Dtos.Message;

public record ReadMessageDto(
    /// <summary>
    /// Gets or sets the unique identifier of the message.
    /// </summary>
    Guid Guid, 
    /// <summary>
    /// Gets or sets the content of the message.
    /// </summary>
    string Content,
    /// <summary>
    /// Gets or sets the date and time when the message was sent.
    /// </summary>
    DateTime DateAndTime,
    /// <summary>
    /// Gets or sets the unique identifier of the text channel the message belongs to.
    /// </summary>
    Guid TextChannelId,
    /// <summary>
    /// Gets or sets the unique identifier of the user who sent the message.
    /// </summary>
    Guid UserId,
    /// <summary>
    /// Gets or sets the unique identifier of the chatroom the message belongs to.
    /// </summary>
    Guid ChatroomId);