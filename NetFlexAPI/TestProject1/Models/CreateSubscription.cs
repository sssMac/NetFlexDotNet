namespace TestProject;

public class CreateSubscription
{
    public string Name { get; set; }
    public string Price { get; set; }
    public CreateSubscription(string name, string price)
    {
        Name = name;
        Price = price;
    }
}