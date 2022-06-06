using System;
using Ply;

namespace TestProject1.Models;

public class Serial
{
    public Guid Id { get; set; }
    public string Poster { get; set; }
    public string Title { get; set; }
    public int NumEpisodes { get; set; }
    public int AgeRating { get; set; }
    public float UserRating { get; set; }
    public string Description { get; set; }

    public Serial(Guid id,string poster, string title,int numEpisodes,int ageRating,float userRating, string description)
    {
        Id = id;
        Poster = poster;
        Title = title;
        NumEpisodes = numEpisodes;
        AgeRating = ageRating;
        UserRating = userRating;
        Description = description;
    }
}