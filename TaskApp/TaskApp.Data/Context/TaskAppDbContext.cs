using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApp.Data.Models;
using Task = TaskApp.Data.Models.Task;

namespace TaskApp.Data.Context
{
    public class TaskAppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<Board> Boards { get; set; }

        public DbSet<Status> Statuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("Server=localhost;Database=taskapp;Uid=root;Pwd=Asdfghjkl1*;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<User>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Board>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Status>()
                .HasKey(t => t.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
