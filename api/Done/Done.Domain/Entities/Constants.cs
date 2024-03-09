namespace Done.Domain.Entities;

public static class Constants
{
    public static class ToDo
    {
        public const int TitleMaxLength = 128;
        public const int DescriptionMaxLength = 2000;
    }

    public static class ToDoList
    {
        public const int TitleMaxLength = 128;
    }

    public static class User
    {
        public const int NameMaxLength = 32;
        public const int EmailMaxLength = 128;
    }
}