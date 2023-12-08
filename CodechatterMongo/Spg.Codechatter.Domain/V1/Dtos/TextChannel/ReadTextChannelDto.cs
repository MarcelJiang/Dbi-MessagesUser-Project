namespace Spg.Codechatter.Domain.V1.Dtos.TextChannel;

public record ReadTextChannelDto(
    /// <summary>
    /// Gets or sets the unique identifier of the text channel.
    /// </summary>
    Guid Guid,
    /// <summary>
    /// Gets or sets the name of the text channel.
    /// </summary>
    string Name,
    /// <summary>
    /// Gets or sets the unique identifier of the chatroom the text channel belongs to.
    /// </summary>
    Guid ChatroomId);