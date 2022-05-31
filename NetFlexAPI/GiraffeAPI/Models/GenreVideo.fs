module GiraffeAPI.Models.GenreVideo

open System
open System.ComponentModel.DataAnnotations
open Microsoft.EntityFrameworkCore.Metadata.Internal

[<CLIMutable>]  
type GenreVideo =
    {
        Id : Guid
        GenreName : string
        ContentId : Guid
    }