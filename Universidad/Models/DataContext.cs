﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Universidad.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User_type> User_types { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Teacher> Teachers { get; set; }
    }
}
