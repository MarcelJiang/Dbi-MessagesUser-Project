using Microsoft.EntityFrameworkCore;
using Spg.Codechatter.Domain.V1.Model;
using Spg.Codechatter.Infrastructure.Configuration;

namespace Spg.Codechatter.Infrastructure
{
    public class CodechatterContext : DbContext
    {
        public DbSet<Chatroom> Chatrooms => Set<Chatroom>();
        public DbSet<Message> Messages => Set<Message>();
        public DbSet<TextChannel> TextChannels => Set<TextChannel>();
        public DbSet<User> Users => Set<User>();

        protected CodechatterContext()
            : this(new DbContextOptions<CodechatterContext>())
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
    }
}