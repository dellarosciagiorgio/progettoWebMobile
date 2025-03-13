using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicam.WebMobile.SagradaFamilia.Models.Entities;

namespace Unicam.WebMobile.SagradaFamilia.Application.Abstraction.Context
{
    public interface IMyDbContext
    {
        public DbSet<Acquirente> Acquirenti { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Biglietto> Biglietti { get; set; }
        public DbSet<Evento> Eventi { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Organizzatore> Organizzatori { get; set; }
        public DbSet<Sagra> Sagre { get; set; }
        public DbSet<TipoBiglietto> TipiBiglietto { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        EntityEntry Entry(object entity);
        EntityEntry<T> Entry<T>(T entity) where T : class;
    }
}
