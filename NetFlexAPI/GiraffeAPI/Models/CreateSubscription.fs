module GiraffeAPI.Models.CreateSubscription

open System
open System.ComponentModel.DataAnnotations
open GiraffeAPI.Models.Subscription
open Microsoft.EntityFrameworkCore.Metadata.Internal
open Ply.TplPrimitives

[<CLIMutable>]  
type CreateSubscription =
    {
        Name : string
        Price : string
    }

    member this.HasErrors =
        if this.Name = null || this.Name = "" then Some "Name is required"
        else None
        
    member this.GetSubscription = {
                                Name = this.Name
                                Id = Guid.NewGuid()
                                Price = this.Price
                            }