using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyKhoaHocThien_LTS.Domain.Entities;

namespace QuanLyKhoaHocThien_LTS.Infrastructure.DataContexts.Configurations
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            
        }
    }
}
