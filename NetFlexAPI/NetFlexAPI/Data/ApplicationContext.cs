﻿using Microsoft.EntityFrameworkCore;
using NetFlexAPI.Models;

namespace NetFlexAPI.Data;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Film> Films { get; set; }
    public DbSet<GenreVideo> GenreVideos { get; set; }
    public DbSet<Serial> Serials { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Episode> Episodes { get; set; }
    public DbSet<UserSubscription> UserSubscriptions { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("User ID=postgres; Server=localhost; port=5432; Database=NetFlexDb; Password=ALEXRED123321!@; Pooling=true;");
    }
}