module GiraffeAPI.Models.GenreCreate

open System
open System.ComponentModel.DataAnnotations
open Microsoft.EntityFrameworkCore.Metadata.Internal
open GiraffeAPI.Models.Genre

[<CLIMutable>]  
type GenreCreate =
    {
        GenreName : string
    }
    
    member this.HasErrors =
        if this.GenreName = null || this.GenreName = "" then Some "Name of genre is required"
        else None
        
    member this.GetGenre = {
                                Id = Guid.NewGuid();
                                GenreName = this.GenreName;
                            }