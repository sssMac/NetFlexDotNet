module GiraffeAPI.Models.UserAuth


open System
open GiraffeAPI.Models.User


type UserAuth =
    {
        Email: string
        PasswordHash: string
    }
    
    member this.HasErrors =
        if this.Email = null || this.Email = "" then Some "Email is required"
        else if this.PasswordHash = null || this.PasswordHash = "" then Some "Password is required"
        else None