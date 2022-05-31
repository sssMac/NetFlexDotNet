namespace NetFlexAPI.Models;

public class Serial
{
    public Guid Id { get; set; }
    public string Poster { get; set; }
    public string Title { get; set; }
    public int NumEpisodes { get; set; }
    public int AgeRating { get; set; }
    public float UserRating { get; set; }
    public string Description { get; set; }
}