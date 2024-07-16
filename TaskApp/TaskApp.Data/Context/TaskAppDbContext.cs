﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApp.Data.Models;

namespace TaskApp.Data.Context
{
    public class TaskAppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<TaskItem> Tasks { get; set; }

        public DbSet<Board> Boards { get; set; }

        public DbSet<Status> Statuses { get; set; }

        public TaskAppDbContext(DbContextOptions<TaskAppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItem>()
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