module GiraffeAPI.Models.Serial

open System
open System.ComponentModel.DataAnnotations
open Microsoft.EntityFrameworkCore.Metadata.Internal

[<CLIMutable>]  
type Serial =
    {
        Id : Guid
        Poster : string
        Title : string
        NumEpisodes : int
        AgeRating : int
        UserRating : float
        Description : string
    }
    
    