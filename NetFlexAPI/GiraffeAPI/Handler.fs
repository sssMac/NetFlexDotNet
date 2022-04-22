module GiraffeAPI.Handler
open System
open System.Runtime.CompilerServices
open GiraffeAPI.Data
open GiraffeAPI.Models
open GiraffeAPI.Models.User
open Microsoft.AspNetCore.Http
open Giraffe
open ApplicationContext

let UsersHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
            let context = ctx.RequestServices.GetService(typeof<ApplicationContext>) :?> ApplicationContext
            getAll context |> ctx.WriteJsonAsync
            
let UserHandler (id : Guid) = 
    fun (next : HttpFunc) (ctx : HttpContext) -> 
        let context = ctx.RequestServices.GetService(typeof<ApplicationContext>) :?> ApplicationContext
        getUser context id |> function
            | Some l -> ctx.WriteJsonAsync l
            | None -> (setStatusCode 404 >=> json "User not found") next ctx

let userAddHandler : HttpHandler = 
    fun (next : HttpFunc) (ctx : HttpContext) -> 
        task {
            let context = ctx.RequestServices.GetService(typeof<ApplicationContext>) :?> ApplicationContext
            let! user = ctx.BindJsonAsync<User>()
            addUserAsync context user
            return! next ctx
        }
let UserUpdateHandler (id : Guid) = 
    fun (next : HttpFunc) (ctx : HttpContext) -> 
        task { 
            let context = ctx.RequestServices.GetService(typeof<ApplicationContext>) :?> ApplicationContext
            let! user = ctx.BindJsonAsync<User>()
            updateUser context user
            return! next ctx
        }
let UserBanHandler (id : Guid) = 
    fun (next : HttpFunc) (ctx : HttpContext) -> 
        let context = ctx.RequestServices.GetService(typeof<ApplicationContext>) :?> ApplicationContext
        banUser context id |> function 
        | Some l -> ctx.WriteJsonAsync l
        | None -> (setStatusCode 404 >=> json "User not banned") next ctx

let UserUnbanHandler (id : Guid) = 
    fun (next : HttpFunc) (ctx : HttpContext) -> 
        let context = ctx.RequestServices.GetService(typeof<ApplicationContext>) :?> ApplicationContext
        unbunUser context id |> function 
        | Some l -> ctx.WriteJsonAsync l
        | None -> (setStatusCode 404 >=> json "User not unbanned") next ctx
        
let UserDeleteHandler (id : Guid) = 
    fun (next : HttpFunc) (ctx : HttpContext) -> 
        let context = ctx.RequestServices.GetService(typeof<ApplicationContext>) :?> ApplicationContext
        deleteUser context id |> function
        | Some l -> ctx.WriteJsonAsync l
        | None -> (setStatusCode 404 >=> json "User not deleted") next ctx
//        deleteUser context id |> function 
//        | l -> ctx.WriteJsonAsync l
//        | None -> (setStatusCode 404 >=> json "User not deleted") next ctx