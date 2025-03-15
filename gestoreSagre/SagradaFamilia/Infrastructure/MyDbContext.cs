using Abstraction.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class MyDbContext : DbContext, IMyDbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }
        public DbSet<Acquirente> Acquirenti { get; set; }
        public DbSet<StockBiglietto> Stocks { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Biglietto> Biglietti { get; set; }
        public DbSet<Evento> Eventi { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public  DbSet<Organizzatore> Organizzatori { get; set; }
        public DbSet<Sagra> Sagre { get; set; }
        public DbSet<TipoBiglietto> TipiBiglietto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

    }
}
