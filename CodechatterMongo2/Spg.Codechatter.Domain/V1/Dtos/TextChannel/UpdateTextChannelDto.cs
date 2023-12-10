using System;
using System.ComponentModel.DataAnnotations;

namespace Spg.Codechatter.Domain.V1.Dtos.TextChannel
{
    public record UpdateTextChannelDto(
        [Required]
        Guid Id,
        [Required]
        [MaxLength(35, ErrorMessage = "Cannot exceed the character limitation of 35")]
        string Name,
        [Required]
        Guid ChatroomId);
}