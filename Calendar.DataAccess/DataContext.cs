using Calendar.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace Calendar.DataAccess
{
    public class DataContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("User ID=postgres;Password=cihan8;Server=localhost;Port=5432;Database=CalendarDb;Integrated Security=true", opt =>
            {
                opt.CommandTimeout(60_000);
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new {Id = Guid.NewGuid(), Name = "Sam", CreationTime = DateTime.Now, IsDeleted = false },
                new {Id = Guid.NewGuid(), Name = "Any", CreationTime = DateTime.Now, IsDeleted = false },
                new {Id = Guid.NewGuid(), Name = "Jay", CreationTime = DateTime.Now, IsDeleted = false },
                new {Id = Guid.NewGuid(), Name = "Samuel", CreationTime = DateTime.Now, IsDeleted = false },
                new {Id = Guid.NewGuid(), Name = "Mike", CreationTime = DateTime.Now, IsDeleted = false }
                );
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
