﻿using Microsoft.EntityFrameworkCore;
using Task.Models;

namespace Bliss.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Person> People { get; set; }

    }
}
