namespace Spg.Codechatter.Domain.V1.Dtos.User;

public record ReadUserDto(
    Guid Guid,
    string EmailAddress,
    string Username);