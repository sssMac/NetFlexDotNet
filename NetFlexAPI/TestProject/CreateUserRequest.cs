namespace TestProject;

public class CreateUserRequest
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public CreateUserRequest(string email, string passwordHash)
    {
        Email = email;
        PasswordHash = passwordHash;
    }
}