using Bogus;
using Microsoft.EntityFrameworkCore;
using Spg.Codechatter.Domain.V1.Model;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Spg.Codechatter.Infrastructure
{
    public class CodechatterContextSeeder
    {
        private readonly CodechatterContext _context;

        public CodechatterContextSeeder(CodechatterContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task SeedAsync(int chatroomCount, int userCount, int textChannelCount, int messageCount)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                var chatroomFaker = new Faker<Chatroom>("de")
                    .CustomInstantiator(f => new Chatroom(f.Lorem.Word()));

                var chatrooms = chatroomFaker.Generate(chatroomCount);

                var userFaker = new Faker<User>("de")
                    .CustomInstantiator(f =>
                    {
                        var chatroom = f.Random.ListItem<Chatroom>(chatrooms);
                        return new User(chatroom.Guid, f.Internet.Email(), f.Name.FullName(), f.Internet.Password());
                    });

                var users = userFaker.Generate(userCount);

                var textChannelFaker = new Faker<TextChannel>("de")
                    .CustomInstantiator(f => new TextChannel(f.Lorem.Word(), f.Random.ListItem<Chatroom>(chatrooms).Guid));

                var textChannels = textChannelFaker.Generate(textChannelCount);

                var messageFaker = new Faker<Message>("de")
                    .CustomInstantiator(f =>
                    {
                        var user = f.Random.ListItem<User>(users);
                        return new Message(f.Lorem.Sentence(), user.Guid, user.ChatroomId,
                            f.Random.ListItem<TextChannel>(textChannels).Guid);
                    });

                var messages = messageFaker.Generate(messageCount);

                _context.Users.AddRange(users);
                _context.Chatrooms.AddRange(chatrooms);
                _context.TextChannels.AddRange(textChannels);
                _context.Messages.AddRange(messages);

                await _context.SaveChangesAsync();
                
                await Task.Delay(10000);
            }
            catch (Exception ex)
            {
                // Handle specific exceptions based on your application's needs
                Console.WriteLine($"Error during seeding: {ex.Message}");
            }
            finally
            {
                stopwatch.Stop();

                Console.WriteLine($"Seeding: Elapsed Time for Data Operation: {stopwatch.ElapsedMilliseconds} ms");
            }
        }
    }
}
