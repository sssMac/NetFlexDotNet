using System;

namespace TestProject;

public class ReviewCreate
{
    public string UserName { get; set; }
    public Guid ContentId { get; set; }
    public string Text { get; set; }
    public float Rating { get; set; }

    public ReviewCreate(string userName,Guid contentId,string text ,float rating)
    {
        UserName = userName;
        ContentId = contentId;
        Text = text;
        Rating = rating;
    }
}