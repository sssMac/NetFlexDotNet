module GiraffeAPI.Data.ApplicationContext


open System
open GiraffeAPI
open GiraffeAPI.Models
open GiraffeAPI.Models.User
open Microsoft.EntityFrameworkCore
open System.Linq

type ApplicationContext(options : DbContextOptions<ApplicationContext>) = 
        inherit DbContext(options)
        
            override __.OnModelCreating modelBuilder = 
                let expr =  modelBuilder.Entity<User>().HasKey(fun user -> (user.Id) :> obj) 
                modelBuilder.Entity<User>().Property(fun user -> user.Id).ValueGeneratedOnAdd() |> ignore
          
        [<DefaultValue>]
        val mutable users:DbSet<User>
        member x.Users 
            with get() = x.users 
            and set v = x.users <- v
              
              
module UserRetository = 

    let getAll (context : ApplicationContext) = context.Users
    let getUser (context : ApplicationContext) id = context.Users |> Seq.tryFind (fun f -> f.Id = id)
    let addUserAsync (context : ApplicationContext) (entity : User) = 
        task { 
            context.Users.AddAsync(entity)
            |> ignore
            let! result = context.SaveChangesAsync true |> Async.AwaitTask
            let result = if result >= 1  then Some(entity) else None
            return result
        }
    let updateUser (context : ApplicationContext) (entity : User) (id : Guid) = 
        let current = context.Users.Find(id)
        let updated = { entity with Id = id }
        context.Entry(current).CurrentValues.SetValues(updated)
        if context.SaveChanges true >= 1  then Some(updated) else None
    let banUser (context : ApplicationContext) (id : Guid) = 
        let current = context.Users.Find(id)
        let deleted = { current with IsBanned = true }
        updateUser context deleted id
    let unbunUser (context : ApplicationContext) (id : Guid) = 
        let current = context.Users.Find(id)
        let banned = { current with IsBanned = false }
        updateUser context banned id
        
    let deleteUser (context:ApplicationContext) (id:Guid) =
        let current = context.Users.Find(id)
        context.Users.Remove(current)
        if context.SaveChanges true >= 1  then Some(current) else None
        

let getAll  = UserRetository.getAll 
let getUser  = UserRetository.getUser
let addUserAsync = UserRetository.addUserAsync
let updateUser = UserRetository.updateUser
let banUser = UserRetository.banUser
let unbunUser = UserRetository.unbunUser
let deleteUser = UserRetository.deleteUser
       
          
        
                                           
        
        