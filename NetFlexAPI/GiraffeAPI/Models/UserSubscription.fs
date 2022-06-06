module GiraffeAPI.Models.UserSubscription

open System
open System.ComponentModel.DataAnnotations
open Microsoft.EntityFrameworkCore.Metadata.Internal
open Ply.TplPrimitives

[<CLIMutable>]  
type UserSubscription =
    {
        [<Key>]
        UserId : Guid
        SubscriptionId : Guid
        StartDate : DateTime
        FinishDate : DateTime
    }
    
    