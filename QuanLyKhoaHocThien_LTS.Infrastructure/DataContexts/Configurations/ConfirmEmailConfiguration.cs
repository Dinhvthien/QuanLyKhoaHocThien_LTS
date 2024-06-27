using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyKhoaHocThien_LTS.Domain.Entities;
namespace QuanLyKhoaHocThien_LTS.Infrastructure.DataContexts.Configurations
{
    public class ConfirmEmailConfiguration : IEntityTypeConfiguration<ConfirmEmail>
    {
        public void Configure(EntityTypeBuilder<ConfirmEmail> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.User).WithMany(c => c.ConfirmEmails).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
