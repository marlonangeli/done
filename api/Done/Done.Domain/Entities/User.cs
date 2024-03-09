using Done.Domain.Shared;

namespace Done.Domain.Entities;

public sealed class User : Entity
{
    public string Username { get; set; }
    public string Email { get; set; }
    public List<ToDoList> ToDoLists { get; set; }
}