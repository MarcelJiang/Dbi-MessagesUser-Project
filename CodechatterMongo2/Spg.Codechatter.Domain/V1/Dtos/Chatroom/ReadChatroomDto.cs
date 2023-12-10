namespace Spg.Codechatter.Domain.V1.Dtos.Chatroom;

public record ReadChatroomDto(
    Guid Id, // oder Guid Guid, je nachdem, wie Sie die Id benennen wollen
    string Name);