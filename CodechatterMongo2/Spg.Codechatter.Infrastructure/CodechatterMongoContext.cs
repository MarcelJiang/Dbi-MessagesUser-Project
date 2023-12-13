using Microsoft.Extensions.Configuration;
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
            // Verwenden Sie die fest codierte Verbindungszeichenfolge
            //var connectionString = configuration.GetConnectionString("mongodb+srv://Ray:yalayala@cluster0.q8dofgk.mongodb.net/?retryWrites=true&w=majority");
            _database = client.GetDatabase("Codechatter");
        }

        public IMongoCollection<Chatroom> Chatrooms => _database.GetCollection<Chatroom>("Chatrooms");
        public IMongoCollection<Message> Messages => _database.GetCollection<Message>("Messages");
        public IMongoCollection<TextChannel> TextChannels => _database.GetCollection<TextChannel>("TextChannels");
        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
    }

    
}