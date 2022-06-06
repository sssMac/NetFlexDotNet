module GiraffeAPI.Models.CreateRole

open System
open GiraffeAPI.Models.Role

type CreateRole =
    {
        RoleName : string
    }
    
    member this.HasErrors =
        if this.RoleName = null || this.RoleName = "" then Some "Name of role is required"
        else if this.RoleName = "User" then Some "you can't add role User"
        else None
        
    member this.GetRole = {
                                RoleId = Guid.NewGuid();
                                RoleName = this.RoleName;
                            }