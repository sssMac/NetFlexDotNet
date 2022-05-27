module GiraffeAPI.Data.ApplicationContext


open System
open GiraffeAPI
open GiraffeAPI.Models
open GiraffeAPI.Models.User
open GiraffeAPI.Models.UserRole
open GiraffeAPI.Models.Role
open GiraffeAPI.Models.UserAuth
open GiraffeAPI.Models.Subscription
open GiraffeAPI.Models.Genre
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
        val mutable subscriptions:DbSet<Subscription>
        member x.Subscriptions 
            with get() = x.subscriptions 
            and set v = x.subscriptions <- v
            
            
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
            
        [<DefaultValue>]
        val mutable genres:DbSet<Genre>
        member x.Genres
            with get() = x.genres 
            and set v = x.genres <- v
              
              
module UserRetository = 

    let getAll (context : ApplicationContext) = context.Users
    
    // DONE
    let getUser (context : ApplicationContext) (id : Guid) = context.Users |> Seq.tryFind (fun f -> f.Id = id)
    
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
        
    let userAuth (context : ApplicationContext) (entity : UserAuth) =
        async {
            let current =
                query {
                    for serie in context.Users do
                        find (serie.Email.Equals (entity.Email))
                }
            let result =
                if current.PasswordHash = entity.PasswordHash then
                    let userId = current.Id
                    let currentRoleId = context.UserRoles.Find(userId)
                    let currentRole = context.Roles.Find(currentRoleId.RoleId)
                    let role = currentRole.RoleName
                    Some(role)    
                else None
            return result
        }
        
        
        
        
        
        
        
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
        context.Entry(current).CurrentValues.SetValues(updated)
        if context.SaveChanges true >= 1  then Some(updated) else None
        
    let updateUserRole (context : ApplicationContext) (entity : UserRole) (id : Guid) = 
        let current = context.UserRoles.Find(id)
        let updated = { entity with UserId = id }
        context.Entry(current).CurrentValues.SetValues(updated)
        if context.SaveChanges true >= 1  then Some(updated) else None

module SubscriptionsRepository =
    let getSub (context : ApplicationContext) (id : Guid) = context.Subscriptions |> Seq.tryFind (fun f -> f.Id = id)
    
    let getAllSub (context : ApplicationContext) = context.Subscriptions
    
    let addSubAsync (context : ApplicationContext) (entity : Subscription) = 
        async {
            context.Subscriptions.AddRangeAsync(entity)
            |> Async.AwaitTask
            |> ignore
            let! result = context.SaveChangesAsync true |> Async.AwaitTask
            let result = if result >= 1  then Some(entity) else None
            return result
        }
        
    let updateSub (context : ApplicationContext) (entity : Subscription) (id : Guid) = 
        let current = context.Subscriptions.Find(id)
        let updated = { entity with Id = id }
        if current.Id = Guid.Parse("9cda4bf9-db72-4299-a7ec-dc608fb4e2c1") then None
        else
        context.Entry(current).CurrentValues.SetValues(updated)
        if context.SaveChanges true >= 1  then Some(updated) else None
        
    let deleteSub (context:ApplicationContext) (id:Guid) =
        let current = context.Subscriptions.Find(id)
        if current.Id = Guid.Parse("9cda4bf9-db72-4299-a7ec-dc608fb4e2c1") then None
        else
        context.Subscriptions.Remove(current)
        if context.SaveChanges true  >= 1  then Some(current) else None
        
module GenreRepository =
    
    let updateGenreName (context : ApplicationContext) (entity : Genre) (id : Guid) = 
        let current = context.Genres.Find(id)
        let updated = { entity with Id = id }
        context.Entry(current).CurrentValues.SetValues(updated)
        if context.SaveChanges true >= 1  then Some(updated) else None
        
    let getAllGenres (context : ApplicationContext) = context.genres
    
    let deleteGenre (context:ApplicationContext) (id:Guid) =
        let current = context.Genres.Find(id)
        context.Genres.Remove(current)
        if context.SaveChanges true  >= 1  then Some(current) else None
        
    let addGenreAsync (context : ApplicationContext) (entity : Genre) = 
        async {
            context.Genres.AddRangeAsync(entity)
            |> Async.AwaitTask
            |> ignore
            let! result = context.SaveChangesAsync true |> Async.AwaitTask
            let result = if result >= 1  then Some(entity) else None
            return result
        }
        
let getAll  = UserRetository.getAll 
let getUser  = UserRetository.getUser
let addUserAsync = UserRetository.addUserAsync
let updateUser = UserRetository.updateUser
let banUser = UserRetository.banUser
let unbunUser = UserRetository.unbunUser
let deleteUser = UserRetository.deleteUser
let userAuth = UserRetository.userAuth

let getAllRoles = RoleRetository.getAllRoles
let getRole = RoleRetository.getRole
let addRoleAsync = RoleRetository.addRoleAsync
let deleteRole = RoleRetository.deleteRole
let updateRole = RoleRetository.updateRole
let updateUserRole = RoleRetository.updateUserRole
          
let getSub = SubscriptionsRepository.getSub
let getAllSub = SubscriptionsRepository.getAllSub
let addSubAsync = SubscriptionsRepository.addSubAsync
let updateSub = SubscriptionsRepository.updateSub
let deleteSub = SubscriptionsRepository.deleteSub

let updateGenreName = GenreRepository.updateGenreName
let getAllGenres = GenreRepository.getAllGenres
let deleteGenre = GenreRepository.deleteGenre
let addGenreAsync = GenreRepository.addGenreAsync