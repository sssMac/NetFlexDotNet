using System;

namespace TestProject;

public class CreateFilm
{
    public string Title { get; set; }
    public string Poster { get; set; }
    public int AgeRating { get; set; }
    public string Description { get; set; }
    public string VideoLink { get; set; }
    public string GenreName { get; set; }

    public CreateFilm(string title, string poster, string description, string videoLink, string genreName)
    {
        Title = title;
        Poster = poster;
        Description = description;
        VideoLink = videoLink;
        GenreName = genreName;
    }
}