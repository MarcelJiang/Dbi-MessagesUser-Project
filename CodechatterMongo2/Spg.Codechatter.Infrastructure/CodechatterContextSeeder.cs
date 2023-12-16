using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using Spg.Codechatter.Domain.V1.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Spg.Codechatter.Infrastructure;

public class CodechatterContextSeeder
{
    
    
    private readonly CodechatterMongoContext _mongoContext;

    public CodechatterContextSeeder(CodechatterMongoContext mongoContext)
    {
        _mongoContext = mongoContext ?? throw new ArgumentNullException(nameof(mongoContext));
    }

    public void Seed(int chatroomCount, int userCount, int textChannelCount, int messageCount)
    {
        
        var chatroomFaker = new Bogus.Faker<Chatroom>("de")
            .CustomInstantiator(f => new Chatroom(f.Lorem.Word()));

        var chatrooms = chatroomFaker.Generate(chatroomCount);

        var userFaker = new Bogus.Faker<User>("de")
            .CustomInstantiator(f =>
            {
                var chatroom = f.Random.ListItem<Chatroom>(chatrooms);
                return new User(chatroom.Guid, f.Internet.Email(), f.Name.FullName(), f.Internet.Password());
            });

        var users = userFaker.Generate(userCount);

        var textChannelFaker = new Bogus.Faker<TextChannel>("de")
            .CustomInstantiator(f => new TextChannel(f.Lorem.Word(), f.Random.ListItem<Chatroom>(chatrooms).Guid));

        var textChannels = textChannelFaker.Generate(textChannelCount);

        var messageFaker = new Bogus.Faker<Message>("de")
            .CustomInstantiator(f =>
            {
                var user = f.Random.ListItem<User>(users);
                return new Message(f.Lorem.Sentence(), user.Guid, user.ChatroomId,
                    f.Random.ListItem<TextChannel>(textChannels).Guid);
            });

        var messages = messageFaker.Generate(messageCount);
        
        Stopwatch stopwatch = new Stopwatch();

        // Start the Stopwatch
        stopwatch.Start();
        try
        {
            _mongoContext.Users.InsertMany(users);
            _mongoContext.Chatrooms.InsertMany(chatrooms);
            _mongoContext.TextChannels.InsertMany(textChannels);
            _mongoContext.Messages.InsertMany(messages);
        }
        finally
        {
            stopwatch.Stop();

            // Log or use the elapsed time as needed
            Console.WriteLine($"Seeding : Elapsed Time for Data Operation: {stopwatch.ElapsedMilliseconds} ms");
              
        }
        
    }
}