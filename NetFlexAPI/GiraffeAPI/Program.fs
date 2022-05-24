module GiraffeAPI.App

open System
open System.IO
open GiraffeAPI.Data
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Cors.Infrastructure
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection
open Giraffe
open Handler
open ApplicationContext
open Microsoft.EntityFrameworkCore
open FSharp.Data.Sql
open Microsoft.AspNetCore.Authentication.JwtBearer
open Microsoft.IdentityModel.Tokens
open System.Security.Claims
open System.IdentityModel.Tokens.Jwt
open System.Text
open GiraffeAPI.JwtCreate

// ---------------------------------
// Models
// ---------------------------------

type Message =
    {
        Text : string
    }

// ---------------------------------
// Views
// ---------------------------------

module Views =
    open Giraffe.ViewEngine

    let layout (content: XmlNode list) =
        html [] [
            head [] [
                title []  [ encodedText "GiraffeAPI" ]
                link [ _rel  "stylesheet"
                       _type "text/css"
                       _href "/main.css" ]
            ]
            body [] content
        ]

    let partial () =
        h1 [] [ encodedText "GiraffeAPI" ]

    let index (model : Message) =
        [
            partial()
            p [] [ encodedText model.Text ]
        ] |> layout

// ---------------------------------
// Web app
// ---------------------------------

let indexHandler (name : string) =
    let greetings = sprintf "Hello %s, from Giraffe!" name
    let model     = { Text = greetings }
    let view      = Views.index model
    htmlView view

let webApp =
    choose [
        GET >=>
            choose [
                // USERS
                requireAdminRole >=> choose[
                    routef "/API/user/%O" UserHandler // get users by id ok   TESTED 
                    routef "/API/user/ban/%O" UserBanHandler // ban user  ok TESTED
                    routef "/API/user/unban/%O" UserUnbanHandler // unban user ok TESTED
                    route "/API/user" >=> UsersHandler //get all users ok
                    route "/API/role" >=> RolesHandler //get all roles ok
                    routef "/API/role/%O" RoleHandler //get role ok   TESTED 
                ]
            ]
        POST >=>
            choose [
                 requireAdminRole >=> choose[
                     // USERS
                     route "/API/user" >=>  userAddHandler
                     route "/API/user/update" >=>   UserUpdateHandler
                     //ROLES
                     route "/API/role" >=>  RoleAddHandler // add new role ok TESTED
                     route "/API/role/update" >=>  RoleUpdateHandler  // update role ok TESTED
                 ]
                 route "/API/user/auth" >=> AuthHandler // Auth TESTED
                 
            ]
        DELETE >=>
            choose [
                requireAdminRole >=> choose[
                    // USERS
                    routef "/API/user/delete/%O" UserDeleteHandler // delete user ok
                    //ROLES
                    routef "/API/role/delete/%O" RoleDeleteHandler // delete role ok
                ]
            ]
            
            
            
        setStatusCode 404 >=> text "Not Found" ]

// ---------------------------------
// Error handler
// ---------------------------------

let errorHandler (ex : Exception) (logger : ILogger) =
    logger.LogError(ex, "An unhandled exception has occurred while executing the request.")
    clearResponse >=> setStatusCode 500 >=> text ex.Message

// ---------------------------------
// Config and Main
// ---------------------------------

let configureCors (builder : CorsPolicyBuilder) =
    builder
        .WithOrigins(
            "http://localhost:5000",
            "https://localhost:5001")
       .AllowAnyMethod()
       .AllowAnyHeader()
       |> ignore

let configureApp (app : IApplicationBuilder) =
    let env = app.ApplicationServices.GetService<IWebHostEnvironment>()
    (match env.IsDevelopment() with
    | true  ->
        app.UseDeveloperExceptionPage()
            .UseAuthentication()
    | false ->
        app
            .UseAuthentication()
            .UseGiraffeErrorHandler(errorHandler)
            .UseHttpsRedirection())
        .UseCors(configureCors)
        .UseStaticFiles()
        .UseGiraffe(webApp)



let configureServices (services : IServiceCollection) =
    services.AddDbContext<ApplicationContext>
        (fun (options : DbContextOptionsBuilder) -> 
        options.UseNpgsql
            (@"User ID=postgres; Server=localhost; port=5432; Database=NetFlexDb; Password=ALEXRED123321!@; Pooling=true;") 
        |> ignore) |> ignore
    services.AddAuthentication(fun opt ->
        opt.DefaultAuthenticateScheme <- JwtBearerDefaults.AuthenticationScheme
    ).AddJwtBearer(fun (opt : JwtBearerOptions)->
        opt.TokenValidationParameters <-  TokenValidationParameters(
            ValidateActor = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "jwtwebapp.net",
                ValidAudience = "jwtwebapp.net",
                IssuerSigningKey = SymmetricSecurityKey(Encoding.UTF8.GetBytes(GiraffeAPI.JwtCreate.secret))
        )) |> ignore
    services.AddCors()    |> ignore
    services.AddGiraffe() |> ignore

let configureLogging (builder : ILoggingBuilder) =
    builder.AddConsole()
           .AddDebug() |> ignore
           
           
type Startup() =
    member _.ConfigureServices (services : IServiceCollection) =
        services.AddDbContext<ApplicationContext>
            (fun (options : DbContextOptionsBuilder) -> 
            options.UseNpgsql
                (@"User ID=postgres; Server=localhost; port=5432; Database=NetFlexDb; Password=ALEXRED123321!@; Pooling=true;") 
            |> ignore) |> ignore
        services.AddAuthentication(fun opt ->
            opt.DefaultAuthenticateScheme <- JwtBearerDefaults.AuthenticationScheme
            ).AddJwtBearer(fun (opt : JwtBearerOptions)->
        opt.TokenValidationParameters <-  TokenValidationParameters(
            ValidateActor = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "jwtwebapp.net",
                ValidAudience = "jwtwebapp.net",
                IssuerSigningKey = SymmetricSecurityKey(Encoding.UTF8.GetBytes(GiraffeAPI.JwtCreate.secret))
        )) |> ignore
        services.AddCors()    |> ignore
        services.AddGiraffe() |> ignore

    member _.Configure (app : IApplicationBuilder)
                        (_ : IHostEnvironment)
                        (_ : ILoggerFactory) =
        let env = app.ApplicationServices.GetService<IWebHostEnvironment>()
        (match env.IsDevelopment() with
    | true  ->
        app.UseDeveloperExceptionPage()
            .UseAuthentication()
    | false ->
        app
            .UseAuthentication()
            .UseGiraffeErrorHandler(errorHandler)
            .UseHttpsRedirection())
            .UseCors(configureCors)
            .UseStaticFiles()
            .UseGiraffe(webApp)

[<EntryPoint>]
let main args =
    let contentRoot = Directory.GetCurrentDirectory()
    let webRoot     = Path.Combine(contentRoot, "WebRoot")
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(
            fun webHostBuilder ->
                webHostBuilder
                    .UseContentRoot(contentRoot)
                    .UseWebRoot(webRoot)
                    .Configure(Action<IApplicationBuilder> configureApp)
                    .ConfigureServices(configureServices)
                    .ConfigureLogging(configureLogging)
                    |> ignore)
        .Build()
        .Run()
    0
    
    
