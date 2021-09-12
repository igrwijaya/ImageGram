using ImageGram.Core.Constant.Entity;
using ImageGram.Core.Domain.AggregateRoots.Post;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageGram.Core.Infrastructure.DataSources.Configurations.Post
{
    public class CommentEntityConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(prop => prop.CreatedBy)
                .HasMaxLength(CommonEntityConstant.AuditableUserLength);

            builder.Property(prop => prop.LastModifiedBy)
                .HasMaxLength(CommonEntityConstant.AuditableUserLength);

            builder.Property(prop => prop.Content)
                .HasMaxLength(PostEntityConstant.ContentLength)
                .IsRequired();
        }
    }
}