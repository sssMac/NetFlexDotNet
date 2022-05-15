﻿module GiraffeAPI.Handler
open System
open System.Runtime.CompilerServices
open GiraffeAPI.Data
open GiraffeAPI.Models
open GiraffeAPI.Models.User
open GiraffeAPI.Models.CreateUserRequest
open GiraffeAPI.Models.Role
open GiraffeAPI.Models.CreateRole
open Microsoft.AspNetCore.Http
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