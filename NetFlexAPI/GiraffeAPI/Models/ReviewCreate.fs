module GiraffeAPI.Models.ReviewCreate

open System
open System.ComponentModel.DataAnnotations
open Microsoft.EntityFrameworkCore.Metadata.Internal
open Microsoft.EntityFrameworkCore.Storage
open Ply.TplPrimitives
open GiraffeAPI.Models.Review

[<CLIMutable>]  
type ReviewCreate =
    {
        UserName : string
        ContentId : Guid
        Text : string
        Rating : float
    }

    member this.HasErrors =
        if this.UserName = null || this.UserName = "" then Some "UserName of role is required"
        else if string this.ContentId = null || string this.ContentId = "" then Some "ContentId is required"
        else if this.Text = null || this.Text = "" then Some "Text is required"
        else if string this.Rating = null || string this.Rating = "" then Some "Rating is required"
        else None
        
    member this.GetReview = {
                                Id = Guid.NewGuid()   
                                UserName = this.UserName
                                ContentId = this.ContentId
                                Text = this.Text
                                Rating = this.Rating
                                PublishTime = DateTime.UtcNow
                             }