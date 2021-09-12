using ImageGram.Core.Constant.Entity;
using ImageGram.Core.Domain.AggregateRoots.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageGram.Core.Infrastructure.DataSources.Configurations.Common
{
    public class AccountEntityConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.Property(prop => prop.CreatedBy)
                .HasMaxLength(CommonEntityConstant.AuditableUserLength);

            builder.Property(prop => prop.LastModifiedBy)
                .HasMaxLength(CommonEntityConstant.AuditableUserLength);

            builder.Property(prop => prop.Name)
                .HasMaxLength(CommonEntityConstant.NameLength)
                .IsRequired();

            builder.HasMany(prop => prop.Posts)
                .WithOne(post => post.Account)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(prop => prop.Comments)
                .WithOne(comm => comm.Account)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}