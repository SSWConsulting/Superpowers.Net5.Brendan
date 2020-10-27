using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Net5DomainModel
{
    public class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
    {
        public void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            builder
                .HasMany(t => t.Tags)
                .WithMany(t => t.TodoItems);

            builder.Property(t => t.Title)
                .HasMaxLength(200)
                .IsRequired();


            builder.HasData(new TodoItem() { Id = -2, ListId = -1, Title = "Attend .NET Superpowers", Priority = PriorityLevel.Medium });
            builder.HasData(new TodoItem() { Id = -3, ListId = -1, Title = "Watch the Demos", Priority = PriorityLevel.Medium });
            builder.HasData(new TodoItem() { Id = -4, ListId = -1, Title = "Try it on your own projects", Priority = PriorityLevel.Medium });
            builder.HasData(new TodoItem() { Id = -5, ListId = -1, Title = "???", Priority = PriorityLevel.Medium });
            builder.HasData(new TodoItem() { Id = -6, ListId = -1, Title = "Profit", Priority = PriorityLevel.Medium });

        }
    }
}
