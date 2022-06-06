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
                // FILMS
                routef "/API/film/%O" FilmHandler 
                route "/API/film" >=> FilmsHandler 
                // REVIEWS
                routef "/API/review/%O" ReviewHandler 
                routef "/API/reviews/%O" ReviewsHandler                
                
                routef "/API/sub/%O" SubHandler 
                
                routef "/API/user/%O" UserHandler 
            ]
        GET >=>
            choose [
                requireAdminRole >=> choose[
                    // USERS
                    routef "/API/user/ban/%O" UserBanHandler 
                    routef "/API/user/unban/%O" UserUnbanHandler 
                    route "/API/user" >=> UsersHandler 
                    // ROLES
                    route "/API/role" >=> RolesHandler
                    routef "/API/role/%O" RoleHandler 
                    // SUBSCRIPIONS
                    route "/API/sub" >=> SubsHandler 
                    // GENRE
                    route "/API/genre" >=> GenresHandler                
                ]
            ]
        POST >=>
            choose [
                  
                // REVIEWS
                route "/API/review/film" >=>  ReviewAddHandler  
                route "/API/review/serial" >=> ReviewSerialAddHandler 
                
            ]
        POST >=>
            choose [
                 requireAdminRole >=> choose[
                     // GENRES
                    route "/API/genre/update" >=> GenreUpdateNameHandler 
                    route "/API/genre" >=> GenreAddHandler 
                    // USERS
                    route "/API/user" >=>  userAddHandler  
                    route "/API/user/update" >=>   UserUpdateHandler 
                    //ROLES
                    route "/API/role" >=>  RoleAddHandler 
                    route "/API/role/update" >=>  RoleUpdateHandler  
                    route "/API/userrole/update" >=>  UserRoleUpdateHandler  
                    // SUBSCRIPIONS
                    route "/API/sub" >=>  SubAddHandler 
                    route "/API/sub/update" >=>  SubUpdateHandler  
                    route "/API/usersub/update" >=>  UserSubUpdateHandler  
                    // FILMS
                    route "/API/film" >=>  FilmAddHandler  
                    route "/API/film/update" >=>  FilmUpdateHandler
                    // SERIAL
                    route "/API/serial" >=>  SerialAddHandler  
                    route "/API/serial/update" >=>  SerialUpdateHandler  
                    // EPISODES
                    route "/API/episode" >=>  EpisodeAddHandler 
                    route "/API/episode/update" >=>  EpisodeUpdateHandler 
                
                    route "/API/user/auth" >=> AuthHandler 
                 ]
            ]
        DELETE >=>
            choose [
                // REVIEWS
                routef "/API/review/delete/%O" ReviewDeleteHandler   
            ]
        DELETE >=>
            choose [
                 requireAdminRole >=> choose[
                     // GENRES
                    routef "/API/genre/delete/%O" GenreDeleteHandler 
                    // USERS
                    routef "/API/user/delete/%O" UserDeleteHandler 
                    //ROLES
                    routef "/API/role/delete/%O" RoleDeleteHandler 
                    // SUBSCRIPTIONS
                    routef "/API/sub/delete/%O" SubDeleteHandler 
                    // FILMS
                    routef "/API/film/delete/%O" FilmDeleteHandler 
                    // SERIALS
                    routef "/API/serial/delete/%O" SerialDeleteHandler 
                    // EPISODES
                    routef "/API/episode/delete/%O" EpisodeDeleteHandler
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
    
    
