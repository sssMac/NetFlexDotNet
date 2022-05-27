module GiraffeAPI.Models.Genre

open System
open System.ComponentModel.DataAnnotations
open Microsoft.EntityFrameworkCore.Metadata.Internal

[<CLIMutable>]  
type Genre =
    {
        Id : Guid
        GenreName : string
    }
