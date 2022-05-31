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
open GiraffeAPI.Models.Film
open GiraffeAPI.Models.GenreVideo
open GiraffeAPI.Models.Serial
open GiraffeAPI.Models.Episode
open GiraffeAPI.Models.UserSubscription
open Microsoft.EntityFrameworkCore
open GiraffeAPI.Models.Review
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
            
        [<DefaultValue>]
        val mutable films:DbSet<Film>
        member x.Films 
            with get() = x.films 
            and set v = x.films <- v
            
        [<DefaultValue>]
        val mutable genrevideo:DbSet<GenreVideo>
        member x.GenreVideos 
            with get() = x.genrevideo 
            and set v = x.genrevideo <- v
            
        [<DefaultValue>]
        val mutable review:DbSet<Review>
        member x.Reviews 
            with get() = x.review 
            and set v = x.review <- v
            
        [<DefaultValue>]
        val mutable serial:DbSet<Serial>
        member x.Serials 
            with get() = x.serial 
            and set v = x.serial <- v
            
        [<DefaultValue>]
        val mutable episode:DbSet<Episode>
        member x.Episodes 
            with get() = x.episode 
            and set v = x.episode <- v
        
        [<DefaultValue>]
        val mutable usersubscription:DbSet<UserSubscription>
        member x.UserSubscriptions 
            with get() = x.usersubscription 
            and set v = x.usersubscription <- v
              
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
            let usersub : UserSubscription[]=
                [|
                    { UserId = entity.Id; SubscriptionId = Guid.Parse("9cda4bf9-db72-4299-a7ec-dc608fb4e2c1");StartDate = DateTime.UtcNow;FinishDate = DateTime.MaxValue }
                |]
            context.UserSubscriptions.AddRangeAsync(usersub)
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
        let currentSub = context.UserSubscriptions.Find(id)
        context.UserSubscriptions.Remove(currentSub)
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
        
    let updateUserSub (context : ApplicationContext) (entity : UserSubscription) =
        let current = context.UserSubscriptions.Find(entity.UserId)
        let updated = { entity with UserId = entity.UserId }
        context.Entry(current).CurrentValues.SetValues(updated)
        if context.SaveChanges true >= 1  then Some(updated) else None
        
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
        
module FilmRepository =
    
    let getAllFilms (context : ApplicationContext) = context.films
    
    let getFilm (context : ApplicationContext) (id : Guid) = context.Films |> Seq.tryFind (fun f -> f.Id = id)
    
    let addFilmAsync (context : ApplicationContext) (entity : Film) (genre : string) = 
        async {
            context.Films.AddRangeAsync(entity)
            |> Async.AwaitTask
            |> ignore
            let genreVideo : GenreVideo[]=
                [|
                    { Id = Guid.NewGuid() ; GenreName = genre; ContentId=entity.Id }
                |]
            context.GenreVideos.AddRangeAsync(genreVideo)
            |> Async.AwaitTask
            |> ignore
            let! result = context.SaveChangesAsync true |> Async.AwaitTask
            let result = if result >= 1  then Some(entity) else None
            return result
        }
        
    let deleteFilm (context:ApplicationContext) (id:Guid) =
        let current = context.Films.Find(id)
        context.Films.Remove(current)
        let currentGenre =
                query {
                    for genreVideo in context.GenreVideos do
                        find (genreVideo.ContentId.Equals (id))
                }
        context.GenreVideos.Remove(currentGenre)
        let reviews =
                query {
                    for review in context.Reviews do
                    where (review.ContentId = id)
                    select review
                }
        for item in reviews do
            context.Reviews.Remove(item)            
        if context.SaveChanges true  >= 1  then Some(current) else None
        
    let updateFilm (context : ApplicationContext) (entity : Film) (genre : string) = 
        let current = context.Films.Find(entity.Id)
        let updated = { entity with Id = entity.Id }
        context.Entry(current).CurrentValues.SetValues(updated)
        let currentGenre =
                query {
                    for genreVideo in context.GenreVideos do
                        find (genreVideo.ContentId.Equals (entity.Id))
                }
        let genreVideo : GenreVideo[]=
                [|
                    { Id = currentGenre.Id ; GenreName = genre; ContentId=currentGenre.ContentId }
                |]
        context.Entry(currentGenre).CurrentValues.SetValues(genreVideo[0])
        if context.SaveChanges true >= 1  then Some(updated) else None
    
        
        
    
