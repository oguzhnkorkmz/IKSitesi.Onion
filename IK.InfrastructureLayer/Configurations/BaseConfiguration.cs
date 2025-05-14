using IK.CoreLayer.Abstracts;
using IK.CoreLayer.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.InfrastructureLayer.Configurations
{
    public abstract class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class, IEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(x => x.EklenmeTarihi)
                   .HasColumnType("smalldatetime")
                   .IsRequired();

            builder.Property(x => x.GuncellemeTarihi)
                   .HasColumnType("smalldatetime");

            builder.Property(x => x.SilmeTarihi)
                   .HasColumnType("smalldatetime");

            builder.Property(x => x.KayitDurumu)
                   .IsRequired();
        }
    }
}
    

