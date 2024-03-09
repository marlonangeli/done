using Done.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Done.Infrastructure.EntityConfiguration;

internal sealed class ToDoListEntityConfiguration : IEntityTypeConfiguration<ToDoList>
{
    public void Configure(EntityTypeBuilder<ToDoList> builder)
    {
        builder.ConfigureBaseEntity();

        builder.HasOne(x => x.User)
            .WithMany(x => x.ToDoLists)
            .HasForeignKey(x => x.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.ToDos)
            .WithOne(x => x.ToDoList)
            .HasForeignKey(x => x.ToDoListId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Title)
            .IsRequired()
            .IsUnicode()
            .HasMaxLength(64);

        builder.HasIndex(x => x.Title)
            .IsUnique(false);
    }
}