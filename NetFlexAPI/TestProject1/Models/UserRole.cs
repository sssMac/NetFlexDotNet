using System;

namespace TestProject;

public class UserRole
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }

    public UserRole(Guid userId, Guid roleId)
    {
        RoleId = roleId;
        UserId = userId;
    }
}