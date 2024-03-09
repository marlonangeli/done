using Done.Domain.Shared;

namespace Done.Domain.Entities;

public sealed class ToDo : Entity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }
    public Priority Priority { get; set; }
    public DateTime? Due { get; set; }
    public ToDoList ToDoList { get; set; }
    public Guid ToDoListId { get; set; }

    public void MarkAsDone(bool isDone = true) => IsDone = isDone;

    public void ChangePriority(Priority priority) => Priority = priority;

    public void ChangeDueDate(DateTime? due) => Due = due;

    public void ChangeTitle(string title) => Title = title;

    public void ChangeDescription(string description) => Description = description;

    public void ChangeToDoList(ToDoList toDoList)
    {
        ToDoList = toDoList ?? throw new ArgumentNullException(nameof(toDoList));
        ToDoListId = toDoList.Id;

        toDoList.ToDos.Add(this);
    }
}