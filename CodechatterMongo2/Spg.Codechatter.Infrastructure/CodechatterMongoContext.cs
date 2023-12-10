using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using Spg.Codechatter.Domain.V1.Model;
using System;
using System.Collections.Generic;

namespace Spg.Codechatter.Infrastructure
{
    public class CodechatterMongoContext
    {
        private readonly IMongoDatabase _database;

        public CodechatterMongoContext(IMongoClient client, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("mongodb+srv://Ray:3425LL44aaajKll014592lskfhj@cluster0.q8dofgk.mongodb.net/Codechatter");
            _database = client.GetDatabase(configuration["MongoDb:CodeChatter"]);
        }

        public IMongoCollection<Chatroom> Chatrooms => _database.GetCollection<Chatroom>("Chatrooms");
        public IMongoCollection<Message> Messages => _database.GetCollection<Message>("Messages");
        public IMongoCollection<TextChannel> TextChannels => _database.GetCollection<TextChannel>("TextChannels");
        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
    }

    public class CodechatterContextSeeder
    {
        private readonly CodechatterMongoContext _mongoContext;

        public CodechatterContextSeeder(CodechatterMongoContext mongoContext)
        {
            _mongoContext = mongoContext ?? throw new ArgumentNullException(nameof(mongoContext));
        }

        public void Seed()
        {
            var chatroomFaker = new Bogus.Faker<Chatroom>("de")
                .CustomInstantiator(f => new Chatroom(f.Lorem.Word()));

            var chatrooms = chatroomFaker.Generate(100);

            var userFaker = new Bogus.Faker<User>("de")
                .CustomInstantiator(f =>
                {
                    var chatroom = f.Random.ListItem<Chatroom>(chatrooms);
                    return new User(chatroom.Guid, f.Internet.Email(), f.Name.FullName(), f.Internet.Password());
                });

            var users = userFaker.Generate(100);

            var textChannelFaker = new Bogus.Faker<TextChannel>("de")
                .CustomInstantiator(f => new TextChannel(f.Lorem.Word(), f.Random.ListItem<Chatroom>(chatrooms).Guid));

            var textChannels = textChannelFaker.Generate(100);

            var messageFaker = new Bogus.Faker<Message>("de")
                .CustomInstantiator(f =>
                {
                    var user = f.Random.ListItem<User>(users);
                    return new Message(f.Lorem.Sentence(), user.Guid, user.ChatroomId,
                        f.Random.ListItem<TextChannel>(textChannels).Guid);
                });

            var messages = messageFaker.Generate(100);

            _mongoContext.Users.InsertMany(users);
            _mongoContext.Chatrooms.InsertMany(chatrooms);
            _mongoContext.TextChannels.InsertMany(textChannels);
            _mongoContext.Messages.InsertMany(messages);
        }
    }
}
