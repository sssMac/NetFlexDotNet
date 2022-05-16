using System.ComponentModel.DataAnnotations;

namespace NetFlexAPI.Models;

public class UserRole
{
    [Key]
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}