namespace TaskFlow.Storage.EFcore.Models;

public enum TaskStatus
{
    ÀFaire,
    EnCours,
    Terminé
}

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public TaskStatus Status { get; set; }
    public DateTime? DueDate { get; set; }

    public int ProjectId { get; set; }
    public Project Project { get; set; } = null!;

    public List<string> Commentaires { get; set; } = new();
}

