
module GiraffeAPI.Models.UserRole


open System
open System.ComponentModel.DataAnnotations
open GiraffeAPI.Models

[<CLIMutable>]  
type UserRole =
    {
        [<Key>]
        UserId : Guid
        RoleId : Guid
    }



