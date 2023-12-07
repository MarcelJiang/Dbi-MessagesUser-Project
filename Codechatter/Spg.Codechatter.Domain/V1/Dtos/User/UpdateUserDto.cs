using System.ComponentModel.DataAnnotations;

namespace Spg.Codechatter.Domain.V1.Dtos.User;

public record UpdateUserDto(
    [Required]
    Guid Guid,
    [Required]
    [MaxLength(35, ErrorMessage = "Cannot exceed the character limitation of 35")]
    string EmailAddress,
    [Required]
    [MaxLength(35, ErrorMessage = "Cannot exceed the character limitation of 35")]
    string Username,
    [Required]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
    string Password);