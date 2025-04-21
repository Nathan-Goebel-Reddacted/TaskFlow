namespace TaskFlow.Storage.EFcore.Models;

public enum UserRole
{
    Admin,
    User
}

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public UserRole Role { get; set; }

    public ICollection<Project> Projects { get; set; } = new List<Project>();
}
