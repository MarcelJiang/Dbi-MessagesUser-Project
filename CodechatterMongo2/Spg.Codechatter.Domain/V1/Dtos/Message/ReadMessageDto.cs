namespace Spg.Codechatter.Domain.V1.Dtos.Message;

public record ReadMessageDto(
    Guid Id,
    string Content,
    DateTime DateAndTime,
    Guid TextChannelId,
    Guid UserId,
    Guid ChatroomId);