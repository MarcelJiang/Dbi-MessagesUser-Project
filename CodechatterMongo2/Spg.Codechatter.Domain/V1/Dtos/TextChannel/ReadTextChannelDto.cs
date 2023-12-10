namespace Spg.Codechatter.Domain.V1.Dtos.TextChannel;

public record ReadTextChannelDto(
    Guid Id,
    string Name,
    Guid ChatroomId);