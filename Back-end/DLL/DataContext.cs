using System;
using System.Collections.Generic;
using System.Text;
using DLL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DLL
{
    public class DataContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DataContext()
        {
            Database.EnsureCreated();
        }
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=COFT;Trusted_Connection=True;");
            }
        }


    }


}
