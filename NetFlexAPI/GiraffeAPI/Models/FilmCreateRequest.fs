module GiraffeAPI.Models.FilmCreateRequest

open System
open System.ComponentModel.DataAnnotations
open Microsoft.EntityFrameworkCore.Metadata.Internal
open GiraffeAPI.Models.Film
open Ply.TplPrimitives

[<CLIMutable>]  
type FilmCreateRequest =
    {
        Title : string
        Poster : string
        AgeRating : int
        UserRating : float
        Description : string
        VideoLink : string
        GenreName : string
    }
    
    member this.HasErrors =
        if this.Title = null || this.Title = "" then Some "Title is required"
        else if this.Poster = null || this.Poster = "" then Some "Poster is required"
        else if string this.AgeRating = null || string this.AgeRating = "" then Some "AgeRating is required"
        else if string this.UserRating = null || string this.UserRating = "" then Some "UserRating is required"
        else if this.Description = null || this.Description = "" then Some "Description is required"
        else if this.VideoLink = null || this.VideoLink = "" then Some "VideoLink is required"
        else if this.GenreName = null || this.GenreName = "" then Some "GenreName is required"
        else None
        
    member this.GetFilm = {
                                Id = Guid.NewGuid()
                                Title = this.Title
                                Poster = this.Poster
                                AgeRating = this.AgeRating
                                UserRating = this.UserRating
                                Description = this.Description
                                VideoLink = this.VideoLink
                            }