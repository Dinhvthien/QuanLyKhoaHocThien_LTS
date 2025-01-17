﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyKhoaHocThien_LTS.Domain.Entities;
namespace QuanLyKhoaHocThien_LTS.Infrastructure.DataContexts.Configurations
{
    public class PracticeConfiguration : IEntityTypeConfiguration<Practice>
    {
        public void Configure(EntityTypeBuilder<Practice> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.SubjectDetail).WithMany(c => c.Practices).HasForeignKey(c => c.SubjectDetailId).OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(c => c.ProgramingLanguage).WithMany(c => c.Practices).HasForeignKey(c => c.LanguageProgrammingId).OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
