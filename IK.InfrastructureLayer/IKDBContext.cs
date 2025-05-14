using IK.CoreLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IK.InfrastructureLayer
{
    public class IKDBContext:IdentityDbContext<ApplicationUser,Rol,int>
    {

        public IKDBContext() { }

        public IKDBContext(DbContextOptions options):base(options)
        {
            
        }
        public DbSet<Kurum> Kurumlar { get; set; }
        public DbSet<Bolum> Bolumler { get; set; }
        public DbSet<Paket> Paketler { get; set; }
        public DbSet<Personel> Personeller { get; set; }
        public DbSet<Bordro> Bordrolar { get; set; }
        public DbSet<HarcamaTalebi> HarcamaTalepleri { get; set; }
        public DbSet<IzinTalebi> IzinTalepleri { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source =.; Initial Catalog = DenemeIKOnionDB; Integrated Security = True; Trust Server Certificate = True");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int> { UserId = 1, RoleId = 1 });

        }
    }
}
