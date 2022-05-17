using System;

namespace TestProject;

public class Role
{
    public Guid RoleId { get; set; }
    public string RoleName { get; set; }

    public Role(Guid roleId, string roleName)
    {
        RoleId = roleId;
        RoleName = roleName;
    }
}