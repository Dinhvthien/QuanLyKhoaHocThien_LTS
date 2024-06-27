using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyKhoaHocThien_LTS.Domain.Entities;
namespace QuanLyKhoaHocThien_LTS.Infrastructure.DataContexts.Configurations
{
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.Creator).WithMany(c => c.Blogs).HasForeignKey(c => c.CreatorId).OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
