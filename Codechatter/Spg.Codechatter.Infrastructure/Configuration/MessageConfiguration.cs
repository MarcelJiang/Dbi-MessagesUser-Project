using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Spg.Codechatter.Domain.V1.Model;

namespace Spg.Codechatter.Infrastructure.Configuration;


public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> entity)
    {
        entity.HasKey(e => e.Id);
        entity.HasIndex(e => e.Guid).IsUnique();
    }
}