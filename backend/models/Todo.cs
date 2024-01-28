namespace backend.models
{
  public class Todo
  {
    public Todo(string title)
    {
      Id = Guid.NewGuid();
      Title = title;
      Status = false;
    }
    public Guid Id { get; set; }
    public string Title { get; set; } = "";
    public bool Status { get; set; }
  }
}