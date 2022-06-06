module GiraffeAPI.Models.Episode

open System
open System.ComponentModel.DataAnnotations
open Microsoft.EntityFrameworkCore.Metadata.Internal

[<CLIMutable>]  
type Episode =
    {
        Id : Guid
        Title : string
        SerialId : Guid
        Duration : int
        Number : int
        VideoLink : string
        PreviewVideo : string
    }
    
    