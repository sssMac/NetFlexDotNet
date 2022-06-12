module GiraffeAPI.Models.Review

open System
open System.ComponentModel.DataAnnotations
open Microsoft.EntityFrameworkCore.Metadata.Internal
open Ply.TplPrimitives

[<CLIMutable>]  
type Review =
    {
        Id : Guid
        UserName : string
        ContentId : Guid
        Text : string
        Rating : float
        PublishTime : DateTime
    }