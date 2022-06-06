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
        db.Users.Add(new GiraffeAPI.Models.User.User
        {
            Id = new Guid("d0d4ccff-7564-4714-92ce-d3422ed877d8"),
            Avatar = "3",
            UserName = "Ilya@gmail.com",
            NormalizedUserName = "ILYA@GMAIL.COM",
            Email = "Ilya@gmail.com",
            NormalizedEmail = "ILYA@GMAIL.COM",
            EmailConfirmed = false,
            PasswordHash = "234432",
            IsBanned = false
        });
        db.UserRoles.Add(new GiraffeAPI.Models.UserRole.UserRole()
        {
            UserId = new Guid("d0d4ccff-7564-4714-92ce-d3422ed877d8"),
            RoleId = new Guid("37050332-97c2-4fb9-a9cd-97b5c86b35f9")
        });
        db.usersubscription.Add(new UserSubscription.UserSubscription()
        {
            UserId = new Guid("d0d4ccff-7564-4714-92ce-d3422ed877d8"),
            SubscriptionId = new Guid("7b022eab-2e8a-4ae7-a1a3-409b3f942069"),
            StartDate = DateTime.UtcNow,
            FinishDate = DateTime.UtcNow
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
        db.Films.Add(new Film.Film()
        {
            Id = new Guid("9fb1b7d6-67c5-4f70-8e2e-20f5445fa573"),
            Title = "123",
            Poster = "31231",
            AgeRating = 18,
            UserRating = 0.0,
            Description = "123",
            VideoLink = "[qwepqepq"
        });
        db.Films.Add(new Film.Film()
        {
            Id = new Guid("757ada68-5c30-464e-8b84-fc1dccc5cc3d"),
            Title = "Hello",
            Poster = "adaweqr",
            AgeRating = 18,
            UserRating = 0.0,
            Description = "qqwrqwrq",
            VideoLink = "[qwepqepq"
        });
        db.genrevideo.Add(new GenreVideo.GenreVideo()
        {
            Id = new Guid("8fb1b7d6-67c5-4f70-8e2e-20f5445fa589"),
            GenreName = "Horror",
            ContentId = new Guid("9fb1b7d6-67c5-4f70-8e2e-20f5445fa573")
        });
        db.review.Add(new Review.Review()
        {
            Id = new Guid("6253ccd7-7094-474f-8314-2d5d9e876dd6"),
            UserName = "Ilya@gmail.com",
            ContentId = new Guid("9fb1b7d6-67c5-4f70-8e2e-20f5445fa574"),
            Text ="Ploho",
            Rating = 2.0,
            PublishTime = DateTime.UtcNow
        });
        db.review.Add(new Review.Review()
        {
            Id = new Guid("12206271-6e1b-4483-a185-409ea2b25aad"),
            UserName = "Rezeda@gmail.com",
            ContentId = new Guid("757ada68-5c30-464e-8b84-fc1dccc5cc3d"),
            Text ="Horosho",
            Rating = 10.0,
            PublishTime = DateTime.UtcNow
        });
        db.Serials.Add(new Serial.Serial()
        {
            Id = new Guid("9fb1b7d6-67c5-4f70-8e2e-20f5445fa573"),
            Poster = "dada",
            Title ="dada",
            NumEpisodes = 4,
            AgeRating = 18,
            UserRating = 0.0,
            Description = "adada"
        });
        db.Genres.Add(new Genre.Genre()
        {
            Id = new Guid("9fb1b7d6-67c5-4f70-8e2e-20f5445fa588"),
            GenreName = "Shootings"
        });
        db.Episodes.Add(new Episode.Episode()
        {
            Id = new Guid("d7edd297-e544-4e4d-aac1-4db068e904cf"),
            Title ="dadadada",
            SerialId =new Guid("179bf6d6-9402-4f2a-8047-7e4aacb6d389"),
            Duration = 20,
            Number = 1,
            VideoLink ="dadada",
            PreviewVideo = "dadada"
        });
        db.Episodes.Add(new Episode.Episode()
        {
            Id = new Guid("4733e024-527a-4e74-865b-b1c9244d456d"),
            Title ="dadadada",
            SerialId =new Guid("9fb1b7d6-67c5-4f70-8e2e-20f5445fa573"),
            Duration = 20,
            Number = 1,
            VideoLink ="dadada",
            PreviewVideo = "dadada"
        });
        db.Serials.Add(new Serial.Serial()
        {
            Id = new Guid("db514859-d99f-4525-b592-d398298e349d"),
            Poster = "dada",
            Title ="dada",
            NumEpisodes = 4,
            AgeRating = 18,
            UserRating = 0.0,
            Description = "adada"
        });
        db.Episodes.Add(new Episode.Episode()
        {
            Id = new Guid("12d713a2-0c21-4252-8985-0db0c0d125ad"),
            Title ="dadadada",
            SerialId =new Guid("db514859-d99f-4525-b592-d398298e349d"),
            Duration = 20,
            Number = 1,
            VideoLink ="dadada",
            PreviewVideo = "dadada"
        });
        db.Serials.Add(new Serial.Serial()
        {
            Id = new Guid("9fb1b7d6-67c5-4f70-8e2e-20f5445fa573"),
            Poster = "dada",
            Title ="dada",
            NumEpisodes = 4,
            AgeRating = 18,
            UserRating = 0.0,
            Description = "adada"
        });
        db.SaveChanges();
    }
}