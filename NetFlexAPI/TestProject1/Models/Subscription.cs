using System;

namespace TestProject;

public class Subscription
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Price { get; set; }
    
    public Subscription(Guid id, string name,string price)
    {
        Id = id;
        Name = name;
        Price = price;
    }
}

