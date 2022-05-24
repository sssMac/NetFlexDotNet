using System;
using Giraffe;
using GiraffeAPI.Data;

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
            RoleId = new Guid("57050332-97c2-4fb9-a9cd-97b5c86b35d6"),
            RoleName = "Redactor"
        });
        db.SaveChanges();
    }
}