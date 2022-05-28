namespace NetFlexAPI.Models;

public class Film
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Poster { get; set; }
    public int AgeRating { get; set; }
    public float UserRating { get; set; }
    public string Description { get; set; }
    public string VideoLink { get; set; }
}