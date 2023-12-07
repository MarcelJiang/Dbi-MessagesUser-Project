using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Spg.Codechatter.Domain.V1.Model;

namespace Spg.Codechatter.Infrastructure.Configuration;


public class ChatroomConfiguration : IEntityTypeConfiguration<Chatroom>
{
    public void Configure(EntityTypeBuilder<Chatroom> entity)
    {
        entity.HasKey(e => e.Id);
        entity.HasIndex(e => e.Guid).IsUnique();
    }
}