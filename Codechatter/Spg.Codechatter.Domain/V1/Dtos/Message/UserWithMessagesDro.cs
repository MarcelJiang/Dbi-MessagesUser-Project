namespace Spg.Codechatter.Domain.V1.Dtos.Message;

public record UserWithMessagesDto
{
    public Guid UserId { get; init; }
    public string UserName { get; init; }
    public List<ReadMessageDto> Messages { get; init; }
}