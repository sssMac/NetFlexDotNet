namespace NetFlexAPI.Models;

public class SerialCreate
{
    public string Poster { get; set; }
    public string Title { get; set; }
    public int NumEpisodes { get; set; }
    public int AgeRating { get; set; }
    public string Description { get; set; }

    public SerialCreate(string poster, string title, int numEpisodes, int ageRating, string description)
    {
        Poster = poster;
        Title = title;
        NumEpisodes = numEpisodes;
        AgeRating = ageRating;
        Description = description;
    }
}