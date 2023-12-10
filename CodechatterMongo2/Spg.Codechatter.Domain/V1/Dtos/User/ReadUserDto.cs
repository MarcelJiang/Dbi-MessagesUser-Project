namespace Spg.Codechatter.Domain.V1.Dtos.User;

public record ReadUserDto(
    Guid Id,
    string EmailAddress,
    string Username);