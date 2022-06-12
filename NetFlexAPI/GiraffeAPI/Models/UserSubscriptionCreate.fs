module GiraffeAPI.Models.UserSubscriptionCreate

open System
open System.ComponentModel.DataAnnotations
open Microsoft.EntityFrameworkCore.Metadata.Internal
open Ply.TplPrimitives
open GiraffeAPI.Models.UserSubscription

[<CLIMutable>]  
type UserSubscriptionCreate =
    {
        UserId : Guid
        SubscriptionId : Guid
    }

    member this.HasErrors =
        if string this.UserId = null || string this.UserId = "" then Some "UserId is required"
        else if string this.SubscriptionId = null || string this.SubscriptionId = "" then Some "SubscriptionId is required"
        else None
        
    member this.GetSubscription = {
                                UserId = this.UserId
                                SubscriptionId = this.SubscriptionId
                                StartDate = DateTime.UtcNow
                                FinishDate = DateTime.UtcNow.AddDays(30)
                            }