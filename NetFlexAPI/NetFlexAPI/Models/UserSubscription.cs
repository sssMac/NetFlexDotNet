using System.ComponentModel.DataAnnotations;

namespace NetFlexAPI.Models;

public class UserSubscription
{
    [Key]
    public Guid UserId { get; set; }
    public Guid SubscriptionId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime FinishDate { get; set; }
}