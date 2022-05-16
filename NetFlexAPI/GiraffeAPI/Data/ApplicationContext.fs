module GiraffeAPI.Data.ApplicationContext


open System
open GiraffeAPI
open GiraffeAPI.Models
open GiraffeAPI.Models.User
open GiraffeAPI.Models.UserRole
open GiraffeAPI.Models.Role
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
            
        [<DefaultValue>]
        val mutable userroles:DbSet<UserRole>
        member x.UserRoles 
            with get() = x.userroles 
            and set v = x.userroles <- v
            
        [<DefaultValue>]
        val mutable roles:DbSet<Role>
        member x.Roles
            with get() = x.roles 
            and set v = x.roles <- v
              
              
module UserRetository = 

    let getAll (context : ApplicationContext) = context.Users
    
    // DONE
    let getUser (context : ApplicationContext) id = context.Users |> Seq.tryFind (fun f -> f.Id = id)
    
    // DONE
    let addUserAsync (context : ApplicationContext) (entity : User) = 
        async {
            context.Users.AddRangeAsync(entity)
            |> Async.AwaitTask
            |> ignore
            let userrole : UserRole[]=
                [|
                    { UserId = entity.Id; RoleId = Guid.Parse("37050332-97c2-4fb9-a9cd-97b5c86b35d6") }
                |]
            context.UserRoles.AddRangeAsync(userrole)
            |> Async.AwaitTask
            |> ignore
            let! result = context.SaveChangesAsync true |> Async.AwaitTask
            let result = if result >= 1  then Some(entity) else None
            return result
        }
        
        // DONE
    let updateUser (context : ApplicationContext) (entity : User) (id : Guid) = 
        let current = context.Users.Find(id)
        let updated = { entity with Id = id }
        context.Entry(current).CurrentValues.SetValues(updated)
        if context.SaveChanges true >= 1  then Some(updated) else None
    let banUser (context : ApplicationContext) (id : Guid) = 
        let current = context.Users.Find(id)
        let deleted = { current with IsBanned = true }
        updateUser context deleted id
        
        //DONE
    let unbunUser (context : ApplicationContext) (id : Guid) = 
        let current = context.Users.Find(id)
        let banned = { current with IsBanned = false }
        updateUser context banned id
        
        //DONE
        
    let deleteUser (context:ApplicationContext) (id:Guid) =
        let current = context.Users.Find(id)
        context.Users.Remove(current)
        let currentUserRole = context.UserRoles.Find(id)
        context.UserRoles.Remove(currentUserRole)
        if context.SaveChanges true  >= 1  then Some(current) else None
        
        // DONE
        
module RoleRetository =
    let getAllRoles (context : ApplicationContext) = context.roles
    
    let getRole (context : ApplicationContext) id = context.roles |> Seq.tryFind (fun f -> f.RoleId = id)
    
    let addRoleAsync (context : ApplicationContext) (entity : Role) = 
        async {
            context.Roles.AddRangeAsync(entity)
            |> Async.AwaitTask
            |> ignore
            let! result = context.SaveChangesAsync true |> Async.AwaitTask
            let result = if result >= 1  then Some(entity) else None
            return result
        }
        
    let deleteRole (context:ApplicationContext) (id:Guid) =
        let current = context.Roles.Find(id)
        if current.RoleId = Guid.Parse("37050332-97c2-4fb9-a9cd-97b5c86b35d6") then None
        else
        context.Roles.Remove(current)
        for user in context.UserRoles do
            if user.RoleId = id then
                context.Entry(user).CurrentValues.SetValues({RoleId = Guid.Parse("37050332-97c2-4fb9-a9cd-97b5c86b35d6")
                                                             UserId = user.UserId})
        if context.SaveChanges true  >= 1  then Some(current) else None
        
    let updateRole (context : ApplicationContext) (entity : Role) (id : Guid) = 
        let current = context.Roles.Find(id)
        let updated = { entity with RoleId = id }
        if current.RoleId = Guid.Parse("37050332-97c2-4fb9-a9cd-97b5c86b35d6") then None
        else
        context.Entry(current).CurrentValues.SetValues(updated)
        if context.SaveChanges true >= 1  then Some(updated) else None
        
        
let getAll  = UserRetository.getAll 
let getUser  = UserRetository.getUser
let addUserAsync = UserRetository.addUserAsync
let updateUser = UserRetository.updateUser
let banUser = UserRetository.banUser
let unbunUser = UserRetository.unbunUser
let deleteUser = UserRetository.deleteUser


let getAllRoles = RoleRetository.getAllRoles
let getRole = RoleRetository.getRole
let addRoleAsync = RoleRetository.addRoleAsync
let deleteRole = RoleRetository.deleteRole
let updateRole = RoleRetository.updateRole   
          
        
                                           
        
        