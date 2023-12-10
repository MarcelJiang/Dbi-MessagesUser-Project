using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Spg.Codechatter.Domain.V1.Model;

namespace Spg.Codechatter.Infrastructure.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Guid).IsUnique();

            // Anpassungen für MongoDB
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        }
    }
}