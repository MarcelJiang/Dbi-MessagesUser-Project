namespace Spg.Codechatter.Domain.V1.Dtos.Chatroom;

public record ReadChatroomDto(
    /// <summary>
    /// Gets or sets the unique identifier of the chatroom.
    /// </summary>
    Guid Guid,
    /// <summary>
    /// Gets or sets the name of the chatroom.
    /// </summary>
    string Name);