using ImageGram.Core.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageGram.Core.Infrastructure.DataSources.Configurations.Common
{
    public class AspNetUserConfiguration : IEntityTypeConfiguration<AppUserIdentity>
    {
        public void Configure(EntityTypeBuilder<AppUserIdentity> builder)
        {
            builder.ToTable("AspNetUsers");

            builder.HasOne(prop => prop.Account);
        }
    }
}