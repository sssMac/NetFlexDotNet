module GiraffeAPI.Handler
open System
open System.Runtime.CompilerServices
open GiraffeAPI.Data
open GiraffeAPI.Models
open GiraffeAPI.Models.User
open GiraffeAPI.Models.CreateUserRequest
open GiraffeAPI.Models.Role
open GiraffeAPI.Models.CreateRole
open GiraffeAPI.Models.UserAuth
open GiraffeAPI.Models.CreateSubscription
open GiraffeAPI.Models.Subscription
open GiraffeAPI.Models.UserRole
open GiraffeAPI.Models.Genre
open GiraffeAPI.Models.FilmCreateRequest
open GiraffeAPI.Models.GenreCreate
open GiraffeAPI.Models.FilmUpdate
open GiraffeAPI.Models.Film
open Microsoft.AspNetCore.Http
open GiraffeAPI.JwtCreate
open Giraffe
open ApplicationContext

let UsersHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
            let context = ctx.RequestServices.GetService(typeof<ApplicationContext>) :?> ApplicationContext
            getAll context |> ctx.WriteJsonAsync
            
     // DONE
            
let UserHandler (id : Guid) = 
    fun (next : HttpFunc) (ctx : HttpContext) -> 
        let context = ctx.RequestServices.GetService(typeof<ApplicationContext>) :?> ApplicationContext
        getUser context id |> function
            | Some l -> ctx.WriteJsonAsync l
            | None -> (setStatusCode 404 >=> json "User not found") next ctx
            
    // DONE

let userAddHandler : HttpHandler = 
    fun (next : HttpFunc) (ctx : HttpContext) -> 
        task { 
            let context = ctx.RequestServices.GetService(typeof<ApplicationContext>) :?> ApplicationContext
            let! user = ctx.BindJsonAsync<CreateUserRequest>()
            match user.HasErrors with
            | Some msg -> return! (setStatusCode 400 >=> json msg) next ctx
            | None -> 
                return! addUserAsync context user.GetUser
                        |> Async.RunSynchronously
                        |> function 
                        | Some l -> Successful.CREATED l next ctx
                        | None -> (setStatusCode 400 >=> json "User with login or") next ctx
        }
        
        /// DONE
let UserUpdateHandler : HttpHandler = 
    fun (next : HttpFunc) (ctx : HttpContext) -> 
        task { 
            let context = ctx.RequestServices.GetService(typeof<ApplicationContext>) :?> ApplicationContext
            let! user = ctx.BindJsonAsync<User>()
            match user.HasErrors with
            | Some msg -> return! (setStatusCode 400 >=> json msg) next ctx
            | None -> 
                return! updateUser context user user.Id |> function 
                        | Some l -> ctx.WriteJsonAsync l
                        | None -> (setStatusCode 400 >=> json "User not updated") next ctx
        }

        // DONE
        
        
let UserBanHandler (id : Guid) = 
    fun (next : HttpFunc) (ctx : HttpContext) -> 
        let context = ctx.RequestServices.GetService(typeof<ApplicationContext>) :?> ApplicationContext
        banUser context id |> function 
        | Some l -> ctx.WriteJsonAsync l
        | None -> (setStatusCode 404 >=> json "User not found or already banned") next ctx
        
        // DONE
        
let UserUnbanHandler (id : Guid) = 
    fun (next : HttpFunc) (ctx : HttpContext) -> 
        let context = ctx.RequestServices.GetService(typeof<ApplicationContext>) :?> ApplicationContext
        unbunUser context id |> function 
        | Some l -> ctx.WriteJsonAsync l
        | None -> (setStatusCode 404 >=> json "User not found or already unbanned") next ctx
        
        // DONE
let UserDeleteHandler (id : Guid) = 
    fun (next : HttpFunc) (ctx : HttpContext) -> 
        let context = ctx.RequestServices.GetService(typeof<ApplicationContext>) :?> ApplicationContext
        deleteUser context id |> function
        | Some l -> ctx.WriteJsonAsync l
        | None -> (setStatusCode 404 >=> json "User not deleted") next ctx
        
        //DONE
        
        
let RolesHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
            let context = ctx.RequestServices.GetService(typeof<ApplicationContext>) :?> ApplicationContext
            getAllRoles context |> ctx.WriteJsonAsync
          
let RoleHandler (id : Guid) = 
    fun (next : HttpFunc) (ctx : HttpContext) -> 
        let context = ctx.RequestServices.GetService(typeof<ApplicationContext>) :?> ApplicationContext
        getRole context id |> function
            | Some l -> ctx.WriteJsonAsync l
            | None -> (setStatusCode 404 >=> json "Role not found") next ctx
            
