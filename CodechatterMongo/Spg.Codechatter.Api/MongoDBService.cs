using System;
using Spg.Codechatter.API;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Spg.Codechatter.Application.V1.Interfaces.ChatroomService;
using Spg.Codechatter.Application.V1.Interfaces.MessageService;
using Spg.Codechatter.Application.V1.Interfaces.TextChannelService;
using Spg.Codechatter.Application.V1.Interfaces.UserService;
using Spg.Codechatter.Domain.V1.Dtos.Chatroom;
using Spg.Codechatter.Domain.V1.Dtos.Message;
using Spg.Codechatter.Domain.V1.Dtos.TextChannel;
using Spg.Codechatter.Domain.V1.Dtos.User;
using Spg.Codechatter.Domain.V1.Model;
using Spg.Codechatter.Repository.V1.Interfaces.ChatroomRepository;
using Spg.Codechatter.Repository.V1.Interfaces.MessageRepository;
using Spg.Codechatter.Repository.V1.Interfaces.TextChannelRepository;
using Spg.Codechatter.Repository.V1.Interfaces.UserRepository;


namespace Spg.Codechatter.API
{
    public class MongoDbService : IReadChatroomService, IModifyChatroomService,
    IReadMessageService, IModifyMessageService,
    IReadTextChannelService, IModifyTextChannelService,
    IReadUserService, IModifyUserService
{
    private readonly IMongoClient _mongoClient;
    private readonly IMongoDatabase _database;

    public MongoDbService(IOptions<Settings> options)
    {
        var clientSettings = MongoClientSettings.FromConnectionString(options.Value.MongoDB.ConnectionString);
        _mongoClient = new MongoClient(clientSettings);
        _database = _mongoClient.GetDatabase(options.Value.MongoDB.Database);
    }

    // Chatrooms
    public IEnumerable<ReadChatroomDto> GetAllChatrooms()
    {
        var chatroomCollection = _database.GetCollection<Chatroom>("Chatrooms");
        return chatroomCollection.AsQueryable().Select(c => new ReadChatroomDto(c.Guid, c.Name));
    }

    public ReadChatroomDto GetChatroomById(Guid id)
    {
        var chatroomCollection = _database.GetCollection<Chatroom>("Chatrooms");
        var chatroom = chatroomCollection.AsQueryable().FirstOrDefault(c => c.Guid == id);
        return new ReadChatroomDto(chatroom.Guid, chatroom.Name);
    }

    public ReadChatroomDto AddChatroom(CreateChatroomDto chatroom)
    {
        var chatroomCollection = _database.GetCollection<Chatroom>("Chatrooms");
        var c = new Chatroom(chatroom.Name);
        chatroomCollection.InsertOne(c);
        return new ReadChatroomDto(c.Guid, c.Name);
    }

    public void UpdateChatroom(UpdateChatroomDto chatroom)
    {
        var chatroomCollection = _database.GetCollection<Chatroom>("Chatrooms");
        var c = new Chatroom(chatroom.Name);
        chatroomCollection.ReplaceOne(cr => cr.Guid == chatroom.Guid, c);
    }

    public void DeleteChatroom(Guid guid)
    {
        var chatroomCollection = _database.GetCollection<Chatroom>("Chatrooms");
        chatroomCollection.DeleteOne(cr => cr.Guid == guid);
    }

    // TextChannels
    public IEnumerable<ReadTextChannelDto> GetAllTextChannels()
    {
        var textChannelCollection = _database.GetCollection<TextChannel>("TextChannels");
        return textChannelCollection.AsQueryable().Select(t => new ReadTextChannelDto(t.Guid, t.Name, t.ChatroomId));
    }

    public ReadTextChannelDto GetTextChannelById(Guid id)
    {
        var textChannelCollection = _database.GetCollection<TextChannel>("TextChannels");
        var textChannel = textChannelCollection.AsQueryable().FirstOrDefault(t => t.Guid == id);
        return new ReadTextChannelDto(textChannel.Guid, textChannel.Name, textChannel.ChatroomId);
    }

    public IEnumerable<ReadTextChannelDto> GetTextChannelsByChatroomId(Guid id)
    {
        var textChannelCollection = _database.GetCollection<TextChannel>("TextChannels");
        return textChannelCollection.AsQueryable().Where(t => t.ChatroomId == id).Select(t => new ReadTextChannelDto(t.Guid, t.Name, t.ChatroomId));
    }

    public ReadTextChannelDto AddTextChannel(CreateTextChannelDto textChannel)
    {
        var textChannelCollection = _database.GetCollection<TextChannel>("TextChannels");
        var t = new TextChannel(textChannel.Name, textChannel.ChatroomId);
        textChannelCollection.InsertOne(t);
        return new ReadTextChannelDto(t.Guid, t.Name, t.ChatroomId);
    }

    public void UpdateTextChannel(UpdateTextChannelDto textChannel)
    {
        var textChannelCollection = _database.GetCollection<TextChannel>("TextChannels");
        var t = new TextChannel(textChannel.Name, textChannel.ChatroomId);
        textChannelCollection.ReplaceOne(tc => tc.Guid == textChannel.Guid, t);
    }

    public void DeleteTextChannel(Guid id)
    {
        var textChannelCollection = _database.GetCollection<TextChannel>("TextChannels");
        textChannelCollection.DeleteOne(tc => tc.Guid == id);
    }

    // Messages
    public IEnumerable<ReadMessageDto> GetAllMessages()
    {
        var messageCollection = _database.GetCollection<Message>("Messages");
        return messageCollection.AsQueryable().Select(m => new ReadMessageDto(m.Guid, m.Content, m.DateAndTime, m.TextChannelId, m.UserId, m.ChatroomId));
    }

    public ReadMessageDto GetMessageById(Guid id)
    {
        var messageCollection = _database.GetCollection<Message>("Messages");
        var message = messageCollection.AsQueryable().FirstOrDefault(m => m.Guid == id);
        return new ReadMessageDto(message.Guid, message.Content, message.DateAndTime, message.TextChannelId, message.UserId, message.ChatroomId);
    }

    public IEnumerable<ReadMessageDto> GetMessagesByMemberId(Guid userId, Guid chatroomId)
    {
        var messageCollection = _database.GetCollection<Message>("Messages");
        return messageCollection.AsQueryable().Where(m => m.UserId == userId && m.ChatroomId == chatroomId).Select(m => new ReadMessageDto(m.Guid, m.Content, m.DateAndTime, m.TextChannelId, m.UserId, m.ChatroomId));
    }

    public IEnumerable<ReadMessageDto> GetMessagesByTextChannelId(Guid textChannelId)
    {
        var messageCollection = _database.GetCollection<Message>("Messages");
        return messageCollection.AsQueryable().Where(m => m.TextChannelId == textChannelId).Select(m => new ReadMessageDto(m.Guid, m.Content, m.DateAndTime, m.TextChannelId, m.UserId, m.ChatroomId));
    }

    public ReadMessageDto AddMessage(CreateMessageDto message)
    {
        var messageCollection = _database.GetCollection<Message>("Messages");
        var m = new Message(message.Content, message.UserId, message.ChatroomId, message.TextChannelId);
        messageCollection.InsertOne(m);
        return new ReadMessageDto(m.Guid, m.Content, m.DateAndTime, m.TextChannelId, m.UserId, m.ChatroomId);
    }

    public void UpdateMessage(UpdateMessageDto message)
    {
        var messageCollection = _database.GetCollection<Message>("Messages");
        var m = new Message(message.Content, message.UserId, message.ChatroomId, message.TextChannelId);
        messageCollection.ReplaceOne(msg => msg.Guid == message.Guid, m);
    }

    public void DeleteMessage(Guid id)
    {
        var messageCollection = _database.GetCollection<Message>("Messages");
        messageCollection.DeleteOne(msg => msg.Guid == id);
    }

    // Users
    public IEnumerable<ReadUserDto> GetAllUsers()
    {
        var userCollection = _database.GetCollection<User>("Users");
        return userCollection.AsQueryable().Select(u => new ReadUserDto(u.Guid, u.EmailAddress, u.Username));
    }

    public ReadUserDto GetUserById(Guid id)
    {
        var userCollection = _database.GetCollection<User>("Users");
        var user = userCollection.AsQueryable().FirstOrDefault(u => u.Guid == id);
        return new ReadUserDto(user.Guid, user.EmailAddress, user.Username);
    }

    public ReadUserDto AddUser(CreateUserDto user)
    {
        var userCollection = _database.GetCollection<User>("Users");
        var u = new User(user.ChatroomId, user.EmailAddress, user.Username, user.Password);
        userCollection.InsertOne(u);
        return new ReadUserDto(u.Guid, u.EmailAddress, u.Username);
    }

    public void UpdateUser(UpdateUserDto user)
    {
        var userCollection = _database.GetCollection<User>("Users");
        var u = new User(user.ChatroomId, user.EmailAddress, user.Username, user.Password);
        userCollection.ReplaceOne(usr => usr.Guid == user.Guid, u);
    }

    public void DeleteUser(Guid id)
    {
        var userCollection = _database.GetCollection<User>("Users");
        userCollection.DeleteOne(usr => usr.Guid == id);
    }
}
}
