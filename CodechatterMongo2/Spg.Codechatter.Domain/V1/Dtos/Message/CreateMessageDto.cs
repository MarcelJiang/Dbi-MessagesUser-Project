using System.ComponentModel.DataAnnotations;

namespace Spg.Codechatter.Domain.V1.Dtos.Message;

public record CreateMessageDto(
    [Required]
    [MaxLength(200, ErrorMessage = "Cannot exceed message character limit of 200")]
    string Content,
    [Required]
    Guid TextChannelId,
    [Required]
    Guid UserId,
    Guid ChatroomId);