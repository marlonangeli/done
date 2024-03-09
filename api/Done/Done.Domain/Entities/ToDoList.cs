using Done.Domain.Shared;

namespace Done.Domain.Entities;

public sealed class ToDoList : Entity
{
    public string Title { get; set; }
    public List<ToDo> ToDos { get; set; }
    public User User { get; set; }
    public Guid UserId { get; set; }

    public void MarkAsDone()
    {
        foreach (var toDo in ToDos)
        {
            toDo.IsDone = true;
        }
    }

    public void MarkAsUndone()
    {
        foreach (var toDo in ToDos)
        {
            toDo.IsDone = false;
        }
    }
}