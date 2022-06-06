using System;
using Ply;

namespace TestProject1.Models;

public class Film
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Poster { get; set; }
    public int AgeRating { get; set; }
    public float UserRating { get; set; }
    public string Description { get; set; }
    public string VideoLink { get; set; }
    public string GenreName { get; set; }

    public Film(Guid id,string title, string poster,int ageRating, float userRating,string description, string videoLink,string genreName)
    {
        Id = id;
        Title = title;
        Poster = poster;
        AgeRating = ageRating;
        UserRating = userRating;
        Description = description;
        VideoLink = videoLink;
        GenreName = genreName;
    }
}