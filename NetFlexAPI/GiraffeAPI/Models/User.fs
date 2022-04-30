module GiraffeAPI.Models.User

open System

[<CLIMutable>]  
type User =
    {
        Id : Guid
        Avatar: string
        UserName: string
        NormalizedUserName: string
        Email: string
        NormalizedEmail: string
        EmailConfirmed: bool
        PasswordHash: string
        IsBanned: bool
    }
    
    member this.HasErrors =
        if this.Email = null || this.Email = "" then Some "Email is required"
        else if this.PasswordHash = null || this.PasswordHash = "" then Some "Password is required"
        else None