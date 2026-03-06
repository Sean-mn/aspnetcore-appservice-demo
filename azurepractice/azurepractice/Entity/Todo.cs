namespace azurepractice.Entity;

public class Todo
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsDone { get; set; }

    public Todo(int id, string title, bool isDone)
    {
        Id = id;
        Title = title;
        IsDone = isDone;
    }
}