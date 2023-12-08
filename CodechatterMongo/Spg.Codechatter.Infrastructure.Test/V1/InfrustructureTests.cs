using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Spg.Codechatter.Domain.V1.Model;
using Xunit;

namespace Spg.Codechatter.Infrastructure.Test.V1
{
    public class InfrastructureTests
    {
        private CodechatterContext GetContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder()
                .UseSqlite("Data Source=Codecatter.db")
                .Options;

            CodechatterContext db = new CodechatterContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            return db;
        }

        private void populateDatabase()
        {
            CodechatterContext db = GetContext();

            Chatroom c1 = new Chatroom("Test");
            db.Chatrooms.Add(c1);

            User u1 = new User(c1.Guid, "franz@hotmail.com", "Boba", "12345");
            User u2 = new User(c1.Guid, "manfred@hotmail.com", "Dreio", "12345");
            User u3 = new User(c1.Guid, "herbert@hotmail.com", "Logoa", "12345");

            db.Users.AddRange(u1, u2, u3);

            TextChannel tx1 = new TextChannel("General", c1.Guid);
            TextChannel tx2 = new TextChannel("asdf", c1.Guid);

            c1.AddTextChannel(tx1);
            c1.AddTextChannel(tx2);

            Message m1 = new Message("test", u1.Guid, c1.Guid, tx1.Guid);

            tx1.AddMessage(m1);

            db.SaveChanges();
        }


        [Fact]
        public void DomainModel_Create_Chatroom_Success_Test()
        {
            // Arrange
            CodechatterContext db = GetContext();

            populateDatabase();

            // Assert
            Assert.Equal(1, db.Chatrooms.Count());
        }

        [Fact]
        public void DomainModel_Create_TextChannel_Success_Test()
        {
            // Arrange
            CodechatterContext db = GetContext();

            populateDatabase();

            // Assert
            Assert.Equal(2, db.TextChannels.Count());
        }

        [Fact]
        public void DomainModel_Create_User_Success_Test()
        {
            // Arrange
            CodechatterContext db = GetContext();

            populateDatabase();

            // Assert
            Assert.Equal(3, db.Users.Count());
        }

        [Fact]
        public void DomainModel_Create_Message_Success_Test()
        {
            // Arrange
            CodechatterContext db = GetContext();

            populateDatabase();

            // Assert
            Assert.Equal(1, db.Messages.Count());
        }
    }
}
