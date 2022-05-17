using System;

namespace TestProject;

public class User
{
    public Guid Id { get; set; }
    public string Avatar { get; set; }
    public string UserName { get; set; }
    public string NormalizedUserName { get; set; }
    public string Email { get; set; }
    public string NormalizedEmail { get; set; }
    public bool EmailConfirmed { get; set; }
    public string PasswordHash { get; set; }
    public bool IsBanned { get; set; }

    public User(Guid id, string avatar,string userName,string normalizedUserName,string email, string normalizedEmail, bool emailConfirmed, string passwordHash,bool isBanned)
    {
        Id = id;
        Avatar = avatar;
        UserName = userName;
        NormalizedEmail = normalizedEmail;
        Email = email;
        NormalizedUserName = normalizedUserName;
        EmailConfirmed = emailConfirmed;
        PasswordHash = passwordHash;
        IsBanned = isBanned;
    }
}