using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyKhoaHocThien_LTS.Domain.Entities;

namespace QuanLyKhoaHocThien_LTS.Infrastructure.DataContexts.Configurations
{
    public class CertificateConfiguration : IEntityTypeConfiguration<Certificate>
    {
        public void Configure(EntityTypeBuilder<Certificate> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.CertificateType).WithMany(c => c.Certificates).HasForeignKey(c => c.CertificateTypeId).OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
