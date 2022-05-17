module GiraffeAPI.JwtCreate

open System
open System.Text
open System.Security.Claims
open System.IdentityModel.Tokens.Jwt
open Microsoft.IdentityModel.Tokens
open Microsoft.AspNetCore.Authentication.JwtBearer
open Giraffe

let secret = "spadR2dre#u-ruBrE@TepA&*Uf@U"

let requireAdminRole : HttpHandler = 
    requiresRole "Admin" (RequestErrors.FORBIDDEN  "Permission denied. You must be an admin.")
   
let authenticate : HttpHandler =
    requiresAuthentication (challenge JwtBearerDefaults.AuthenticationScheme >=> text "please authenticate")
let generateToken email =
    let claims = [|
        Claim(JwtRegisteredClaimNames.Sub, email);
        Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        new Claim(ClaimTypes.Role, email) |]

    let expires = Nullable(DateTime.UtcNow.AddHours(1.0))
    let notBefore = Nullable(DateTime.UtcNow)
    let securityKey = SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
    let signingCredentials = SigningCredentials(key = securityKey, algorithm = SecurityAlgorithms.HmacSha256)

    let token =
        JwtSecurityToken(
            issuer = "jwtwebapp.net",
            audience = "jwtwebapp.net",
            claims = claims,
            expires = expires,
            notBefore = notBefore,
            signingCredentials = signingCredentials)

    let tokenResult = JwtSecurityTokenHandler().WriteToken(token)

    tokenResult

//let handleGetSecured =
//    fun (next : HttpFunc) (ctx : HttpContext) ->
//        let email = ctx.User.FindFirst ClaimTypes.NameIdentifier
//            
//        text ("User " + email.Value + " is authorized to access this resource.") next ctx
