module GiraffeAPI.Models.Subscription

open System
open System.ComponentModel.DataAnnotations
open Microsoft.EntityFrameworkCore.Metadata.Internal
open Ply.TplPrimitives

[<CLIMutable>]  
type Subscription =
    {
        Id : Guid
        Name : string
        Price : string
    }

    member this.HasErrors =
        if this.Name = null || this.Name = "" then Some "Name is required"
        elif this.Price = null || this.Price = "" then Some "Cost is required"
        else None
        