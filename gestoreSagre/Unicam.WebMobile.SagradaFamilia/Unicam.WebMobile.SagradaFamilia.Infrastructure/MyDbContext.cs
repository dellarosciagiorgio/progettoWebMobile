using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Unicam.WebMobile.SagradaFamilia.Application.Abstraction.Context;
using Unicam.WebMobile.SagradaFamilia.Models.Entities;

namespace Unicam.WebMobile.SagradaFamilia.Infrastructure
{
    public class MyDbContext : DbContext, IMyDbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }
        public virtual DbSet<Acquirente> Acquirenti { get; set; }
        DbSet<Acquirente> IMyDbContext.Acquirenti { get ; set ; }
        DbSet<Admin> IMyDbContext.Admins { get; set; }
        DbSet<Biglietto> IMyDbContext.Biglietti { get; set; }
        DbSet<Evento> IMyDbContext.Eventi { get; set; }
        DbSet<Feedback> IMyDbContext.Feedbacks { get; set; }
        DbSet<Organizzatore> IMyDbContext.Organizzatori { get; set; }
        DbSet<Sagra> IMyDbContext.Sagre { get; set; }
        DbSet<TipoBiglietto> IMyDbContext.TipiBiglietto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        EntityEntry IMyDbContext.Entry(object entity)
        {
            throw new NotImplementedException();
        }

        EntityEntry<T> IMyDbContext.Entry<T>(T entity)
        {
            throw new NotImplementedException();
        }

        Task<int> IMyDbContext.SaveChangesAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