let RoleAddHandler : HttpHandler = 
    fun (next : HttpFunc) (ctx : HttpContext) -> 
        task { 
            let context = ctx.RequestServices.GetService(typeof<ApplicationContext>) :?> ApplicationContext
            let! role = ctx.BindJsonAsync<CreateRole>()
            match role.HasErrors with
            | Some msg -> return! (setStatusCode 400 >=> json msg) next ctx
            | None -> 
                return! addRoleAsync context role.GetRole
                        |> Async.RunSynchronously
                        |> function 
                        | Some l -> Successful.CREATED l next ctx
                        | None -> (setStatusCode 400 >=> json "Role with this name made") next ctx
                        }
        
let RoleDeleteHandler (id : Guid) = 
    fun (next : HttpFunc) (ctx : HttpContext) -> 
        let context = ctx.RequestServices.GetService(typeof<ApplicationContext>) :?> ApplicationContext
        deleteRole context id |> function
        | Some l -> ctx.WriteJsonAsync l
        | None -> (setStatusCode 404 >=> json "Role not deleted") next ctx
        
let RoleUpdateHandler : HttpHandler = 
    fun (next : HttpFunc) (ctx : HttpContext) -> 
        task { 
            let context = ctx.RequestServices.GetService(typeof<ApplicationContext>) :?> ApplicationContext
            let! role = ctx.BindJsonAsync<Role>()
            match role.HasErrors with
            | Some msg -> return! (setStatusCode 400 >=> json msg) next ctx
            | None -> 
                return! updateRole context role role.RoleId |> function 
                        | Some l -> ctx.WriteJsonAsync l
                        | None -> (setStatusCode 400 >=> json "Role not updated") next ctx
        }
        
let AuthHandler : HttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            let context = ctx.RequestServices.GetService(typeof<ApplicationContext>) :?> ApplicationContext
            let! user = ctx.BindJsonAsync<UserAuth>()
            match user.HasErrors with
            | Some msg -> return! (setStatusCode 400 >=> json msg) next ctx
            | None -> 
                return! userAuth context user
                        |> Async.RunSynchronously
                        |> function
                        | Some l -> ctx.WriteJsonAsync (generateToken l)
                        | None -> (setStatusCode 400 >=> json "Auth is faild") next ctx
        }
        
        
let SubHandler (id : Guid) = 
    fun (next : HttpFunc) (ctx : HttpContext) -> 
        let context = ctx.RequestServices.GetService(typeof<ApplicationContext>) :?> ApplicationContext
        getSub context id |> function
            | Some l -> ctx.WriteJsonAsync l
            | None -> (setStatusCode 404 >=> json "Subscription not find") next ctx
            
let SubsHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
            let context = ctx.RequestServices.GetService(typeof<ApplicationContext>) :?> ApplicationContext
            getAllSub context |> ctx.WriteJsonAsync
            
            
let SubAddHandler : HttpHandler = 
    fun (next : HttpFunc) (ctx : HttpContext) -> 
        task { 
            let context = ctx.RequestServices.GetService(typeof<ApplicationContext>) :?> ApplicationContext
            let! sub = ctx.BindJsonAsync<CreateSubscription>()
            match sub.HasErrors with
            | Some msg -> return! (setStatusCode 400 >=> json msg) next ctx
            | None -> 
                return! addSubAsync context sub.GetSubscription
                        |> Async.RunSynchronously
                        |> function 
                        | Some l -> Successful.CREATED l next ctx
                        | None -> (setStatusCode 400 >=> json "Subscription with this name made") next ctx
                        }
        
let SubUpdateHandler : HttpHandler = 
    fun (next : HttpFunc) (ctx : HttpContext) -> 
        task { 
            let context = ctx.RequestServices.GetService(typeof<ApplicationContext>) :?> ApplicationContext
            let! sub = ctx.BindJsonAsync<Subscription>()
            match sub.HasErrors with
            | Some msg -> return! (setStatusCode 400 >=> json msg) next ctx
            | None -> 
                return! updateSub context sub sub.Id |> function 
                        | Some l -> ctx.WriteJsonAsync l
                        | None -> (setStatusCode 400 >=> json "Subscription not updated") next ctx
        }
        
let SubDeleteHandler (id : Guid) = 
    fun (next : HttpFunc) (ctx : HttpContext) -> 
        let context = ctx.RequestServices.GetService(typeof<ApplicationContext>) :?> ApplicationContext
        deleteSub context id |> function
        | Some l -> ctx.WriteJsonAsync l
        | None -> (setStatusCode 404 >=> json "Subscription not deleted") next ctx
        
