using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyKhoaHocThien_LTS.Domain.Entities;

namespace QuanLyKhoaHocThien_LTS.Infrastructure.DataContexts.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.User).WithMany(c => c.Notifications).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
