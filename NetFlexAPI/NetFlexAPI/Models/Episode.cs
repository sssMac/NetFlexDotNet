namespace NetFlexAPI.Models;

public class Episode
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public Guid SerialId { get; set; }
    public int Duration { get; set; }
    public int Number { get; set; }
    public string VideoLink { get; set; }
    public string PreviewVideo { get; set; }
}