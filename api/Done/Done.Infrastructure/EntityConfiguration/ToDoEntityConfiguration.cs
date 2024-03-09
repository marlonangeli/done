using Done.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Done.Infrastructure.EntityConfiguration;

internal sealed class ToDoEntityConfiguration : IEntityTypeConfiguration<ToDo>
{
    public void Configure(EntityTypeBuilder<ToDo> builder)
    {
        builder.ConfigureBaseEntity();

        builder.HasOne(x => x.ToDoList)
            .WithMany(x => x.ToDos)
            .HasForeignKey(x => x.ToDoListId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(128)
            .IsUnicode();

        builder.HasIndex(x => x.Title)
            .IsUnique(false);

        builder.Property(x => x.Description)
            .IsRequired(false)
            .HasMaxLength(2000)
            .HasDefaultValue("")
            .IsUnicode();

        builder.Property(x => x.IsDone)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.Priority)
            .IsRequired()
            .HasConversion<int>()
            .HasDefaultValue(Priority.None);

        builder.Property(x => x.Due)
            .IsRequired(false);
    }
}