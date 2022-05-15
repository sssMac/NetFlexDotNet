module GiraffeAPI.Models.Role

open System
open System.ComponentModel.DataAnnotations
open Microsoft.EntityFrameworkCore.Metadata.Internal

[<CLIMutable>]  
type Role =
    {
        RoleId : Guid
        RoleName : string
    }

    member this.HasErrors =
        if this.RoleName = null || this.RoleName = "" then Some "Name of role is required"
        else None
        