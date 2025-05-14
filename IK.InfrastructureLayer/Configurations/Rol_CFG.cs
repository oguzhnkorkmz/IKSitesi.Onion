using IK.CoreLayer.Entities;
using IK.CoreLayer.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace IK.InfrastructureLayer.Configurations
{
    public class Rol_CFG : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.HasData(
                new Rol { Id = 1, Name = "SiteAdmin", NormalizedName = "SITEADMIN", ConcurrencyStamp = Guid.NewGuid().ToString() },
                new Rol { Id = 2, Name = "KurumAdmin", NormalizedName = "KURUMADMIN", ConcurrencyStamp = Guid.NewGuid().ToString() },
                new Rol { Id = 3, Name = "Personel", NormalizedName = "PERSONEL", ConcurrencyStamp = Guid.NewGuid().ToString() }
            );
        }
    }

}
