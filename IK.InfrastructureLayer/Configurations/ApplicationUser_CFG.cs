using IK.CoreLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace IK.InfrastructureLayer.Configurations
{
    public class ApplicationUser_CFG : IEntityTypeConfiguration<ApplicationUser> // IEntityTypeConfiguration'ı implement ediyoruz
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasIndex(x => x.UserName).IsUnique();

            var adminUser = new ApplicationUser
            {
                Id = 1,
                UserName = "siteadmin",
                NormalizedUserName = "SITEADMIN",
                Email = "siteadmin@example.com",
                NormalizedEmail = "SITEADMIN@EXAMPLE.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                KurumID = 1 // Bu kullanıcı Kurum ile ilişkilendirilmek isteniyorsa
            };

            adminUser.PasswordHash = hasher.HashPassword(adminUser, "super_User123");

            builder.HasData(adminUser);
        }
    }
}
