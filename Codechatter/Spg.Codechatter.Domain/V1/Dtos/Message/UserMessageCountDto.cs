namespace Spg.Codechatter.Domain.V1.Dtos.Message;

public record UserMessageCountDto
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public long MessageCount { get; set; }
}