let UserRoleUpdateHandler : HttpHandler = 
    fun (next : HttpFunc) (ctx : HttpContext) -> 
        task { 
            let context = ctx.RequestServices.GetService(typeof<ApplicationContext>) :?> ApplicationContext
            let! userRole = ctx.BindJsonAsync<UserRole>()
            return! updateUserRole context userRole userRole.UserId |> function
                        | Some l -> ctx.WriteJsonAsync l
                        | None -> (setStatusCode 400 >=> json "Role not updated") next ctx
        }
   
   
// GENRES        
let GenreUpdateNameHandler : HttpHandler = 
    fun (next : HttpFunc) (ctx : HttpContext) -> 
        task { 
            let context = ctx.RequestServices.GetService(typeof<ApplicationContext>) :?> ApplicationContext
            let! genre = ctx.BindJsonAsync<Genre>()
            return! updateGenreName context genre genre.Id |> function
                        | Some l -> ctx.WriteJsonAsync l
                        | None -> (setStatusCode 400 >=> json "Genre not updated") next ctx
        }
        
let GenresHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
            let context = ctx.RequestServices.GetService(typeof<ApplicationContext>) :?> ApplicationContext
            getAllGenres context |> ctx.WriteJsonAsync
            
let GenreDeleteHandler (id : Guid) = 
    fun (next : HttpFunc) (ctx : HttpContext) -> 
        let context = ctx.RequestServices.GetService(typeof<ApplicationContext>) :?> ApplicationContext
        deleteGenre context id |> function
        | Some l -> ctx.WriteJsonAsync l
        | None -> (setStatusCode 404 >=> json "Genre not deleted") next ctx
        
let GenreAddHandler : HttpHandler = 
    fun (next : HttpFunc) (ctx : HttpContext) -> 
        task { 
            let context = ctx.RequestServices.GetService(typeof<ApplicationContext>) :?> ApplicationContext
            let! genre = ctx.BindJsonAsync<GenreCreate>()
            match genre.HasErrors with
            | Some msg -> return! (setStatusCode 400 >=> json msg) next ctx
            | None -> 
                return! addGenreAsync context genre.GetGenre
                        |> Async.RunSynchronously
                        |> function 
                        | Some l -> Successful.CREATED l next ctx
                        | None -> (setStatusCode 400 >=> json "Genre with this name made") next ctx
                        }
       
        
let FilmsHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
            let context = ctx.RequestServices.GetService(typeof<ApplicationContext>) :?> ApplicationContext
            getAllFilms context |> ctx.WriteJsonAsync
            
let FilmHandler (id : Guid) = 
    fun (next : HttpFunc) (ctx : HttpContext) -> 
        let context = ctx.RequestServices.GetService(typeof<ApplicationContext>) :?> ApplicationContext
        getFilm context id |> function
            | Some l -> ctx.WriteJsonAsync l
            | None -> (setStatusCode 404 >=> json "Film not find") next ctx
            
let FilmAddHandler : HttpHandler = 
    fun (next : HttpFunc) (ctx : HttpContext) -> 
        task { 
            let context = ctx.RequestServices.GetService(typeof<ApplicationContext>) :?> ApplicationContext
            let! film = ctx.BindJsonAsync<FilmCreateRequest>()
            match film.HasErrors with
            | Some msg -> return! (setStatusCode 400 >=> json msg) next ctx
            | None -> 
                return! addFilmAsync context film.GetFilm film.GenreName 
                        |> Async.RunSynchronously
                        |> function 
                        | Some l -> Successful.CREATED l next ctx
                        | None -> (setStatusCode 400 >=> json "Genre with this name made") next ctx
                        }
        
let FilmDeleteHandler (id : Guid) = 
    fun (next : HttpFunc) (ctx : HttpContext) -> 
        let context = ctx.RequestServices.GetService(typeof<ApplicationContext>) :?> ApplicationContext
        deleteFilm context id |> function
        | Some l -> ctx.WriteJsonAsync l
        | None -> (setStatusCode 404 >=> json "Film not deleted") next ctx
        
let FilmUpdateHandler : HttpHandler = 
    fun (next : HttpFunc) (ctx : HttpContext) -> 
        task { 
            let context = ctx.RequestServices.GetService(typeof<ApplicationContext>) :?> ApplicationContext
            let! film = ctx.BindJsonAsync<FilmUpdate>()
            return! updateFilm context film.GetFilm film.GenreName |> function
                        | Some l -> ctx.WriteJsonAsync l
                        | None -> (setStatusCode 400 >=> json "Film not updated") next ctx
        }