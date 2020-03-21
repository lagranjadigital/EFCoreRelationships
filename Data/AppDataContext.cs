using System;
using EfRelations.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EfRelations.Data
{
    public class AppDataContext : DbContext
    {
        public DbSet<User> Usuarios { get; set; }
        public DbSet<Group> Grupos { get; set; }
        public DbSet<UserGroup> UsuariosGrupos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite(@"Data Source=prueba.db");            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<UserGroup>().HasKey(ug => new {ug.UserId, ug.GroupId});
        }
    }
}