using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyKhoaHocThien_LTS.Domain.Entities;
namespace QuanLyKhoaHocThien_LTS.Infrastructure.DataContexts.Configurations
{
    public class CertificateTypeConfiguration : IEntityTypeConfiguration<CertificateType>
    {
        public void Configure(EntityTypeBuilder<CertificateType> builder)
        {
           
        }
    }
}
