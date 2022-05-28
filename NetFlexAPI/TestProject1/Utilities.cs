using System;
using Giraffe;
using GiraffeAPI.Data;
using GiraffeAPI.Models;

namespace TestProject;

public class Utilities
{
    public static void InitializeDbForTests(ApplicationContext.ApplicationContext db)
    {
        db.Users.Add(new GiraffeAPI.Models.User.User
        {
            Id = new Guid("f92ac032-4341-4845-99c7-6dc2ed8a9624"),
            Avatar = "1",
            UserName = "Alexey@gmail.com",
            NormalizedUserName = "ALEXEY@GMAIL.COM",
            Email = "Alexey@gmail.com",
            NormalizedEmail = "ALEXEY@GMAIL.COM",
            EmailConfirmed = false,
            PasswordHash = "123321",
            IsBanned = false
        });
        db.Users.Add(new GiraffeAPI.Models.User.User
        {
            Id = new Guid("93d3cb87-1446-4c48-9ce2-e70ef629ec43"),
            Avatar = "2",
            UserName = "Ilya@gmail.com",
            NormalizedUserName = "ILYA@GMAIL.COM",
            Email = "Ilya@gmail.com",
            NormalizedEmail = "ILYA@GMAIL.COM",
            EmailConfirmed = false,
            PasswordHash = "234432",
            IsBanned = false
        });
        db.Users.Add(new GiraffeAPI.Models.User.User
        {
            Id = new Guid("55b4126a-d7a8-4d26-833c-303898f13018"),
            Avatar = "3",
            UserName = "Ilya@gmail.com",
            NormalizedUserName = "ILYA@GMAIL.COM",
            Email = "Ilya@gmail.com",
            NormalizedEmail = "ILYA@GMAIL.COM",
            EmailConfirmed = false,
            PasswordHash = "234432",
            IsBanned = false
        });
        db.Roles.Add(new GiraffeAPI.Models.Role.Role
        {
            RoleId = new Guid("37050332-97c2-4fb9-a9cd-97b5c86b35d6"),
            RoleName = "User"
        });
        db.Roles.Add(new GiraffeAPI.Models.Role.Role
        {
            RoleId = new Guid("47050332-97c2-4fb9-a9cd-97b5c86b35d6"),
            RoleName = "Admin"
        });
        db.Roles.Add(new GiraffeAPI.Models.Role.Role
        {
            RoleId = new Guid("9cda4bf9-db72-4299-a7ec-dc608fb4e2c1"),
            RoleName = "Redactor"
        });
        db.Roles.Add(new GiraffeAPI.Models.Role.Role
        {
            RoleId = new Guid("d586e9ee-1703-44ec-826c-39cbb700d420"),
            RoleName = "Friend"
        });
        db.Subscriptions.Add(new GiraffeAPI.Models.Subscription.Subscription()
        {
            Id = new Guid("9cda4bf9-db72-4299-a7ec-dc608fb4e2c1"),
            Name = "Standart",
            Price = "0",
        });
        db.Subscriptions.Add(new GiraffeAPI.Models.Subscription.Subscription()
        {
            Id = new Guid("d481d149-60d1-4853-96e1-32c8e9655171"),
            Name = "Students",
            Price = "100",
        });
        db.UserRoles.Add(new GiraffeAPI.Models.UserRole.UserRole()
        {
            UserId = new Guid("93d3cb87-1446-4c48-9ce2-e70ef629ec43"),
            RoleId = new Guid("37050332-97c2-4fb9-a9cd-97b5c86b35d6")
        });
        db.UserRoles.Add(new GiraffeAPI.Models.UserRole.UserRole()
        {
            UserId = new Guid("55b4126a-d7a8-4d26-833c-303898f13018"),
            RoleId = new Guid("37050332-97c2-4fb9-a9cd-97b5c86b35d6")
        });
        db.SaveChanges();
    }
}