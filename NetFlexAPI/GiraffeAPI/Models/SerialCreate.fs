module GiraffeAPI.Models.SerialCreate

open System
open System.ComponentModel.DataAnnotations
open Microsoft.EntityFrameworkCore.Metadata.Internal
open GiraffeAPI.Models.Serial

[<CLIMutable>]  
type SerialCreate =
    {
        Poster : string
        Title : string
        NumEpisodes : int
        AgeRating : int
        Description : string
    }
    
    member this.HasErrors =
        if this.Poster = null || this.Poster = "" then Some "Poster is required"
        else if this.Title = null || this.Title = "" then Some "Title is required"
        else if string this.NumEpisodes = null || string this.NumEpisodes = "" then Some "NumEpisodes is required"
        else if string this.AgeRating = null || string this.AgeRating = "" then Some "AgeRating is required"
        else if this.Description = null || this.Description = "" then Some "Description is required"
        else None
        
    member this.GetSerial = {
                                Id = Guid.NewGuid()
                                Poster = this.Poster
                                Title = this.Title
                                NumEpisodes = this.NumEpisodes
                                AgeRating = this.AgeRating
                                UserRating = 0.0
                                Description = this.Description
                            }