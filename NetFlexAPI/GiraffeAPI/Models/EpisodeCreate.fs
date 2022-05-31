module GiraffeAPI.Models.EpisodeCreate



open System
open System.ComponentModel.DataAnnotations
open Microsoft.EntityFrameworkCore.Metadata.Internal
open GiraffeAPI.Models.Episode
open GiraffeAPI.Models.Serial

[<CLIMutable>]  
type EpisodeCreate =
    {
        Title : string
        SerialId : Guid
        Duration : int
        Number : int
        VideoLink : string
        PreviewVideo : string
    }
    
    member this.HasErrors =
        if this.Title = null || this.Title = "" then Some "Title is required"
        else if this.Title = null || this.Title = "" then Some "Title is required"
        else if string this.SerialId = null || string this.SerialId = "" then Some "SerialId is required"
        else if string this.Duration = null || string this.Duration = "" then Some "Duration is required"
        else if string this.Number = null || string this.Number = "" then Some "Number is required"
        else if this.VideoLink = null || this.VideoLink = "" then Some "VideoLink is required"
        else if this.PreviewVideo = null || this.PreviewVideo = "" then Some "PreviewVideo is required"
        else None
        
    member this.GetEpisode = {
                                Id = Guid.NewGuid()
                                Title = this.Title
                                SerialId = this.SerialId
                                Duration = this.Duration
                                Number = this.Number
                                VideoLink = this.VideoLink
                                PreviewVideo = this.PreviewVideo
                            }