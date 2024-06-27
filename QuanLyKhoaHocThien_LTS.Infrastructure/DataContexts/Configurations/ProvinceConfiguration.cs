using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyKhoaHocThien_LTS.Domain.Entities;

namespace QuanLyKhoaHocThien_LTS.Infrastructure.DataContexts.Configurations
{
    public class ProvinceConfiguration : IEntityTypeConfiguration<Province>
    {
        public void Configure(EntityTypeBuilder<Province> builder)
        {
            
        }
    }
}
