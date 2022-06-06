module GiraffeAPI.Models.Film

open System
open System.ComponentModel.DataAnnotations
open Microsoft.EntityFrameworkCore.Metadata.Internal

[<CLIMutable>]  
type Film =
    {
        Id : Guid
        Title : string
        Poster : string
        AgeRating : int
        UserRating : float
        Description : string
        VideoLink : string
    }
    
    
    