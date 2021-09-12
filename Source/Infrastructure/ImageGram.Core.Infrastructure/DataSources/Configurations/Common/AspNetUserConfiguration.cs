using ImageGram.Core.Constant.Entity;
using ImageGram.Core.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageGram.Core.Infrastructure.DataSources.Configurations.Common
{
    public class AspNetUserConfiguration : IEntityTypeConfiguration<AppIdentityModel>
    {
        public void Configure(EntityTypeBuilder<AppIdentityModel> builder)
        {
            builder.ToTable("AspNetUsers");

            builder.Property(prop => prop.Name)
                .HasMaxLength(CommonEntityConstant.NameLength)
                .IsRequired();
        }
    }
}