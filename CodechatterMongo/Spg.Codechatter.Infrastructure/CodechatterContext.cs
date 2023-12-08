using Bogus;
using Microsoft.EntityFrameworkCore;
using Spg.Codechatter.Domain.V1.Model;
using Spg.Codechatter.Infrastructure.Configuration;
using System;
using System.Collections.Generic;


namespace Spg.Codechatter.Infrastructure
{
    public class CodechatterContext : DbContext
    {
        public DbSet<Chatroom> Chatrooms => Set<Chatroom>();
        public DbSet<Message> Messages => Set<Message>();
        public DbSet<TextChannel> TextChannels => Set<TextChannel>();
        public DbSet<User> Users => Set<User>();

        protected CodechatterContext()
            : this(new DbContextOptions<DbContext>())
        { }

        public CodechatterContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=Codechatter.db");
                optionsBuilder.UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ChatroomConfiguration());
            modelBuilder.ApplyConfiguration(new MessageConfiguration());
            modelBuilder.ApplyConfiguration(new TextChannelConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }

        public void Seed()
        {
            var chatroomFaker = new Faker<Chatroom>("de")
                .CustomInstantiator(f => new Chatroom(f.Lorem.Word()));

            var chatrooms = chatroomFaker.Generate(100);

            var userFaker = new Faker<User>("de")
                .CustomInstantiator(f =>
                {
                    var chatroom = f.Random.ListItem<Chatroom>(chatrooms);
                    return new User(chatroom.Guid, f.Internet.Email(), f.Name.FullName(), f.Internet.Password());
                });

            var users = userFaker.Generate(100);

            var textChannelFaker = new Faker<TextChannel>("de")
                .CustomInstantiator(f => new TextChannel(f.Lorem.Word(), f.Random.ListItem<Chatroom>(chatrooms).Guid));

            var textChannels = textChannelFaker.Generate(100);

            var messageFaker = new Faker<Message>("de")
                .CustomInstantiator(f =>
                {
                    var user = f.Random.ListItem<User>(users);
                    return new Message(f.Lorem.Sentence(), user.Guid, user.ChatroomId,
                        f.Random.ListItem<TextChannel>(textChannels).Guid);
                });

            var messages = messageFaker.Generate(100);

            Users.AddRange(users);
            Chatrooms.AddRange(chatrooms);
            TextChannels.AddRange(textChannels);
            Messages.AddRange(messages);

            SaveChanges();
        }
    }
}