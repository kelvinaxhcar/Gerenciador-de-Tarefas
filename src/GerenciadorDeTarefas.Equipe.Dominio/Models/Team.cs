namespace TaskManager.TeamApi.Domain.Models;

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}

public class Task
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Description { get; set; }
    public bool Concluida { get; set; }
    public int TeamId { get; set; }
}
