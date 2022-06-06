using System;

namespace TestProject1;

public class EpisodeCreate
{
    public string Title { get; set; }
    public Guid SerialId { get; set; }
    public int Duration { get; set; }
    public int Number { get; set; }
    public string VideoLink { get; set; }
    public string PreviewVideo { get; set; }

    public EpisodeCreate(string title,Guid serialId, int duration,int number, string videoLink, string previewVideo)
    {
        Title = title;
        SerialId = serialId;
        Duration = duration;
        Number = number;
        VideoLink = videoLink;
        PreviewVideo = previewVideo;
    }
}