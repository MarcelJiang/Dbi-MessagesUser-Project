using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Spg.Codechatter.Domain.V1.Model;

namespace Spg.Codechatter.Infrastructure.Configuration;


public class TextChannelConfiguration : IEntityTypeConfiguration<TextChannel>
{
    public void Configure(EntityTypeBuilder<TextChannel> entity)
    {
        entity.HasKey(e => e.Id);
        entity.HasIndex(e => e.Guid).IsUnique();
    }
}