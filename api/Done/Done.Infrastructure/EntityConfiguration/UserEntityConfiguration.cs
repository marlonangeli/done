using Done.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Done.Infrastructure.EntityConfiguration;

internal sealed class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ConfigureBaseEntity();

        builder.HasMany(x => x.ToDoLists)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(128)
            .IsUnicode();

        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.Property(x => x.Username)
            .IsRequired()
            .HasMaxLength(64)
            .IsUnicode();
    }
}