module ReviewRepository =
    
    let addReviewAsync (context : ApplicationContext) (entity : Review) = 
        async {
            context.Reviews.AddRangeAsync(entity)
            |> Async.AwaitTask
            |> ignore
            let x = query {
                 for review in context.Reviews do
                 where (review.ContentId = entity.ContentId)
                 select review
            }
            let mutable y = entity.Rating
            let mutable z = 1.0
            for item in x do
                y <- y+item.Rating
                z <- z+1.0
            let current = context.Films.Find(entity.ContentId)
            let updated : Film[]=
                [|
                    { Id = current.Id ; Title = current.Title; Poster = current.Poster; AgeRating = current.AgeRating; UserRating = y/z; Description=current.Description;VideoLink=current.VideoLink }
                |]
            context.Entry(current).CurrentValues.SetValues(updated[0])
            let! result = context.SaveChangesAsync true |> Async.AwaitTask
            let result = if result >= 1  then Some(entity) else None
            return result
        }
        
    let addReviewSerialAsync (context : ApplicationContext) (entity : Review) = 
        async {
            context.Reviews.AddRangeAsync(entity)
            |> Async.AwaitTask
            |> ignore
            let x = query {
                 for review in context.Reviews do
                 where (review.ContentId = entity.ContentId)
                 select review
            }
            let mutable y = entity.Rating
            let mutable z = 1.0
            for item in x do
                y <- y+item.Rating
                z <- z+1.0
            let current = context.Serials.Find(entity.ContentId)
            let updated : Serial[]=
                [|
                    { Id = current.Id; Poster=current.Poster; Title=current.Title;NumEpisodes=current.NumEpisodes;AgeRating=current.AgeRating;UserRating=y/z;Description=current.Description }
                |]
            context.Entry(current).CurrentValues.SetValues(updated[0])
            let! result = context.SaveChangesAsync true |> Async.AwaitTask
            let result = if result >= 1  then Some(entity) else None
            return result
        }
        
        
    let getReview (context : ApplicationContext) (id : Guid) = context.Reviews |> Seq.tryFind (fun f -> f.Id = id)
    
    let getReviewByFilm (context : ApplicationContext) (id : Guid) =
        let x =
                query {
                    for review in context.Reviews do
                    where (review.ContentId = id)
                    select review
                }
        if x <> null then Some(x)
        else None
        
    let deleteReview (context:ApplicationContext) (id:Guid) =
        let current = context.Reviews.Find(id)
        context.Reviews.Remove(current)
        if context.SaveChanges true  >= 1  then Some(current) else None
        
        
module SerialRepository =
    
    let addSerialAsync (context : ApplicationContext) (entity : Serial) = 
        async {
            context.Serials.AddRangeAsync(entity)
            |> Async.AwaitTask
            |> ignore
            let! result = context.SaveChangesAsync true |> Async.AwaitTask
            let result = if result >= 1  then Some(entity) else None
            return result
        }
        
    let getAllSerials (context : ApplicationContext) = context.Serials
    
    let getSerial (context : ApplicationContext) id = context.serial |> Seq.tryFind (fun f -> f.Id = id)
    
    let deleteSerial (context:ApplicationContext) (id:Guid) =
        let current = context.Serials.Find(id)
        context.Serials.Remove(current)
        let episodes =
                query {
                    for episode in context.Episodes do
                    where (episode.SerialId = id)
                    select episode
                }
        for item in episodes do
            context.Episodes.Remove(item)
        let reviews =
                query {
                    for review in context.Reviews do
                    where (review.ContentId = id)
                    select review
                }
        for item in reviews do
            context.Reviews.Remove(item) 
        if context.SaveChanges true  >= 1  then Some(current) else None
        
    let updateSerial (context : ApplicationContext) (entity : Serial) = 
        let current = context.Serials.Find(entity.Id)
        let updated = { entity with Id = entity.Id }
        context.Entry(current).CurrentValues.SetValues(updated)
        if context.SaveChanges true >= 1  then Some(updated) else None
        
    
        
    
        
        
module EpisodeRepository =
    
    let addEpisodeAsync (context : ApplicationContext) (entity : Episode) = 
        async {
            context.Episodes.AddRangeAsync(entity)
            |> Async.AwaitTask
            |> ignore
            let! result = context.SaveChangesAsync true |> Async.AwaitTask
            let result = if result >= 1  then Some(entity) else None
            return result
        }
        
    let getEpisodesBySerial (context : ApplicationContext) (id : Guid) =
        let x =
                query {
                    for episode in context.Episodes do
                    where (episode.SerialId = id)
                    select episode
                }
        if x <> null then Some(x)
        else None
        
    let getEpisode (context : ApplicationContext) (id : Guid) = context.Episodes |> Seq.tryFind (fun f -> f.Id = id)
    
    let deleteEpisode (context:ApplicationContext) (id:Guid) =
        let current = context.Episodes.Find(id)
        context.Episodes.Remove(current)           
        if context.SaveChanges true  >= 1  then Some(current) else None
        
    let updateEpisode (context : ApplicationContext) (entity : Episode) = 
        let current = context.Episodes.Find(entity.Id)
        let updated = { entity with Id = entity.Id }
        context.Entry(current).CurrentValues.SetValues(updated)
        if context.SaveChanges true >= 1  then Some(updated) else None
        
        
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
let updateUserSub = SubscriptionsRepository.updateUserSub

let updateGenreName = GenreRepository.updateGenreName
let getAllGenres = GenreRepository.getAllGenres
let deleteGenre = GenreRepository.deleteGenre
let addGenreAsync = GenreRepository.addGenreAsync

let getFilm = FilmRepository.getFilm
let getAllFilms = FilmRepository.getAllFilms
let addFilmAsync = FilmRepository.addFilmAsync
let deleteFilm = FilmRepository.deleteFilm
let updateFilm = FilmRepository.updateFilm

let addReviewAsync = ReviewRepository.addReviewAsync
let getReview = ReviewRepository.getReview
let getReviewByFilm = ReviewRepository.getReviewByFilm
let deleteReview = ReviewRepository.deleteReview
let addReviewSerialAsync = ReviewRepository.addReviewSerialAsync

let addSerialAsync = SerialRepository.addSerialAsync
let getAllSerials = SerialRepository.getAllSerials
let getSerial = SerialRepository.getSerial
let deleteSerial = SerialRepository.deleteSerial
let updateSerial = SerialRepository.updateSerial

let addEpisodeAsync = EpisodeRepository.addEpisodeAsync
let getEpisodesBySerial = EpisodeRepository.getEpisodesBySerial
let getEpisode = EpisodeRepository.getEpisode
let deleteEpisode = EpisodeRepository.deleteEpisode
let updateEpisode = EpisodeRepository.updateEpisode