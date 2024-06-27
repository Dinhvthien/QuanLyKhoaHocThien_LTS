using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyKhoaHocThien_LTS.Domain.Entities;
namespace QuanLyKhoaHocThien_LTS.Infrastructure.DataContexts.Configurations
{
    public class TestCaseConfiguration : IEntityTypeConfiguration<TestCase>
    {
        public void Configure(EntityTypeBuilder<TestCase> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.ProgramingLanguage).WithMany(c => c.TestCases).HasForeignKey(c => c.ProgramingLanguageId).OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(c => c.Practice).WithMany(c => c.TestCases).HasForeignKey(c => c.PracticeId).OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
