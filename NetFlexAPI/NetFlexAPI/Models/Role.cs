using System.ComponentModel.DataAnnotations;

namespace NetFlexAPI.Models;

public class Role
{
    public Guid RoleId { get; set; }
    public string RoleName { get; set; }
}