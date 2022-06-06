namespace TestProject;

public class UserAuth
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public UserAuth(string email, string passwordHash)
    {
        Email = email;
        PasswordHash = passwordHash;
    }
}