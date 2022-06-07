using System;

namespace TestProject1.Models;

public class UserSubscriptionCreate
{
    public Guid UserId { get; set; }
    public Guid SubscriptionId { get; set; }

    internal UserSubscriptionCreate(Guid userId, Guid subscriptionId)
    {
        UserId = userId;
        SubscriptionId = subscriptionId;
    }
}